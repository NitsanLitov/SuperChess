export default class Client {

    constructor({ ip, port }) {
        this.testingCount = 0
        this.Colors = {
            WHITE: "w",
            BLACK: "b",
            GREEN: "g"
        }
    }

    startGame() {
        // gets starting places and player color
        const result = this.connect();
        return { playerColor: result.color, startingPiecesByLocation: result.piecesByLocation, players: 2 };
    }

    movePiece(oldLocation, newLocation, pieceForTestingOnly) {
        let movedPieces = [];
        movedPieces.push([oldLocation, newLocation, pieceForTestingOnly]);

        return { movedPieces };
    }

    async getMovementOptions() {
        await this.sleep(5000)
        let movedPieces = [];
        this.testingCount += 1;
        if (this.testingCount === 1) {
            movedPieces.push(['e7', 'e5', { color: 'b', type: 'pawn' }]);
            return { movedPieces, movementOptions: { 'a2': ['a3', 'a4'], 'b1': ['a3', 'c3'] } };
        }
        if (this.testingCount === 2) {
            movedPieces.push(['d7', 'd5', { color: 'b', type: 'pawn' }]);
            return { movedPieces, movementOptions: {} };
        }
        if (this.testingCount === 3) {
            return { movedPieces, movementOptions: { 'a2': ['a3', 'a4'], 'c3': ['e4', 'b3', 'a4', 'b5', 'd5'] } };
        }
        if (this.testingCount === 4) {
            movedPieces.push(['b8', 'c6', { color: 'b', type: 'knight' }]);
            movedPieces.push(['f8', 'a3', { color: 'b', type: 'bishop' }]);
            return { movedPieces, movementOptions: { 'a2': ['a3', 'a4'] } };
        }
        if (this.testingCount === 5) {
            return { movedPieces, movementOptions: { 'a3': ['a4'] } };
        }
    }

    sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    connect() {
        // should connect to server and get data

        let piecesByLocation = new Map();

        piecesByLocation['a8'] = { color: 'b', type: 'rook' };
        piecesByLocation['b8'] = { color: 'b', type: 'knight' };
        piecesByLocation['c8'] = { color: 'b', type: 'bishop' };
        piecesByLocation['d8'] = { color: 'b', type: 'queen' };
        piecesByLocation['e8'] = { color: 'b', type: 'king' };
        piecesByLocation['f8'] = { color: 'b', type: 'bishop' };
        piecesByLocation['g8'] = { color: 'b', type: 'knight' };
        piecesByLocation['h8'] = { color: 'b', type: 'rook' };
        piecesByLocation['a7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['b7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['c7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['d7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['e7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['f7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['g7'] = { color: 'b', type: 'pawn' };
        piecesByLocation['h7'] = { color: 'b', type: 'pawn' };

        piecesByLocation['a1'] = { color: 'w', type: 'rook' };
        piecesByLocation['b1'] = { color: 'w', type: 'knight' };
        piecesByLocation['c1'] = { color: 'w', type: 'bishop' };
        piecesByLocation['d1'] = { color: 'w', type: 'queen' };
        piecesByLocation['e1'] = { color: 'w', type: 'king' };
        piecesByLocation['f1'] = { color: 'w', type: 'bishop' };
        piecesByLocation['g1'] = { color: 'w', type: 'knight' };
        piecesByLocation['h1'] = { color: 'w', type: 'rook' };
        piecesByLocation['a2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['b2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['c2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['d2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['e2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['f2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['g2'] = { color: 'w', type: 'pawn' };
        piecesByLocation['h2'] = { color: 'w', type: 'pawn' };
        return {
            color: this.Colors.BLACK,
            piecesByLocation
        };
    }
}