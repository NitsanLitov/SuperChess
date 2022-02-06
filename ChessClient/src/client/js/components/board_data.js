const React = require('react');
const { useState, useRef } = React

const { BoardCommunication } = require('./board_communication');

export function BoardData(props) {
    const [gameEnded, setGameEnded] = useState(false)
    const [gameResult, setGameResult] = useState({})
    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [lastMove, setLastMove] = useState([])

    const gameEndedRef = useRef();
    gameEndedRef.current = gameEnded;

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

    return (
        <BoardCommunication
            handleMovedPiecesChange={handleMovedPiecesChange}
            handleMovementOptionsChange={handleMovementOptionsChange}
            handleEndGame={handleEndGame}
            gameEnded={gameEnded}
            gameResult={gameResult}
            piecesByLocation={piecesByLocation}
            movementOptions={movementOptions}
            lastMove={lastMove}
            clearMovementOptions={()=>setMovementOptions([])}
        />
    )
}
