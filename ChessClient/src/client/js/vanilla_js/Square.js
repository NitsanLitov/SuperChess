import PieceImage from './PieceImage.js';

export default class Square {
    constructor({ board, number, letter, isBlack, index }) {
        this.number = number;
        this.board = board;
        this.letter = letter;
        this.index = index;
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
            const imageUrl = PieceImage(currentPiece);
            if (imageUrl) {
                const image = new Image();
                image.src = imageUrl;
                this.element.append(image);
            } else {
                this.element.textContent = currentPiece.type;
            }
        }

        this.isMovementSquare = this.board.isMovementSquare(this.letter, this.number);
        this.isMovingSquare = this.board.isMovingSquare(this.letter, this.number);

        if (this.isMovingSquare) {
            this.element.classList.add('moving');
        } else {
            this.element.classList.remove('moving');
        }

        if (this.isMovementSquare) {
            this.element.classList.add('movement');
        } else {
            this.element.classList.remove('movement');
        }

        if (Math.floor(this.index / 8) === 7) {
            const label = document.createElement('span');
            label.classList.add('letter-label');
            label.textContent = this.letter;
            this.element.append(label);
        }

        if (this.index % 8 === 0) {
            const label = document.createElement('span');
            label.classList.add('number-label');
            label.textContent = this.number;
            this.element.append(label);
        }
    }

    onClick() {
        if (this.isMovementSquare) {
            this.board.movePiece(this.letter, this.number);
            return;
        }
        if (this.isMovingSquare) {
            this.board.unColorMovementOptions();
            return;
        }
        this.board.colorMovementOptions(this.letter, this.number);
    }
}