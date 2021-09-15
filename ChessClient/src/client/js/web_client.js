export function startGame() {
    // gets starting places and player color
    const result = connect();
    return { players: result.players, startingPieces: result.piecesByLocation };
}

export function movePiece(oldLocation, newLocation) {
    console.log("Moving ${oldLocation} to ${newLocation}")
    return true
}

export async function getMovementOptions(testingCount) {
    await sleep(2000)
    let movedPieces = [];
    if (testingCount === 1) {
        movedPieces.push([{ color: 'b', type: 'pawn' }, 'e7', 'e5']);
        return { movedPieces, movementOptions: { 'a2': ['a3', 'a4'], 'b1': ['a3', 'c3'] } };
    }
    if (testingCount === 2) {
        movedPieces.push([{ color: 'w', type: 'knight' }, 'b1', 'c3']);
        return { movedPieces, movementOptions: {} };
    }
    if (testingCount === 3) {
        movedPieces.push([{ color: 'b', type: 'pawn' }, 'd7', 'd5']);
        return { movedPieces, movementOptions: {} };
    }
    if (testingCount === 4) {
        return { movedPieces, movementOptions: { 'a2': ['a3', 'a4'], 'c3': ['e4', 'b3', 'a4', 'b5', 'd5'] } };
    }
    if (testingCount === 5) {
        movedPieces.push([{ color: 'w', type: 'pawn' }, 'a2', 'a3']);
        movedPieces.push([{ color: 'b', type: 'knight' }, 'b8', 'c6']);
        movedPieces.push([{ color: 'b', type: 'bishop' }, 'f8', 'a3']);
        return { movedPieces, movementOptions: { 'c3': ['e4', 'b3', 'a4', 'b5', 'd5'] } };
    }
    if (testingCount === 6) {
        movedPieces.push([{ color: 'w', type: 'knight' }, 'c3', 'd5']);
        return { movedPieces, movementOptions: { 'd5': ['d6'] } };
    }
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function connect() {
    // should connect to server and get data

    let piecesByLocation = [];

    piecesByLocation.push([{ color: 'b', type: 'rook' }, 'a8', 'a8']);
    piecesByLocation.push([{ color: 'b', type: 'knight' }, 'b8', 'b8']);
    piecesByLocation.push([{ color: 'b', type: 'bishop' }, 'c8', 'c8']);
    piecesByLocation.push([{ color: 'b', type: 'queen' }, 'd8', 'd8']);
    piecesByLocation.push([{ color: 'b', type: 'king' }, 'e8', 'e8']);
    piecesByLocation.push([{ color: 'b', type: 'bishop' }, 'f8', 'f8']);
    piecesByLocation.push([{ color: 'b', type: 'knight' }, 'g8', 'g8']);
    piecesByLocation.push([{ color: 'b', type: 'rook' }, 'h8', 'h8']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'a7', 'a7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'b7', 'b7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'c7', 'c7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'd7', 'd7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'e7', 'e7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'f7', 'f7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'g7', 'g7']);
    piecesByLocation.push([{ color: 'b', type: 'pawn' }, 'h7', 'h7']);

    piecesByLocation.push([{ color: 'w', type: 'rook' }, 'a1', 'a1']);
    piecesByLocation.push([{ color: 'w', type: 'knight' }, 'b1', 'b1']);
    piecesByLocation.push([{ color: 'w', type: 'bishop' }, 'c1', 'c1']);
    piecesByLocation.push([{ color: 'w', type: 'queen' }, 'd1', 'd1']);
    piecesByLocation.push([{ color: 'w', type: 'king' }, 'e1', 'e1']);
    piecesByLocation.push([{ color: 'w', type: 'bishop' }, 'f1', 'f1']);
    piecesByLocation.push([{ color: 'w', type: 'knight' }, 'g1', 'g1']);
    piecesByLocation.push([{ color: 'w', type: 'rook' }, 'h1', 'h1']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'a2', 'a2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'b2', 'b2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'c2', 'c2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'd2', 'd2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'e2', 'e2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'f2', 'f2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'g2', 'g2']);
    piecesByLocation.push([{ color: 'w', type: 'pawn' }, 'h2', 'h2']);
    return {
        players: [{ color: 'w', nickname: 'Nitsan' }, { color: 'b', nickname: 'Eran' }],
        piecesByLocation
    };
}