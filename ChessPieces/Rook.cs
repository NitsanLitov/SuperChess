using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players;

namespace ChessBoard
{
    class Rook : ChessPiece
    {
        public Rook((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location)));
            
            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}