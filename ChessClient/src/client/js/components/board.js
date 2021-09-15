const React = require('react');
const { useState, useEffect } = React

const { TwoPlayerBoard } = require('./two_player_board');
const { GameData } = require('./game_data');
const client = require('../web_client');

export function Board(props) {
    const [players, setPlayers] = useState([])
    const [player, setPlayer] = useState({})
    const [piecesByLocation, setPiecesByLocation] = useState({})
    const [movementOptions, setMovementOptions] = useState([])
    const [movementSquares, setMovementSquares] = useState([])
    const [movingLocation, setMovingLocation] = useState("")
    const [lastMove, setLastMove] = useState([])

    const [testingNumber, setTestingNumber] = useState(1)

    useEffect(() => {
        const game = client.startGame()
        const startingPieces = game.startingPieces
        setPlayers(game.players)

        setPlayer(game.players[0])

        updatePiecesByLocation(startingPieces);

        getUpdates()
    }, [])

    function updatePiecesByLocation(movedPieces) {
        setPiecesByLocation(prevPiecesByLocation => {
            movedPieces.forEach(movedPiece => {
                prevPiecesByLocation[`${movedPiece[1][0]}${movedPiece[1][1]}`] = null
                prevPiecesByLocation[`${movedPiece[2][0]}${movedPiece[2][1]}`] = movedPiece[0]
            });
            return prevPiecesByLocation
        })
    }

    function getMovementOptionsByLocationString(locationString) {
        if (locationString in movementOptions) return movementOptions[locationString];
        return [];
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
        const localMovementSquares = getMovementOptionsByLocationString(localMovingLocation)
        setMovementSquares(localMovementSquares);
        setMovingLocation(localMovementSquares.length !== 0 ? localMovingLocation : "");
    }

    function unColorSquares() {
        setMovingLocation('');
        setMovementSquares([]);
    }

    function movePiece(newLetter, newNumber) {
        const newLocation = `${newLetter.toLowerCase()}${newNumber}`

        if (client.movePiece(movingLocation, newLocation)) {
            setMovementOptions([])
            unColorSquares();

            getUpdates()
        }
        else {
            console.log("illegal move");
        }
    }

    async function getUpdates() {
        let test = testingNumber

        while (true) {
            console.log("waiting for results")
            const result = await client.getMovementOptions(test);
            test += 1
            console.log("got results")

            if (result.movedPieces.length !== 0) {
                updatePiecesByLocation(result.movedPieces);
                setLastMove(result.movedPieces.at(-1))
            }
            if (Object.keys(result.movementOptions).length !== 0) {
                setMovementOptions(result.movementOptions);
                setTestingNumber(test)
                return;
            }
        }
    }

    if (!player) return <div></div>
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
