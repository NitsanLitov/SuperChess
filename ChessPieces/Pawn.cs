using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    class Pawn : ChessPiece
    {
        public Pawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, this.isFirstMove ? 2 : 1), canPieceTakeOpponentKing, false);

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.DiagonalUp(this.location, 1);

            foreach (List<(char,int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction, canPieceTakeOpponentKing, true, true));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}