using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    class Knight : ChessPiece
    {
        public Knight((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> movementOptions = new List<(char, int)>();
            List<List<(char, int)>> knightMovementOptions = this.movementBoard.Knight(this.location);

            foreach (List<(char,int)> direction in knightMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }
    }
}