const React = require('react');
const { useState, useEffect } = React

const { TwoPlayerBoard } = require('./two_player_board');
const { GameData } = require('./game_data');
const client = require('../web_client');

export function Board(props) {
    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})
    const [nickname, setNickname] = useState("Nitsan")
    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [movementSquares, setMovementSquares] = useState([])
    const [movingLocation, setMovingLocation] = useState("")
    const [lastMove, setLastMove] = useState([])
    const [waitingForMovingAck, setWaitingForMovingAck] = useState(false)
    const [socket, setSocket] = useState()

    useEffect(() => {
        setSocket(client.connectSocketIo(nickname, handleStartGame, handleMovedPiecesChange, handleMovementOptionsChange))
    }, [])

    function handleStartGame(players) {
        setPlayers(players)
        setPlayer(players[0])
    }

    function handleMovedPiecesChange(movedPieces) {
        updatePiecesByLocation(movedPieces);
        setLastMove(movedPieces.at(-1))
    }

    function handleMovementOptionsChange(movementOptions) {
        setMovementOptions(movementOptions);
    }

    function updatePiecesByLocation(movedPieces) {
        setPiecesByLocation(prevPiecesByLocation => {
            movedPieces.forEach(movedPiece => {
                prevPiecesByLocation[movedPiece[1]] = null
                prevPiecesByLocation[movedPiece[2]] = movedPiece[0]
            });
            return prevPiecesByLocation
        })
    }

    function handleSquareClick(letter, number, isMovementSquare, isMovingSquare) {
        if (isMovementSquare) {
            movePiece(letter, number)
            return;
        }
        if (isMovingSquare) {
            unColorSquares();
            return;
        }

        // Color movement squares
        const localMovingLocation = `${letter.toLowerCase()}${number}`
        const localMovementSquares = localMovingLocation in movementOptions ? movementOptions[localMovingLocation] : []
        setMovementSquares(localMovementSquares);
        setMovingLocation(localMovementSquares.length !== 0 ? localMovingLocation : "");
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

    if (players.length === 0) return <div></div>
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
