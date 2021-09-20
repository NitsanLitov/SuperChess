const socketIo = require("socket.io");
const uuidv4 = require("uuid").v4

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

        socket.on("disconnect", asd => {
            const nickname = nicknameBySocket[socket.id]
            console.log(`CLOSING ${nickname}!!!!!!!`)

            delete socketByNickname[nickname]
            delete nicknameBySocket[socket.id]
        })

        socket.on("game", nickname => {
            nickname = uuidv4()
            console.log(`nickname: ${nickname}`);
            nicknameBySocket[socket.id] = nickname
            socketByNickname[nickname] = socket

            const nicknames = getNicknames()
            const gameId = "thisIsTheGameId"

            if (nicknames.length === 2) {
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
    console.log(getNicknames())
    console.log(nickname)
    console.log(eventName)
    console.log(data)
    socketByNickname[nickname].emit(eventName, data);
}

function emitNicknames(nicknames, eventName, data) {
    nicknames.forEach(nickname => {
        emitNickname(nickname, eventName, data)
    });
}

module.exports = { startSocketIo }