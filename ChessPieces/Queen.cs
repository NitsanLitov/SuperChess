using System;
using System.Collections.Generic;

using Movement;
using Players;


namespace ChessBoard
{
    class Queen : ChessPiece
    {
        public Queen((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location)));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location)));

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.Diagonal(this.location);

            foreach (List<(char,int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}