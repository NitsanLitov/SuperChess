import Pieces from './Pieces.js';

export default class Square {
    constructor({ board, number, letter, isBlack }) {
        this.number = number;
        this.board = board;
        this.letter = letter;
        this.element = document.createElement('div');
        this.element.classList.add('square');
        if (isBlack) {
            this.element.classList.add('black');
        }
        this.element.setAttribute('number', number);
        this.element.setAttribute('letter', letter);
        this.update();
    }

    update() {
        this.element.innerHTML = '';
        const current = this.board.getPieceByLocation(this.letter, this.number);
        if (current) {
            const imageUrl = Pieces[`${current.color}${current.type}`];
            if (imageUrl) {
                const image = new Image();
                image.src = imageUrl;
                this.element.append(image);
            } else {
                this.element.textContent = current.type;
            }
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
}