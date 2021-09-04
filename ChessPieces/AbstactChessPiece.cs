using System;
using System.Collections.Generic;

using Movement;
using Players;

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
            this.movementOptions = new List<(char, int)>();

            this.board.SetPieceByLocation(this, this.location);
        }

        public abstract List<(char, int)> GetMovementOptions();
        
        public void Move((char, int) newLocation)
        {
            if (!this.movementOptions.Contains(newLocation))
                throw new ArgumentException("This move is illegal");
            
            this.ForceMove(newLocation);
        }
        
        // For KingWillBeThreatened usuge only
        public void ForceMove((char, int) newLocation)
        {
            ChessPiece piece = this.board.GetPieceByLocation(newLocation);

            if (piece != null)
                this.board.chessPiecesByColor[piece.color].Remove(piece);

            this.MovePieceOnBoardLocation(newLocation);

            this.movementOptions.Clear();
        }

        protected internal virtual void MovePieceOnBoardLocation((char, int) newLocation)
        {
            this.board.SetPieceByLocation(this, newLocation);
            this.board.SetPieceByLocation(null, this.location);
            
            this.location = newLocation;
            
            if (this.isFirstMove)
                this.isFirstMove = false;
        }
      
        protected List<(char, int)> ProcessMoves(List<(char, int)> movementOptions, bool canTake = true, bool canOnlyTake = false)
        {
            List<(char, int)> finalMovementOptions = new List<(char, int)>();
            foreach ((char, int) movement in movementOptions)
            {
                ChessPiece otherPiece = this.board.GetPieceByLocation(movement);
                bool kingWillBeThreatended = this.board.KingWillBeThreatened(this, movement);

                if (otherPiece != null)
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
}