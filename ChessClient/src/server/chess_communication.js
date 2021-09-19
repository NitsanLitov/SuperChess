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
        sendJson(client, { 'Category': START_GAME_CATEGORY, 'Nicknames': nicknames })

        games[gameId] = client
    });

    client.on('error', function(e) {
        console.log("handled error");
        console.log(e);
    });

    client.on('data', function(data) {
        console.log('Received: ' + data);
        handleData(JSON.parse(data), updatePlayersInfo, notifyMovementToAll, updateMovementOptions)
    });

    return client
}

function handleData(data, updatePlayersInfo, notifyMovementToAll, updateMovementOptions) {
    switch (data.Category) {
        case UPDATE_PLAYERS_CATEGORY:
            updatePlayersInfo(data.Players);
            break;
        case UPDATE_MOVEMENT_CATEGORY:
            updateMovementOptions(data.Nickname, data.MovementOptions);
            break;
        case END_GAME_CATEGORY:
            console.log("game ended")
            break;
        case NOTIFY_MOVEMENT_CATEGORY:
            notifyMovementToAll(data.MovedPieces)
            break;
        default:
            console.log("unrecognized category")
    }
}

function sendJson(client, json) {
    return client.write(JSON.stringify(json));
}

function movePiece(gameId, nickname, movement) {
    if (!(gameId in games)) {
        console.log("game doesn't exists")
        return false
    }
    sendJson(games[gameId], { 'Category': MOVED_PIECE_CATEGORY, 'nickname': nickname, 'oldLocation': movement.oldLocation, 'newLocation': movement.newLocation })

    return true
}

module.exports = { startGame, movePiece }