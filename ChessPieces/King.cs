using System;
using System.Collections.Generic;

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

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.Diagonal(this.location, 1);

            foreach (List<(char, int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction));

            if (this.isFirstMove && !this.board.IsKingThreatened(this.color))
            {
                List<(char, int)> rightCastle = GetCastling(this.movementBoard.Right(this.location));
                List<(char, int)> leftCastle = GetCastling(this.movementBoard.Left(this.location));
                
                if (rightCastle != null && !this.board.KingWillBeThreatened(this, rightCastle[0])) movementOptions.AddRange(rightCastle);
                if (leftCastle != null && !this.board.KingWillBeThreatened(this, leftCastle[0])) movementOptions.AddRange(leftCastle);
            }

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }

        private List<(char, int)> GetCastling(List<(char, int)> movementOptions)
        {
            if (movementOptions.Count < 2) return null;

            // validate no pieces between
            for (int i = 0; i < movementOptions.Count - 1; i++)
            {
                (char, int) location = movementOptions[i];
                if (this.board.GetPieceByLocation(location) != null)
                    return null;
            }

            // validate piece is rook and it haven't moved
            (char, int) otherPieceLocation = movementOptions[movementOptions.Count - 1];
            ChessPiece otherPiece = this.board.GetPieceByLocation(otherPieceLocation);
            if (!(otherPiece is Rook) || !otherPiece.isFirstMove)
                return null;

            // return step 2 movement
            return new List<(char, int)>() { movementOptions[1] };
        }
    }
}