using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players;


namespace ChessBoard
{
    class King : ChessPiece
    {
        public King((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, 1));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location, 1)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location, 1)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location, 1)));

            this.movementOptions = movementOptions;
            return movementOptions;
        }
    }
}