using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players;

namespace ChessBoard
{
    class Bishop : ChessPiece
    {
        public Bishop((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = new List<(char, int)>();
            List<List<(char, int)>> diagMovementOptions = this.movementBoard.Diagonal(this.location);

            foreach (List<(char,int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction));
                
            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}