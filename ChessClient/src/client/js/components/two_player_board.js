const React = require('react');

const { Square } = require('./square');

export const PlayerColors = {
    WHITE: "w",
    BLACK: "b",
    GREEN: "g"
}

export function TwoPlayerBoard(props) {
    const players = props.players;
    const player = props.player;
    const piecesByLocation = props.piecesByLocation
    const movementOptions = props.movementOptions
    const movementSquares = props.movementSquares
    const movingLocation = props.movingLocation

    function getPieceByLocationTuple(letter, number) {
        return piecesByLocation[`${letter.toLowerCase()}${number}`];
    }

    function isMovingSquare(letter, number) {
        return movingLocation === `${letter.toLowerCase()}${number}`;
    }

    function isMovementSquare(letter, number) {
        return movementSquares.includes(`${letter.toLowerCase()}${number}`);
    }

    return (
        <div className="board">
            {
                Array.from({ length: 64 }, (_, index) => {
                    const number = player.color === PlayerColors.WHITE ? 8 - Math.floor(index / 8) : Math.floor(index / 8) + 1;
                    const column = index % 8;
                    const letter = String.fromCharCode(player.color === PlayerColors.WHITE ? ('A'.charCodeAt(0) + column) : ('H'.charCodeAt(0) - column));
                    const isBlack = !(number % 2 === column % 2);
                    const square = (<Square key={index} number={number} letter={letter} index={index} isBlack={isBlack}
                        piece={getPieceByLocationTuple(letter, number)} 
                        isMovingSquare={isMovingSquare(letter, number)}
                        isMovementSquare={isMovementSquare(letter, number)}
                        handleSquareClick={props.handleSquareClick}
                    />)
                    return square
                })
            }
        </div>
    )
}
