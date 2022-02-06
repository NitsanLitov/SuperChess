const React = require('react');
const { useState, useRef } = React

const { BoardCommunication } = require('./board_communication');

export function BoardData(props) {
    const [gameEnded, setGameEnded] = useState(false)
    const [gameResult, setGameResult] = useState({})
    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [lastMove, setLastMove] = useState([])

    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})
    const [finalNickname, setFinalNickname] = useState("")
    
    const nicknameRef = useRef();
    nicknameRef.current = finalNickname;

    const gameEndedRef = useRef();
    gameEndedRef.current = gameEnded;

    function handleStartGame(playingPlayers) {
        setPlayer(playingPlayers.find(p => p.nickname === nicknameRef.current))
        setPlayers(playingPlayers)
    }

    function handleMovedPiecesChange(movedPieces) {
        if (gameEndedRef.current) return;

        updatePiecesByLocation(movedPieces);
        setLastMove(movedPieces.at(-1))
    }

    function handleMovementOptionsChange(movementOptions) {
        if (gameEndedRef.current) return;

        setMovementOptions(movementOptions);
    }

    function handleRefreshGame(data) {
        console.log("Refreshing")
        setFinalNickname(data['nickname'])
        // setGameId(data['gameId'])
        if ('players' in data) {
            handleStartGame(data['players'], data['nickname'])
            if ('movedPieces' in data) handleMovedPiecesChange(data['movedPieces'])
            if ('movementOptions' in data) handleMovementOptionsChange(data['movementOptions'])
        }
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
            players={players}
            player={player}
            finalNickname={finalNickname}
            setFinalNickname={setFinalNickname}
            handleStartGame={handleStartGame}
            handleMovedPiecesChange={handleMovedPiecesChange}
            handleMovementOptionsChange={handleMovementOptionsChange}
            handleRefreshGame={handleRefreshGame}
            handleEndGame={handleEndGame}
            gameEnded={gameEnded}
            gameResult={gameResult}
            piecesByLocation={piecesByLocation}
            movementOptions={movementOptions}
            lastMove={lastMove}
            clearMovementOptions={() => setMovementOptions([])}
        />
    )
}
