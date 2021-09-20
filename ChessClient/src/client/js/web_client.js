import socketClient from "socket.io-client";

export function connectSocketIo(nickname, handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange) {
    // var socket = socketClient(SERVER);
    var socket = socketClient();
    socket.on('connection', () => {
        console.log(`connected with the back-end`);
        socket.emit("game", nickname)
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

    return socket;
}

export function movePiece(socket, oldLocation, newLocation, callback) {
    socket.emit('move', { oldLocation, newLocation }, callback);
}