using System;
using System.Collections.Generic;

using Movement;
using Players;


namespace ChessBoard
{
    public class Queen : ChessPiece
    {
        public Queen((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location), canPieceTakeOpponentKing);
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location), canPieceTakeOpponentKing));

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.Diagonal(this.location);

            foreach (List<(char,int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction, canPieceTakeOpponentKing));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}