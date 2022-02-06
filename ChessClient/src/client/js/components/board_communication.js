const React = require('react');
const { useState, useEffect } = React

const { GameBoard } = require('./game_board');

import socketClient from "socket.io-client";

export function BoardCommunication(props) {
    const [socket, setSocket] = useState()
    const [nickname, setNickname] = useState("")
    const [gameId, setGameId] = useState("")

    const [waitingForGameStart, setWaitingForGameStart] = useState(false)

    useEffect(() => {
        connectSocketIo(props.handleStartGame, props.handleMovedPiecesChange, props.handleMovementOptionsChange, props.handleRefreshGame, props.handleEndGame)
    }, []);

    useEffect(() => {
        setNickname(props.finalNickname)
    }, [props.finalNickname]);

    function connectSocketIo(handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange, handleRefreshGame, handleEndGame) {
        var client = socketClient();
        setSocket(client)

        client.on('connection', () => {
            console.log(`connected with the back-end`);
        });

        client.on('disconnect', () => {
            console.log(`disconnected from the back-end`);
        });

        client.on('game', players => {
            handleStartGame(players)
        });

        client.on('refreshGame', data => {
            handleRefreshGame(data)
        });

        client.on('movedPieces', movedPieces => {
            handleMovedPiecesChange(movedPieces)
        });

        client.on('movementOptions', movementOptions => {
            handleMovementOptionsChange(movementOptions)
        });

        client.on('endGame', result => {
            handleEndGame(result)
        });
    }

    function movePiece(oldLocation, newLocation, callback) {
        socket.emit('move', { oldLocation, newLocation }, callback);
    }

    function startGame() {
        setWaitingForGameStart(true);
        props.setFinalNickname(nickname)
        socket.emit('game', { nickname, gameId })
    }

    const loginForum = (
        <div>
            <label>
                Nickname:
                <input type="text" value={nickname} onChange={e => setNickname(e.target.value)} />
            </label>
            <label>
                GameId:
                <input type="text" value={gameId} onChange={e => setGameId(e.target.value)} />
            </label>
            <button disabled={nickname === "" || gameId === "" || waitingForGameStart} onClick={(e) => startGame()}>Start Game</button>
        </div>
    )

    return (
        <div>
            {props.players.length === 0
                && (loginForum)
                || (
                    <GameBoard movePiece={movePiece} player={props.player} players={props.players}
                        gameEnded={props.gameEnded}
                        gameResult={props.gameResult}
                        piecesByLocation={props.piecesByLocation}
                        movementOptions={props.movementOptions}
                        lastMove={props.lastMove}
                        clearMovementOptions={props.clearMovementOptions}
                    />
                )
            }
        </div>
    )
}
