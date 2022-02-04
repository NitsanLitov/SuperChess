const React = require('react');
const { useState, useEffect } = React

const { TwoPlayerBoard } = require('./two_player_board');
const { GameData } = require('./game_data');
const client = require('../web_client');

export function Board(props) {
    const [socket, setSocket] = useState()
    const [gameEnded, setGameEnded] = useState(false)
    const [gameResult, setGameResult] = useState({})

    const [nickname, setNickname] = useState("")
    const [gameId, setGameId] = useState("")
    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})

    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [movementSquares, setMovementSquares] = useState([])
    const [movingLocation, setMovingLocation] = useState("")
    const [lastMove, setLastMove] = useState([])
    const [waitingForMovingAck, setWaitingForMovingAck] = useState(false)

    function startGame() {
        setSocket(client.connectSocketIo(nickname, gameId, handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange, handleEndGame))
    }

    function handleStartGame(players) {
        setPlayers(players)
        setPlayer(players.find(p => p.nickname === nickname))
    }

    function handleMovedPiecesChange(movedPieces) {
        if (gameEnded) return;

        updatePiecesByLocation(movedPieces);
        setLastMove(movedPieces.at(-1))
    }

    function handleMovementOptionsChange(movementOptions) {
        if (gameEnded) return;

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
        if (gameEnded) return;

        if (waitingForMovingAck) {
            console.log("Waiting for moving ack")
            return
        }

        setWaitingForMovingAck(true)
        client.movePiece(socket, movingLocation, `${newLetter.toLowerCase()}${newNumber}`, response => {
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

    if (players.length === 0) return (
        <div>
            <label>
                Nickname:
                <input type="text" value={nickname} onChange={e => setNickname(e.target.value)} />
            </label>
            <label>
                GameId:
                <input type="text" value={gameId} onChange={e => setGameId(e.target.value)} />
            </label>
            <button disabled={nickname === "" || gameId === ""} onClick={(e) => startGame()}>Start Game</button>
        </div>
    )
    if (gameEnded) return (
        <div>
            {`the result is a "${gameResult.reason}"`}
            {gameResult.nickname && `: "${gameResult.nickname}" lost`}
        </div>
    )
    return (
        <div className="gameBoard">
            <GameData player={player} lastMove={lastMove} isPlayerTurn={Object.keys(movementOptions).length !== 0} />
            {players.length === 2 && (
                <TwoPlayerBoard players={players} player={player} piecesByLocation={piecesByLocation}
                    movementOptions={movementOptions} movementSquares={movementSquares} movingLocation={movingLocation} handleSquareClick={handleSquareClick} />
            )}
        </div>
    )
}
