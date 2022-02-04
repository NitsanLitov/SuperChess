const socketIo = require("socket.io");
const uuidv4 = require("uuid").v4

const chess_communication = require('./chess_communication')

let io

let socketByNickname = {}
let games = {}

function startSocketIo(server) {
    io = socketIo(server);

    io.on("connection", (socket) => {
        console.log("New client connected");
        socket.emit('connection', null);

        socket.on("disconnect", () => {
            const nickname = Object.keys(socketByNickname).find(n => socketByNickname[n].id === socket.id);
            console.log(`CLOSING ${nickname} ws!!!!!!!`)

            delete socketByNickname[nickname]
        })

        socket.on("game", userData => {
            const nickname = userData.nickname
            const gameId = userData.gameId

            if (hasActiveSocket(nickname)) {
                console.log("nickname already taken");
                return;
            }
            socketByNickname[nickname] = socket

            if (!(gameId in games)) games[gameId] = []
            if (!(games[gameId].includes(nickname))) games[gameId].push(nickname)

            console.log(games)

            if (games[gameId].length === 2) { startTwoPlayersGame(gameId); }
        });
    });
}

function emitNickname(nickname, eventName, data) {
    if (hasActiveSocket(nickname)) {
        socketByNickname[nickname].emit(eventName, data);
    }
}

function emitNicknames(nicknames, eventName, data) {
    nicknames.forEach(nickname => {
        emitNickname(nickname, eventName, data)
    });
}

function hasActiveSocket(nickname) {
    return nickname in socketByNickname && socketByNickname[nickname] !== undefined
}

function startTwoPlayersGame(gameId) {
    const playingNicknames = games[gameId]

    updatePlayersInfo = players => emitNicknames(playingNicknames, "game", players);
    notifyMovementToAll = movedPieces => emitNicknames(playingNicknames, "movedPieces", movedPieces);
    updateMovementOptions = (nickname, movementOptions) => emitNickname(nickname, "movementOptions", movementOptions);
    endGame = result => {
        emitNicknames(playingNicknames, "endGame", result);
        playingNicknames.forEach(n => {
            if (hasActiveSocket(n)) { socketByNickname[n].disconnect(); }
            delete socketByNickname[n];
        });
        delete games[gameId]
    }

    chess_communication.startGame(gameId, playingNicknames, updatePlayersInfo, notifyMovementToAll, updateMovementOptions, endGame)

    playingNicknames.forEach(n => {
        if (hasActiveSocket(n)) {
            socketByNickname[n].on('move', (movement, ack) => {
                const result = chess_communication.movePiece(gameId, n, movement)
                ack(result)
            });
        }
    });
}

module.exports = { startSocketIo }