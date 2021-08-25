using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players; // for the ChessColor

namespace ChessBoard
{
    abstract class ChessPiece
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

            this.board.SetPieceByLocation(this, this.location);
        }

        public abstract List<(char, int)> GetMovementOptions();
        public void Move((char, int) newLocation)
        {
            if (!this.movementOptions.Contains(newLocation))
                throw new ArgumentException("This move is illegal");

            ChessPiece piece = this.board.GetPieceByLocation(newLocation);

            if (piece != null)
                this.board.chessPiecesByColor[piece.color].Remove(piece);

            this.board.SetPieceByLocation(this, newLocation);
            this.board.SetPieceByLocation(null, this.location);
            this.location = newLocation;
        }

        protected List<(char, int)> ProcessMoves(List<(char, int)> moves, bool canTake = true)
        {
            List<(char, int)> movesOptions = new List<(char, int)>();
            foreach ((char, int) move in moves)
            {
                ChessPiece tempPiece = this.board.GetPieceByLocation(move);
                bool kingWillBeThreatended = this.board.KingWillBeThreatened(tempPiece, move);

                if (tempPiece != null)
                {
                    if (!kingWillBeThreatended && canTake && tempPiece.color != this.color)
                        movesOptions.Add(move);
                    break;
                }
                if (!kingWillBeThreatended)
                    movesOptions.Add(move);
            }
            return movesOptions;
        }
    }
}