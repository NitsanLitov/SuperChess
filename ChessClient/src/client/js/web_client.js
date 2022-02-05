import socketClient from "socket.io-client";

export function connectSocketIo(handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame, handleRefreshGame) {
    var socket = socketClient();
    socket.on('connection', () => {
        console.log(`connected with the back-end`);
    });

    socket.on('disconnect', () => {
        console.log(`disconnected from the back-end`);
    });

    socket.on('game', players => {
        handleStartGame(players)
    });

    socket.on('refreshGame', data => {
        handleRefreshGame(data)
    });

    socket.on('movedPieces', movedPieces => {
        handleMovedPiecesChange(movedPieces)
    });

    socket.on('movementOptions', movementOptions => {
        handleMovementOptionsChange(movementOptions)
    });

    socket.on('endGame', result => {
        handleEndGame(result)
    });

    return socket;
}

export function movePiece(socket, oldLocation, newLocation, callback) {
    socket.emit('move', { oldLocation, newLocation }, callback);
}

export function startGame(socket, nickname, gameId) {
    socket.emit('game', { nickname, gameId })
}