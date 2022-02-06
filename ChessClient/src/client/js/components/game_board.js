const React = require('react');
const { useState, useEffect } = React

const { TwoPlayerBoard } = require('./two_player_board');
const { GameData } = require('./game_data');

export function GameBoard(props) {
    const gameEnded = props.gameEnded
    const gameResult = props.gameResult
    const piecesByLocation = props.piecesByLocation
    const movementOptions = props.movementOptions
    const lastMove = props.lastMove

    const players = props.players
    const player = props.player

    const [movementSquares, setMovementSquares] = useState([])
    const [movingLocation, setMovingLocation] = useState("")
    const [waitingForMovingAck, setWaitingForMovingAck] = useState(false)

    const movePieceOnServer = props.movePiece;

    useEffect(() => {
        if (gameEnded === true) unColorSquares();
    }, [gameEnded]);

    function handleSquareClick(letter, number, isMovementSquare, isMovingSquare) {
        if (gameEnded) return;

        if (isMovementSquare) {
            movePiece(letter, number)
            return;
        }
        if (isMovingSquare) {
            unColorSquares();
            return;
        }

        // Color movement squares
        const localMovingLocation = `${letter.toLowerCase()}${number}`;

        if (!(localMovingLocation in movementOptions) || movementOptions[localMovingLocation].length === 0) {
            unColorSquares();
            return;
        }
        const localMovementSquares = movementOptions[localMovingLocation]
        setMovementSquares(localMovementSquares);
        setMovingLocation(localMovingLocation);
    }

    function unColorSquares() {
        setMovingLocation('');
        setMovementSquares([]);
    }

    function movePiece(newLetter, newNumber) {
        if (waitingForMovingAck) {
            console.log("Waiting for moving ack")
            return
        }

        setWaitingForMovingAck(true)
        movePieceOnServer(movingLocation, `${newLetter.toLowerCase()}${newNumber}`, response => {
            if (response === true) {
                props.clearMovementOptions()
                unColorSquares();
            }
            else {
                console.log("illegal move");
            }
            setWaitingForMovingAck(false)
        });
    }

    const twoPlayerBoard = (<TwoPlayerBoard players={players} player={player}
        piecesByLocation={piecesByLocation} movementOptions={movementOptions}
        movementSquares={movementSquares} movingLocation={movingLocation} handleSquareClick={handleSquareClick} />
    )

    if (players.length === 0) return (<div></div>)

    return (
        <div className="gameBoard">
            <GameData player={player} lastMove={lastMove} isPlayerTurn={Object.keys(movementOptions).length !== 0} gameEnded={gameEnded} gameResult={gameResult} />
            {players.length === 2 && twoPlayerBoard}
        </div>
    )
}
