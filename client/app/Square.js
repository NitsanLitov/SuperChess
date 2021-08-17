import Pieces from './Pieces.js';

export default class Square {
    constructor({ board, number, letter, isBlack }) {
        this.number = number;
        this.board = board;
        this.letter = letter;
        this.isMovementSquare = false;
        this.element = document.createElement('div');
        this.element.classList.add('square');
        if (isBlack) {
            this.element.classList.add('black');
        }
        this.element.setAttribute('number', number);
        this.element.setAttribute('letter', letter);
        this.element.onclick = this.onClick.bind(this);
        this.update();
    }

    update() {
        this.element.innerHTML = '';
        const currentPiece = this.board.getPieceByLocationTuple(this.letter, this.number);
        if (currentPiece) {
            const imageUrl = Pieces[`${currentPiece.color}${currentPiece.type}`];
            if (imageUrl) {
                const image = new Image();
                image.src = imageUrl;
                this.element.append(image);
            } else {
                this.element.textContent = currentPiece.type;
            }
        }

        this.isMovementSquare = this.board.isMovementSquare(this.letter, this.number);
        if (this.isMovementSquare) {
            this.element.classList.add('movement');
        } else {
            this.element.classList.remove('movement');
        }

        if (this.number === 1) {
            const label = document.createElement('span');
            label.classList.add('letter-label');
            label.textContent = this.letter;
            this.element.append(label);
        }

        if (this.letter === 'A') {
            const label = document.createElement('span');
            label.classList.add('number-label');
            label.textContent = this.number;
            this.element.append(label);
        }
    }

    onClick() {
        if (this.isMovementSquare) {
            this.board.movePiece(this.letter, this.number);
        } else this.board.colorMovementOptions(this.letter, this.number);
    }
}