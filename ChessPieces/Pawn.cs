using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public class Pawn : ChessPiece
    {
        // First value represent the enPassant location. Second value represent the real pawn location.
        private ((char, int), (char, int)) enPassantMove;

        public Pawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            this.movementOptions.Clear();
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, this.isFirstMove ? 2 : 1), canPieceTakeOpponentKing, false);

            this.enPassantMove = default;

            if (movementOptions.Count == 2)
                this.enPassantMove = (movementOptions[0], movementOptions[1]);

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.DiagonalUp(this.location, 1);

            foreach (List<(char, int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction, canPieceTakeOpponentKing, true, true));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }

        protected override List<(ChessPiece, (char, int), (char, int))> TakePiece(ChessPiece piece)
        {
            List<(ChessPiece, (char, int), (char, int))> movedPieces = new List<(ChessPiece, (char, int), (char, int))>();

            if (piece is EnPassantPawn)
                movedPieces.AddRange(((EnPassantPawn)piece).OriginalPawn.Dispose());
            movedPieces.AddRange(base.TakePiece(piece));

            return movedPieces;
        }

        protected internal override List<(ChessPiece, (char, int), (char, int))> MovePieceOnBoardLocation((char, int) newLocation, Type newChessPieceType = null)
        {
            List<(ChessPiece, (char, int), (char, int))> movedPieces = new List<(ChessPiece, (char, int), (char, int))>();

            // If there's no up option then the pawn reached top row
            bool topRow = this.movementBoard.Up(newLocation).Count == 0;

            if (this.movementOptions.Contains(newLocation))
            {
                if (topRow)
                {
                    if (newChessPieceType == null)
                        throw new PawnPromotionException("Pawn reached top row and haven't recived promotion piece type");
                
                    if (newChessPieceType != typeof(Queen) && newChessPieceType != typeof(Knight) && newChessPieceType != typeof(Bishop) && newChessPieceType != typeof(Rook))
                        throw new PawnPromotionException("promotion piece type must be either Queen, Knight, Bishop or Rook");

                    movedPieces.AddRange(this.Dispose());
                    
                    ChessPiece newChessPiece = Activator.CreateInstance(newChessPieceType, new object[] { newLocation, this.color, this.board, this.movementBoard }) as ChessPiece;
                    newChessPiece.isFirstMove = false;
                    this.board.chessPiecesByColor[this.color].Add(newChessPiece);
                    movedPieces.Add((newChessPiece, this.location, newLocation));
                    
                    return movedPieces;
                }

                if (newChessPieceType != null)
                    throw new PawnPromotionException("Pawn haven't reached top row and can't perform promotion");
            }

            movedPieces.AddRange(base.MovePieceOnBoardLocation(newLocation));

            if (newLocation == this.enPassantMove.Item2)
            {
                EnPassantPawn enPassantPawn = new EnPassantPawn(this.enPassantMove.Item1, this);
                this.board.enPassantPawn = enPassantPawn;
                this.enPassantMove = default;
            }

            return movedPieces;
        }
    }

    public class PawnPromotionException : Exception
    {
        public PawnPromotionException() : base() { }
        public PawnPromotionException(string message) : base(message) { }
        public PawnPromotionException(string message, Exception inner) : base(message, inner) { }
    }
}