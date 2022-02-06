const React = require('react');
const { useState } = React

const { Board } = require('./board');

import socketClient from "socket.io-client";

export function ChessBoard(props) {
    const [socket, setSocket] = useState()

    function connectSocketIo(handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame, handleRefreshGame) {
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
    
    function startGame(nickname, gameId) {
        socket.emit('game', { nickname, gameId })
    }

    return (
        <Board connectToServer={connectSocketIo} movePiece={movePiece} startGame={startGame} />
    )
}
