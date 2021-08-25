using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players;

namespace ChessBoard
{
    class Pawn : ChessPiece
    {
        public Pawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, this.isFirstMove ? 2 : 1));
            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}