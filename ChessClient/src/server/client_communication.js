const socketIo = require("socket.io");
const uuidv4 = require("uuid").v4

const chess_communication = require('./chess_communication')

let io

let nicknameBySocket = {}
let socketByNickname = {}
let games = {}
let gameIdByNickname = {}

function getNicknames() { return Object.keys(socketByNickname) }

function startSocketIo(server) {
    io = socketIo(server);

    io.on("connection", (socket) => {
        console.log("New client connected");
        socket.emit('connection', null);

        socket.on("disconnect", () => {
            const nickname = nicknameBySocket[socket.id]
            console.log(`CLOSING ${nickname} ws!!!!!!!`)

            delete socketByNickname[nickname]
            delete nicknameBySocket[socket.id]
            delete games[gameIdByNickname[nickname]]
            delete gameIdByNickname[nickname]
        })

        socket.on("game", userData => {
            const nickname = userData.nickname
            const gameId = userData.gameId
            gameIdByNickname[nickname] = gameId

            // console.log(`nickname: [${nickname}] gameId: [${gameId}] socketId: [${socket.id}]`);
            nicknameBySocket[socket.id] = nickname
            socketByNickname[nickname] = socket

            if (!(gameId in games)) games[gameId] = []

            if (!(games[gameId].includes(nickname))) games[gameId].push(nickname)

            console.log(games)

            if (games[gameId].length === 2) {
                const playingNicknames = games[gameId]

                updatePlayersInfo = players => emitNicknames(playingNicknames, "game", players);
                notifyMovementToAll = movedPieces => emitNicknames(playingNicknames, "movedPieces", movedPieces);
                updateMovementOptions = (nickname, movementOptions) => emitNickname(nickname, "movementOptions", movementOptions);

                chess_communication.startGame(gameId, playingNicknames, updatePlayersInfo, notifyMovementToAll, updateMovementOptions)

                playingNicknames.forEach(n => {
                    socketByNickname[n].on('move', (movement, ack) => {
                        const result = chess_communication.movePiece(gameId, n, movement)
                        ack(result)
                    });
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