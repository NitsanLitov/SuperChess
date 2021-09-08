using System;
using System.Collections.Generic;

using Players;

namespace ChessBoard
{
    public class TempBoard
    {
        Board board;
        bool savedState;

        public ChessPiece[,] locationBoard;
        public List<ChessPiece> firstMovePieces;
        Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        Dictionary<ChessPiece, (char, int)> chessPiecesLocation;

        public TempBoard(Board board)
        {
            this.board = board;
            this.SetSavedState(false);
        }

        public void Save()
        {
            this.locationBoard = this.board.LocationBoard.Clone() as ChessPiece[,];
            this.chessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>(this.board.chessPiecesByColor);
            this.chessPiecesLocation = new Dictionary<ChessPiece, (char, int)>();
            this.firstMovePieces = new List<ChessPiece>();

            foreach (ChessColor color in this.chessPiecesByColor.Keys)
            {
                this.chessPiecesByColor[color] = new List<ChessPiece>(this.chessPiecesByColor[color]);
                
                foreach (ChessPiece piece in this.chessPiecesByColor[color])
                {
                    this.chessPiecesLocation[piece] = piece.location;
                    if (piece.isFirstMove)
                    {
                        this.firstMovePieces.Add(piece);
                    }
                }
            }
            
            this.SetSavedState(true);
        }

        public void Reverse()
        {
            if (!this.savedState || this.locationBoard == null || this.chessPiecesByColor == null || this.chessPiecesByColor == null) throw new NoDataSavedException();

            foreach (ChessColor color in this.chessPiecesByColor.Keys)
            {
                foreach (ChessPiece piece in this.chessPiecesByColor[color])
                {
                    if (piece.location != this.chessPiecesLocation[piece]) piece.location = this.chessPiecesLocation[piece];
                        
                    if (this.firstMovePieces.Contains(piece) && !piece.isFirstMove) piece.isFirstMove = true;
                }
            }
            this.board.LocationBoard = this.locationBoard;
            this.board.chessPiecesByColor = this.chessPiecesByColor;

            this.SetSavedState(false);
        }

        private void SetSavedState(bool savedState)
        {
            if (savedState)
            {
                this.savedState = true;
            }
            else
            {
                this.locationBoard = null;
                this.chessPiecesByColor = null;
                this.firstMovePieces = null;
                this.chessPiecesLocation = null;
                this.savedState = false;
            }
        }
    }

    public class NoDataSavedException : Exception {}
}
