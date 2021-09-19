import { PieceImage } from './piece_image';

const React = require('react');
const { useState, useEffect } = React

export function Square(props) {
    const number = props.number;
    const letter = props.letter;
    const index = props.index;
    const isBlack = props.isBlack;
    const piece = props.piece;
    const isMovingSquare = props.isMovingSquare;
    const isMovementSquare = props.isMovementSquare;
    const [classes, setClasses] = useState("")

    useEffect(() => {
        let c = "square"
        if (isBlack) {c += " black"}
        if (isMovingSquare) {c += " moving"}
        if (isMovementSquare) {c += " movement"}
        setClasses(c)
    }, [isMovingSquare, isMovementSquare])

    return (
        <div className={classes} onClick={(e)=> props.handleSquareClick(letter, number, isMovementSquare, isMovingSquare)}>
            {piece && (<PieceImage color={piece.color} type={piece.type}/>)}

            {Math.floor(index / 8) === 7 && (
                <span className="letter-label">{letter}</span>
            )}

            {index % 8 === 0 && (
                <span className="number-label">{number}</span>
            )}
        </div>
    )
}
