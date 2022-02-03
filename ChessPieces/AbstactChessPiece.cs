using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public abstract class ChessPiece
    {
        public bool isFirstMove;
        public (char, int) location;
        public ChessColor color;
        public Board board;
        public MovementBoard movementBoard;
        protected List<(char, int)> movementOptions;

        public ChessPiece((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {
            this.isFirstMove = true;
            this.location = location;
            this.color = color;
            this.board = board;
            this.movementBoard = movementBoard;
            this.movementOptions = new List<(char, int)>();

            if (this.board.GetPieceByLocation(this.location) != null)
                throw new IllegalMoveException("new location isn't empty");

            this.board.SetPieceByLocation(this, this.location);
        }

        public abstract List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing);

        public List<(ChessPiece, (char, int), (char, int))> Move((char, int) newLocation, Type newChessPieceType = null)
        {
            if (!this.movementOptions.Contains(newLocation))
                throw new IllegalMoveException("This move is illegal");

            return this.ForceMove(newLocation, newChessPieceType);
        }

        // For KingWillBeThreatened usuge only
        public List<(ChessPiece, (char, int), (char, int))> ForceMove((char, int) newLocation, Type newChessPieceType = null)
        {
            ChessPiece piece = this.board.GetPieceByLocation(newLocation);

            List<(ChessPiece, (char, int), (char, int))> movedPieces = new List<(ChessPiece, (char, int), (char, int))>();
            
            if (piece != null)
                movedPieces.AddRange(this.TakePiece(piece));

            movedPieces.AddRange(this.MovePieceOnBoardLocation(newLocation, newChessPieceType));

            this.movementOptions.Clear();

            return movedPieces;
        }

        protected internal virtual List<(ChessPiece, (char, int), (char, int))> MovePieceOnBoardLocation((char, int) newLocation, Type newChessPieceType = null)
        {
            if (newChessPieceType != null)
                throw new PawnPromotionException("Can't promote a piece");

            if (this.board.GetPieceByLocation(newLocation) != null)
                throw new IllegalMoveException("new location isn't empty");

            (char, int) oldLocation = this.location;
            
            this.board.SetPieceByLocation(null, this.location);
            this.board.SetPieceByLocation(this, newLocation);

            this.location = newLocation;

            if (this.isFirstMove)
                this.isFirstMove = false;
            
            return new List<(ChessPiece, (char, int), (char, int))>(){(this, oldLocation, newLocation)};
        }

        protected virtual List<(ChessPiece, (char, int), (char, int))> TakePiece(ChessPiece piece)
        {
            return piece.Dispose();
        }

        public virtual List<(ChessPiece, (char, int), (char, int))> Dispose()
        {
            this.board.chessPiecesByColor[this.color].Remove(this);
            this.board.SetPieceByLocation(null, this.location);
            return new List<(ChessPiece, (char, int), (char, int))>(){(this, location, default)};
        }

        protected List<(char, int)> ProcessMoves(List<(char, int)> movementOptions, bool canPieceTakeOpponentKing, bool canTake = true, bool canOnlyTake = false)
        {
            List<(char, int)> finalMovementOptions = new List<(char, int)>();
            foreach ((char, int) movement in movementOptions)
            {
                ChessPiece otherPiece = this.board.GetPieceByLocation(movement);
                bool kingWillBeThreatended = !canPieceTakeOpponentKing && this.board.KingWillBeThreatened(this, movement);

                if (otherPiece != null && (this is Pawn || otherPiece is not EnPassantPawn))
                {
                    if (!kingWillBeThreatended && canTake && otherPiece.color != this.color)
                        finalMovementOptions.Add(movement);
                    break;
                }

                if (canOnlyTake)
                    continue;

                if (!kingWillBeThreatended)
                    finalMovementOptions.Add(movement);
            }
            return finalMovementOptions;
        }
    }

    public class IllegalMoveException : Exception
    {
        public IllegalMoveException() : base() { }
        public IllegalMoveException(string message) : base(message) { }
        public IllegalMoveException(string message, Exception inner) : base(message, inner) { }
    }
}