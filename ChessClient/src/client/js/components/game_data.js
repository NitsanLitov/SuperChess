const React = require('react');

const { PieceImage } = require('./piece_image');

export function GameData(props) {
    const player = props.player;
    const lastMove = props.lastMove;
    const isPlayerTurn = props.isPlayerTurn;
    const gameEnded = props.gameEnded;
    const gameResult = props.gameResult;

    let moveData = (<div className="moveData"></div>)
    if (lastMove.length !== 0) {
        moveData = (
            <div className="moveData">
                <PieceImage color={lastMove[0].color} type={lastMove[0].type} />
                {`${lastMove[1]} moved to ${lastMove[2]}`}
            </div>
        )
    }

    let message;
    let positive;
    if (gameEnded) {
        positive = false;
        message = `${gameResult.reason}`;

        if (gameResult.nickname) {
            message += `: ${gameResult.nickname} lost`;
            positive = gameResult.nickname !== player.nickname;
        }
    }
    else {
        if (isPlayerTurn) {
            positive = true;
            message = 'Your turn';
        }
        else {
            positive = false;
            message = 'Please wait for your turn';
        }
    }

    let turnData = (
        <div className={`turnData ${positive ? 'positive' : 'negative'}`}>{message}</div>
    );

    return (
        <div className="gameData">
            {moveData}
            {turnData}
        </div>
    )
}
