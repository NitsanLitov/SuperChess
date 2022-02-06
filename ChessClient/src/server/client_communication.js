const socketIo = require("socket.io");
const uuidv4 = require("uuid").v4

const chess_communication = require('./chess_communication')

let io

let socketByNickname = {}
let gamesPlayers = {}
let gamesHistory = {}

function startSocketIo(server, sessionMiddleware) {
    io = socketIo(server);

    io.use((socket, next) => { sessionMiddleware(socket.request, {}, next); });

    io.on("connection", (socket) => {
        const session = socket.request.session;
        console.log("New client connected");
        socket.emit('connection', null);

        socket.on("disconnect", () => {
            const nickname = Object.keys(socketByNickname).find(n => socketByNickname[n].id === socket.id);
            console.log(`CLOSING ${nickname} ws!!!!!!!`)

            delete socketByNickname[nickname]
        })

        if (session.nickname) {
            console.log(`${session.nickname} reconnected`);
            const nickname = session.nickname;
            const gameId = session.gameId;
            if (gameId in gamesPlayers) {
                if (gamesPlayers[gameId].length === 2) {
                    if (gamesPlayers[gameId].includes(nickname)) {
                        socketByNickname[nickname] = socket;
                        updateRefreshedPlayer(nickname, gameId);
                    }
                } else {
                    const index = gamesPlayers[gameId].indexOf(nickname);
                    if (index > -1) {
                        gamesPlayers[gameId].splice(index, 1);
                    }
                }
            }
        }

        socket.on("game", userData => {
            const nickname = userData.nickname
            const gameId = userData.gameId

            if (nickname in socketByNickname) {
                console.log("Nickname already taken");
                return;
            }

            session.nickname = nickname;
            session.gameId = gameId;
            session.save();

            socketByNickname[nickname] = socket

            if (!(gameId in gamesPlayers)) gamesPlayers[gameId] = []

            if (gamesPlayers[gameId].length === 2) {
                console.log("Game already started");
                return;
            }

            if (!(gamesPlayers[gameId].includes(nickname))) gamesPlayers[gameId].push(nickname)

            console.log(gamesPlayers)

            if (gamesPlayers[gameId].length === 2) { startTwoPlayersGame(gameId); }
        });
    });
}

function startTwoPlayersGame(gameId) {
    const playingNicknames = gamesPlayers[gameId]
    gamesHistory[gameId] = {}

    updatePlayersInfo = players => {
        gamesHistory[gameId]['players'] = players;
        emitNicknames(playingNicknames, "game", players);
    }
    notifyMovementToAll = movedPieces => {
        if (!('movedPieces' in gamesHistory[gameId])) gamesHistory[gameId]['movedPieces'] = []
        gamesHistory[gameId]['movedPieces'] = [...gamesHistory[gameId]['movedPieces'], ...movedPieces];
        emitNicknames(playingNicknames, "movedPieces", movedPieces);
    }
    updateMovementOptions = (nickname, movementOptions) => {
        gamesHistory[gameId]['movementOptions'] = [nickname, movementOptions];
        emitNickname(nickname, "movementOptions", movementOptions);
    }
    endGame = result => {
        emitNicknames(playingNicknames, "endGame", result);
        playingNicknames.forEach(n => {
            if (hasActiveSocket(n)) { socketByNickname[n].disconnect(); }
            delete socketByNickname[n];
        });
        delete gamesPlayers[gameId]
        delete gamesHistory[gameId]
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

function hasActiveSocket(nickname) {
    return nickname in socketByNickname && socketByNickname[nickname] !== undefined
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

function updateRefreshedPlayer(nickname, gameId) {
    console.log("Refreshing player");
    data = {
        nickname: nickname,
        gameId: gameId
    }

    if (gameId in gamesHistory && 'players' in gamesHistory[gameId] && gamesHistory[gameId]['players'] !== undefined) {
        data['players'] = gamesHistory[gameId]['players']

        if ('movedPieces' in gamesHistory[gameId]) {
            data['movedPieces'] = gamesHistory[gameId]['movedPieces']
        }

        if ('movementOptions' in gamesHistory[gameId] && gamesHistory[gameId]['movementOptions'][0] === nickname) {
            data['movementOptions'] = gamesHistory[gameId]['movementOptions'][1]
        }

        if (hasActiveSocket(nickname)) {
            socketByNickname[nickname].on('move', (movement, ack) => {
                const result = chess_communication.movePiece(gameId, nickname, movement)
                ack(result)
            });
        }
    }

    socketByNickname[nickname].emit("refreshGame", data);
}

module.exports = { startSocketIo }