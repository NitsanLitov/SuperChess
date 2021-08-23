import Square from './Square.js';
import Client from './Client.js';
import GameData from './GameData.js';

export default class Board {
    constructor({ selector }) {
        this.client = new Client("127.0.0.1", 54321);
        this.squares = [];
        this.movementOptions = {};
        this.movementSquares = [];
        this.movingLocation = "";

        this.element = document.querySelector(selector);
        this.element.classList.add('gameBoard');
    }

    async startGame() {
        const { playerColor, startingPiecesByLocation, players } = this.client.startGame();
        this.playerColor = playerColor;

        if (players === 2) {
            this.initTwoPlayers();
        }
        this.setStartingPiecesLocations(startingPiecesByLocation);

        this.startNextTurn();
    }

    async startNextTurn() {
        this.setMovementOptions({});
        this.unColorMovementOptions();
        let movedPieces, movementOptions;
        do {
            console.log("waiting...")
            const result = await this.client.getMovementOptions();
            [movedPieces, movementOptions] = [result.movedPieces, result.movementOptions];
            this.updatePiecesLocations(movedPieces);
        }
        while (Object.keys(movementOptions).length === 0);
        this.setMovementOptions(movementOptions);
        this.gameData.updateTurn(true);
    }

    initTwoPlayers() {
        const size = '90vmin';
        this.element.style.width = size;
        this.element.style.height = size;

        this.gameData = new GameData({ board: this });
        this.gameData;
        this.element.appendChild(this.gameData.element);

        this.boardElement = document.createElement('div');
        this.boardElement.classList.add('board');
        this.element.appendChild(this.boardElement);

        this.squaresByLocation = new Map();
        this.piecesByLocation = new Map();

        this.squares = Array.from({ length: 64 }, (_, index) => {
            const number = this.playerColor === this.client.Colors.WHITE ? 8 - Math.floor(index / 8) : Math.floor(index / 8) + 1;
            const column = index % 8;
            const letter = String.fromCharCode(this.playerColor === this.client.Colors.WHITE ? ('A'.charCodeAt(0) + column) : ('H'.charCodeAt(0) - column));
            const isBlack = !(number % 2 === column % 2);
            const square = new Square({
                board: this,
                number,
                letter,
                isBlack,
                index
            });
            this.squaresByLocation.set(`${letter.toLowerCase()}${number}`, square);
            this.piecesByLocation.set(`${letter.toLowerCase()}${number}`, null);
            this.boardElement.appendChild(square.element);
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

    updatePiecesLocations(movedPieces) {
        movedPieces.forEach(movedPiece => {
            const [oldLocation, newLocation, piece] = movedPiece;
            this.gameData.updateLastMove(oldLocation, newLocation, piece)

            this.piecesByLocation.set(oldLocation, null);
            this.piecesByLocation.set(newLocation, piece);
            this.getSquareByLocationString(oldLocation).update();
            this.getSquareByLocationString(newLocation).update();
        });
    }

    setStartingPiecesLocations(piecesByLocation) {
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
        const movingLocation = `${letter.toLowerCase()}${number}`
        this.movementSquares = this.getMovementOptionsByLocationString(movingLocation);
        this.movingLocation = this.movementSquares.length !== 0 ? movingLocation : "";
        this.updateAllSquares()
    }

    unColorMovementOptions() {
        this.movingLocation = '';
        this.movementSquares = [];
        this.updateAllSquares()
    }

    isMovementSquare(letter, number) {
        return this.movementSquares.includes(`${letter.toLowerCase()}${number}`);
    }

    isMovingSquare(letter, number) {
        return this.movingLocation === `${letter.toLowerCase()}${number}`;
    }

    movePiece(movementSquareLetter, movementSquareNumber) {
        const newLocation = `${movementSquareLetter.toLowerCase()}${movementSquareNumber}`
        const { movedPieces } = this.client.movePiece(this.movingLocation, newLocation, this.getPieceByLocationString(this.movingLocation));

        this.gameData.updateTurn(false);

        // ToDo: Supposed to be in GetMovementOptions
        this.updatePiecesLocations(movedPieces);

        this.startNextTurn();
    }
}