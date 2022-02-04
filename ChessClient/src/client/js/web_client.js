import socketClient from "socket.io-client";

export function connectSocketIo(nickname, gameId, handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame) {
    var socket = socketClient();
    socket.on('connection', () => {
        console.log(`connected with the back-end`);
        socket.emit("game", { nickname, gameId })
    });

    socket.on('game', players => {
        handleStartGame(players)
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

    console.log(socket.id)
    return socket;
}

export function movePiece(socket, oldLocation, newLocation, callback) {
    socket.emit('move', { oldLocation, newLocation }, callback);
}