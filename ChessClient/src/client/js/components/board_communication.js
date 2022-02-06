const React = require('react');
const { useState, useRef, useEffect } = React

const { GameBoard } = require('./game_board');

import socketClient from "socket.io-client";

export function BoardCommunication(props) {
    const [socket, setSocket] = useState()
    const [nickname, setNickname] = useState("")
    const [gameId, setGameId] = useState("")

    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})

    const nicknameRef = useRef();
    nicknameRef.current = nickname;

    useEffect(() => {
        connectSocketIo(props.handleMovedPiecesChange, props.handleMovementOptionsChange, props.handleEndGame)
    }, []);

    function connectSocketIo(handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame) {
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

    function handleStartGame(playingPlayers) {
        setPlayer(playingPlayers.find(p => p.nickname === nicknameRef.current))
        setPlayers(playingPlayers)
    }

    function handleRefreshGame(data) {
        console.log("Refreshing")
        setNickname(data.nickname)
        setGameId(data.gameId)
        handleStartGame(data.players)
    }

    function movePiece(oldLocation, newLocation, callback) {
        socket.emit('move', { oldLocation, newLocation }, callback);
    }

    function startGame() {
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
            <button disabled={nickname === "" || gameId === ""} onClick={(e) => startGame()}>Start Game</button>
        </div>
    )

    return (
        <div>
            {players.length === 0
                && loginForum
                || (
                    <GameBoard movePiece={movePiece} player={player} players={players}
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
