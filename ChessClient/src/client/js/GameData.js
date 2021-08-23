import PieceImage from './PieceImage.js';

export default class GameData {
    constructor({ board }) {
        this.board = board;
        this.playerColor = this.board.playerColor;
        this.lastMove = [];
        this.isPlayerTurn = false;
        this.element = document.createElement('div');
        this.element.classList.add('gameData');
        this.element.setAttribute('playerColor', this.playerColor);

        this.turnElement = document.createElement('div');
        this.moveElement = document.createElement('div');
        this.turnElement.classList.add('turnData');
        this.moveElement.classList.add('moveData');
        this.element.appendChild(this.moveElement);
        this.element.appendChild(this.turnElement);

        this.update();
    }

    update() {
        this.turnElement.innerHTML = '';
        this.moveElement.innerHTML = '';

        if (this.lastMove.length !== 0) {
            const imageUrl = PieceImage(this.lastMove[2]);
            if (imageUrl) {
                const image = new Image();
                image.src = imageUrl;
                this.moveElement.append(image);
            }
            this.moveElement.innerHTML += `${this.lastMove[0]} moved to ${this.lastMove[1]}`;
        }
        this.turnElement.innerHTML = this.isPlayerTurn ? "Your turn" : "Please wait for your turn";
        this.turnElement.classList.remove('positive');
        this.turnElement.classList.remove('negative');
        this.turnElement.classList.add(this.isPlayerTurn ? "positive" : "negative");
    }

    updateLastMove(oldLocation, newLocation, movedPiece) {
        this.lastMove = [oldLocation, newLocation, movedPiece];
        this.update()
    }

    updateTurn(isPlayerTurn) {
        this.isPlayerTurn = isPlayerTurn;
        this.update()
    }
}