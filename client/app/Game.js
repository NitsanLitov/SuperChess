import Board from './Board.js';
import Client from './Client.js';

export default class Game {
    constructor() {
        this.client = new Client("127.0.0.1", 54321);
        this.startGame()
    }

    async startGame() {
        const { playerColor, startingPiecesByLocation, players } = this.client.startGame();
        this.playerColor = playerColor;

        const board = new Board({
            selector: '#board',
            playerColor,
            startingPiecesByLocation,
            players
        });
    }
}