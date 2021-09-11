using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public class Rook : ChessPiece
    {
        public Rook((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location), canPieceTakeOpponentKing);
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location), canPieceTakeOpponentKing));
            
            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}