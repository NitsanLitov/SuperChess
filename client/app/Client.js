export default class Client {
    constructor({ ip, port }) {}

    start() {
        // gets starting places and player color
        const result = this.connect();
        return { playerColor: result.color, piecesByLocation: result.piecesByLocation };
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
            color: 'w',
            piecesByLocation
        };
    }
}