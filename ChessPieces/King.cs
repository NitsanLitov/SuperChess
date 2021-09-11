using System;
using System.Linq;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public class King : ChessPiece
    {
        protected Dictionary<(char, int), (Rook, string)> castlingMovementOptions;
        public King((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, 1), canPieceTakeOpponentKing);
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Down(this.location, 1), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Left(this.location, 1), canPieceTakeOpponentKing));
            movementOptions.AddRange(this.ProcessMoves(this.movementBoard.Right(this.location, 1), canPieceTakeOpponentKing));

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.Diagonal(this.location, 1);

            foreach (List<(char, int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction, canPieceTakeOpponentKing));

            if (!canPieceTakeOpponentKing && this.isFirstMove && !this.board.IsKingThreatened(this.color))
            {
                this.castlingMovementOptions = new Dictionary<(char, int), (Rook, string)>();

                this.AddCastlingMovement(this.movementBoard.Right(this.location), "right");
                this.AddCastlingMovement(this.movementBoard.Left(this.location), "left");
                
                movementOptions.AddRange(castlingMovementOptions.Keys.ToList());
            }

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }

        protected internal override void MovePieceOnBoardLocation((char, int) newLocation, Type newChessPieceType = null)
        {            
            base.MovePieceOnBoardLocation(newLocation);
            
            if (this.castlingMovementOptions == null || !this.castlingMovementOptions.Keys.Contains(newLocation))
                return;

            (Rook rook, string kingDirection) = this.castlingMovementOptions[newLocation];

            (char, int) rookNewLocation;
            if (kingDirection == "right")
                rookNewLocation = this.movementBoard.Left(newLocation, 1)[0];
            else
                rookNewLocation = this.movementBoard.Right(newLocation, 1)[0];
            
            rook.MovePieceOnBoardLocation(rookNewLocation);

            this.castlingMovementOptions.Clear();
        }

        private void AddCastlingMovement(List<(char, int)> movementOptions, string direction)
        {
            if (movementOptions.Count < 2) return;

            // validate no pieces between
            for (int i = 0; i < movementOptions.Count - 1; i++)
            {
                (char, int) location = movementOptions[i];
                if (this.board.GetPieceByLocation(location) != null)
                    return;
            }

            // validate piece is rook and it haven't moved
            (char, int) otherPieceLocation = movementOptions[movementOptions.Count - 1];
            ChessPiece otherPiece = this.board.GetPieceByLocation(otherPieceLocation);
            if (!(otherPiece is Rook) || !otherPiece.isFirstMove)
                return;

            // validate no king threats in between
            if (this.board.KingWillBeThreatened(this, movementOptions[0])) return;

            // add step 2 movement (before the second KingWillBeThreatened so the castling move will be possible at move function)
            this.castlingMovementOptions[movementOptions[1]] = ((Rook)otherPiece, direction);

            // if king will be threatened after castling, remove option
            if (this.board.KingWillBeThreatened(this, movementOptions[1])) this.castlingMovementOptions.Remove(movementOptions[1]);
        }
    }
}