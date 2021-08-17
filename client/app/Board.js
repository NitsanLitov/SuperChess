import Square from './Square.js';

const colors = [
    '#5d8aa8',
    '#f0f8ff',
    '#e32636',
    '#efdecd',
    '#e52b50',
    '#ffbf00',
    '#ff033e',
    '#9966cc',
    '#a4c639',
    '#f2f3f4',
    '#cd9575',
    '#915c83',
    '#faebd7',
    '#008000',
];

export default class Board {
    constructor({
        selector,
        playerColor,
        startingPiecesByLocation,
        players
    }) {
        this.squares = [];
        this.element = document.querySelector(selector);
        this.element.classList.add('board');
        this.playerColor = playerColor;
        if (players === 2) {
            this.initTwoPlayers();
        }
        this.updatePiecesLocations(startingPiecesByLocation);
    }

    initTwoPlayers() {
        const size = '90vmin';
        this.element.style.width = size;
        this.element.style.height = size;

        this.squaresByLocation = new Map();
        this.piecesByLocation = new Map();

        this.squares = Array.from({ length: 64 }, (_, index) => {
            const number = 8 - Math.floor(index / 8);
            const column = index % 8;
            const letter = String.fromCharCode(('A'.charCodeAt(0) + column));
            const isBlack = !(number % 2 === column % 2);
            const square = new Square({
                board: this,
                number,
                letter,
                isBlack
            });
            this.squaresByLocation.set(`${letter.toLowerCase()}${number}`, square);
            this.piecesByLocation.set(`${letter.toLowerCase()}${number}`, null);
            this.element.appendChild(square.element);
            return square;
        });
    }

    getSquare(letter, number) {
        return this.squaresByLocation.get(`${letter.toLowerCase()}${number}`);
    }

    getPieceByLocation(letter, number) {
        return this.piecesByLocation.get(`${letter.toLowerCase()}${number}`);
    }

    updateAllSquares() {
        this.squares.forEach((square) => square.update());
    }

    updatePiecesLocations(piecesByLocation) {
        for (const [location, piece] of Object.entries(piecesByLocation)) {
            this.piecesByLocation.set(location, piece);
            this.squaresByLocation.get(location).update();
        }
    }
}