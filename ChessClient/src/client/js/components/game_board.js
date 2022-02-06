const React = require('react');
const { useState, useEffect, useRef } = React

const { TwoPlayerBoard } = require('./two_player_board');
const { GameData } = require('./game_data');

export function Board(props) {
    const [gameEnded, setGameEnded] = useState(false)
    const [gameResult, setGameResult] = useState({})

    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [movementSquares, setMovementSquares] = useState([])
    const [movingLocation, setMovingLocation] = useState("")
    const [lastMove, setLastMove] = useState([])
    const [waitingForMovingAck, setWaitingForMovingAck] = useState(false)

    const players = props.players
    const player = props.player

    const gameEndedRef = useRef();
    gameEndedRef.current = gameEnded;

    const connectToServer = props.connectToServer;
    const movePieceOnServer = props.movePiece;

    useEffect(() => {
        connectToServer(handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame)
    }, []);

    function handleMovedPiecesChange(movedPieces) {
        if (gameEndedRef.current) return;

        updatePiecesByLocation(movedPieces);
        setLastMove(movedPieces.at(-1))
    }

    function handleMovementOptionsChange(movementOptions) {
        if (gameEndedRef.current) return;

        setMovementOptions(movementOptions);
    }

    function handleEndGame(result) {
        setGameEnded(true);
        unColorSquares();
        setMovementOptions([]);
        setGameResult(result);
    }

    function updatePiecesByLocation(movedPieces) {
        setPiecesByLocation(prevPiecesByLocation => {
            movedPieces.forEach(movedPiece => {
                prevPiecesByLocation[movedPiece[1]] = null
                if (movedPiece[2] !== '') prevPiecesByLocation[movedPiece[2]] = movedPiece[0]
            });
            return prevPiecesByLocation
        })
    }

    function handleSquareClick(letter, number, isMovementSquare, isMovingSquare) {
        if (gameEndedRef.current) return;

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
        if (gameEndedRef.current) return;

        if (waitingForMovingAck) {
            console.log("Waiting for moving ack")
            return
        }

        setWaitingForMovingAck(true)
        movePieceOnServer(movingLocation, `${newLetter.toLowerCase()}${newNumber}`, response => {
            if (response === true) {
                setMovementOptions([])
                unColorSquares();
            }
            else {
                console.log("illegal move");
            }
            setWaitingForMovingAck(false)
        });
    }

    if (players.length === 0) return (<div></div>)

    return (
        <div className="gameBoard">
            <GameData player={player} lastMove={lastMove} isPlayerTurn={Object.keys(movementOptions).length !== 0} gameEnded={gameEnded} gameResult={gameResult} />
            {players.length === 2 && (
                <TwoPlayerBoard players={players} player={player} piecesByLocation={piecesByLocation}
                    movementOptions={movementOptions} movementSquares={movementSquares} movingLocation={movingLocation} handleSquareClick={handleSquareClick} />
            )}
        </div>
    )
}
