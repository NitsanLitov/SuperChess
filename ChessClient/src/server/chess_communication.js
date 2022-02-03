var net = require('net');

const START_GAME_CATEGORY = "Start";
const UPDATE_PLAYERS_CATEGORY = "Players";
const UPDATE_MOVEMENT_CATEGORY = "UpdateMovementOptions";
const END_GAME_CATEGORY = "EndGame";
const MOVED_PIECE_CATEGORY = "MovedPiece";
const NOTIFY_MOVEMENT_CATEGORY = "NotifyMovementToAll";

let games = {}

function startGame(gameId, nicknames, updatePlayersInfo, notifyMovementToAll, updateMovementOptions) {
    console.log(`Connecting to chess server: ${gameId}-${nicknames}`)
    var client = new net.Socket();
    client.connect(3030, '127.0.0.1', function() {
        console.log('Connected');
        const data = { 'nicknames': nicknames }
        sendJson(client, { 'category': START_GAME_CATEGORY, 'data': data })

        games[gameId] = client
    });

    client.on('error', e => {
        console.log("handled error");
        console.log(e);
    });

    client.on('data', data => {
        console.log('Received: ' + data);
        handleMessage(JSON.parse(data), updatePlayersInfo, notifyMovementToAll, updateMovementOptions)
    });

    return client
}

function handleMessage(message, updatePlayersInfo, notifyMovementToAll, updateMovementOptions) {
    const data = message.data

    switch (message.category) {
        case UPDATE_PLAYERS_CATEGORY:
            updatePlayersInfo(data.players);
            break;
        case UPDATE_MOVEMENT_CATEGORY:
            updateMovementOptions(data.nickname, data.movementOptions);
            break;
        case END_GAME_CATEGORY:
            console.log("game ended")
                // ToDo: update clients game ended
            break;
        case NOTIFY_MOVEMENT_CATEGORY:
            notifyMovementToAll(data.movedPieces)
            break;
        default:
            console.log("unrecognized category")
    }
}

function sendJson(client, json) {
    const data = JSON.stringify(json)
    console.log(`Sending: ${data}`);
    return client.write(data);
}

function movePiece(gameId, nickname, movement) {
    if (!(gameId in games)) {
        console.log("game doesn't exists")
        return false
    }

    const data = { 'nickname': nickname, 'oldLocation': movement.oldLocation, 'newLocation': movement.newLocation }

    sendJson(games[gameId], { 'category': MOVED_PIECE_CATEGORY, 'data': data })
    return true
}

module.exports = { startGame, movePiece }