import Square from './Square.js';
import Client from './Client.js';

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
    constructor({ selector }) {
        this.client = new Client("127.0.0.1", 54321);
        this.squares = [];
        this.movementOptions = {};
        this.movementSquares = [];
        this.movingLocation = "";
        this.element = document.querySelector(selector);
        this.element.classList.add('board');
    }

    async startGame() {
        const { playerColor, startingPiecesByLocation, players } = this.client.startGame();
        this.playerColor = playerColor;

        if (players === 2) {
            this.initTwoPlayers();
        }
        this.updatePiecesLocations(startingPiecesByLocation);

        //while (true) {
        const { piecesByLocationDiff, movementOptions } = await this.client.getMovementOptions();
        this.updatePiecesLocations(piecesByLocationDiff);
        this.setMovementOptions(movementOptions);
        //}
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

    getSquareByLocationString(location) {
        return this.squaresByLocation.get(location);
    }

    getPieceByLocationTuple(letter, number) {
        return this.piecesByLocation.get(`${letter.toLowerCase()}${number}`);
    }

    getPieceByLocationString(location) {
        return this.piecesByLocation.get(location);
    }

    updateAllSquares() {
        this.squares.forEach((square) => square.update());
    }

    updatePiecesLocations(piecesByLocation) {
        for (const [location, piece] of Object.entries(piecesByLocation)) {
            this.piecesByLocation.set(location, piece);
            this.getSquareByLocationString(location).update();
        }
    }

    setMovementOptions(movementOptions) {
        console.log("set movement options");
        this.movementOptions = movementOptions;
    }

    getMovementOptionsByLocationString(locationString) {
        if (locationString in this.movementOptions) return this.movementOptions[locationString];
        return [];
    }

    colorMovementOptions(letter, number) {
        this.movingLocation = `${letter.toLowerCase()}${number}`;
        this.movementSquares = this.getMovementOptionsByLocationString(this.movingLocation);
        this.updateAllSquares()
    }

    unColorMovementOptions(letter, number) {
        this.setMovementOptions({});
        this.movingLocation = '';
        this.movementSquares = [];
        this.updateAllSquares()
    }

    isMovementSquare(letter, number) {
        return this.movementSquares.includes(`${letter.toLowerCase()}${number}`)
    }

    movePiece(movementSquareLetter, movementSquareNumber) {
        const { piecesByLocationDiff } = this.client.movePiece(this.movingLocation, `${movementSquareLetter.toLowerCase()}${movementSquareNumber}`, this.getPieceByLocationString(this.movingLocation));

        this.updatePiecesLocations(piecesByLocationDiff);
        this.unColorMovementOptions();
    }
}