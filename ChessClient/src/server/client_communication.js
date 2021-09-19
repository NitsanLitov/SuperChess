const socketIo = require("socket.io");

const chess_communication = require('./chess_communication')

let io

let nicknameBySocket = {}
let socketByNickname = {}
let games = {}

function getNicknames() { return Object.keys(socketByNickname) }

function startSocketIo(server) {
    io = socketIo(server);

    io.on("connection", (socket) => {
        console.log("New client connected");
        socket.emit('connection', null);

        socket.on("game", nickname => {
            console.log(`nickname: ${nickname}`);
            nicknameBySocket[socket.id] = nickname
            socketByNickname[nickname] = socket

            const nicknames = getNicknames()
            const gameId = "thisIsTheGameId"

            if (nicknames.length === 1) {
                games[gameId] = nicknames

                updatePlayersInfo = players => emitNicknames(nicknames, "game", players);
                notifyMovementToAll = movedPieces => emitNicknames(nicknames, "movedPieces", movedPieces);
                updateMovementOptions = (nickname, movementOptions) => emitNickname(nickname, "movementOptions", movementOptions);

                chess_communication.startGame(gameId, nicknames, updatePlayersInfo, notifyMovementToAll, updateMovementOptions)

                socket.on("move", (movement, ack) => {
                    console.log(`${nicknameBySocket[socket.id]} movement: ${movement}`);
                    const result = chess_communication.movePiece(gameId, nickname, movement)
                    ack(result)
                });
            }
        });
    });
}

function emitNickname(nickname, eventName, data) {
    socketByNickname[nickname].emit(eventName, data);
}

function emitNicknames(nicknames, eventName, data) {
    nicknames.forEach(nickname => {
        emitNickname(nickname, eventName, data)
    });
}

module.exports = { startSocketIo }