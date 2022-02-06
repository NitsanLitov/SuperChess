var net = require('net');

const START_GAME_CATEGORY = "Start";
const UPDATE_PLAYERS_CATEGORY = "Players";
const UPDATE_MOVEMENT_CATEGORY = "UpdateMovementOptions";
const END_GAME_CATEGORY = "EndGame";
const MOVED_PIECE_CATEGORY = "MovedPiece";
const NOTIFY_MOVEMENT_CATEGORY = "NotifyMovementToAll";

let gamesClient = {}

function startGame(gameId, nicknames, updatePlayersInfo, notifyMovementToAll, updateMovementOptions, endGame) {
    console.log(`Connecting to chess server: ${gameId}-${nicknames}`)
    var client = new net.Socket();
    client.connect(3030, '127.0.0.1', function() {
        console.log('Connected');
        const data = { 'nicknames': nicknames }
        sendJson(client, { 'category': START_GAME_CATEGORY, 'data': data })

        gamesClient[gameId] = client
    });

    client.on('error', e => {
        console.log("handled error");
        console.log(e);
        endGame({ "reason": "Server connection has terminated, the game is finished", "nickname": "" })
        finishGame(gameId)
    });

    client.on('data', data => {
        console.log('Received: ' + data);
        gameContinues = handleMessage(JSON.parse(data), updatePlayersInfo, notifyMovementToAll, updateMovementOptions, endGame)
        if (!gameContinues) {
            finishGame(gameId)
        }
    });

    return client
}

function finishGame(gameId) {
    if (gamesClient[gameId])
        gamesClient[gameId].destroy();
    delete gamesClient[gameId];
}

function handleMessage(message, updatePlayersInfo, notifyMovementToAll, updateMovementOptions, endGame) {
    const data = message.data

    switch (message.category) {
        case UPDATE_PLAYERS_CATEGORY:
            updatePlayersInfo(data.players);
            return true;
        case UPDATE_MOVEMENT_CATEGORY:
            updateMovementOptions(data.nickname, data.movementOptions);
            return true;
        case END_GAME_CATEGORY:
            console.log("game ended")
            endGame(data)
            return false;
        case NOTIFY_MOVEMENT_CATEGORY:
            notifyMovementToAll(data.movedPieces)
            return true;
        default:
            console.log("unrecognized category")
            endGame({ "reason": "Bad communication with the server, the game is finished", "nickname": "" })
            return false;
    }
}

function sendJson(client, json) {
    const data = JSON.stringify(json)
    console.log(`Sending: ${data}`);
    return client.write(data);
}

function movePiece(gameId, nickname, movement) {
    if (!(gameId in gamesClient)) {
        console.log("game doesn't exists")
        return false
    }

    const data = { 'nickname': nickname, 'oldLocation': movement.oldLocation, 'newLocation': movement.newLocation }

    sendJson(gamesClient[gameId], { 'category': MOVED_PIECE_CATEGORY, 'data': data })
    return true
}

module.exports = { startGame, movePiece }