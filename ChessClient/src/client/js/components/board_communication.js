const React = require('react');
const { useState, useRef } = React

const { Board } = require('./game_board');

import socketClient from "socket.io-client";

export function ChessBoard(props) {
    const [socket, setSocket] = useState()

    const [nickname, setNickname] = useState("")
    const [gameId, setGameId] = useState("")
    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})

    const nicknameRef = useRef();
    nicknameRef.current = nickname;

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

    function movePiece(oldLocation, newLocation, callback) {
        socket.emit('move', { oldLocation, newLocation }, callback);
    }

    function startGame() {
        socket.emit('game', { nickname, gameId })
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
            <Board connectToServer={connectSocketIo} movePiece={movePiece} player={player} players={players} />
            {players.length === 0 && loginForum}
        </div>
    )
}
