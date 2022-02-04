const React = require('react');

const { PieceImage } = require('./piece_image');

export function GameData(props) {
    const player = props.player;
    const lastMove = props.lastMove;
    const isPlayerTurn = props.isPlayerTurn;

    let moveData = (<div className="moveData"></div>)
    if (lastMove.length !== 0) {
        moveData = (
            <div className="moveData">
                <PieceImage color={lastMove[0].color} type={lastMove[0].type} />
                {`${lastMove[1]} moved to ${lastMove[2]}`}
            </div>
        )
    }
    
    
    return (
        <div className="gameData">
            {moveData}

            {isPlayerTurn
                && (<div className="turnData positive">Your turn</div>)
                || (<div className="turnData negative">Please wait for your turn</div>)
            }
        </div>
    )
}
