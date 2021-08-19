using System;
using System.Collections.Generic;

namespace Board
{
    class TempBoard
    {
        public ChessPiece[,] piecesLocation;
        public List<ChessPiece> firstMovePieces;
        Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        Board board;
        bool savedState;

        public TempBoard(Board board)
        {
            this.board = board;
            this.savedState = false;
        }

        public void Save()
        {
            this.piecesLocation = this.board.piecesLocation.Clone() as ChessPiece[,];
            this.chessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>(this.board.chessPiecesByColor);
            foreach (ChessColor color in this.chessPiecesByColor.Keys)
            {
                this.chessPiecesByColor[color] = new List<ChessPiece>(this.chessPiecesByColor[color]);
                this.firstMovePieces = this.chessPiecesByColor[color].FindAll(p => p.firstMove);
            }
            
            this.savedState = true;
        }

        public void Reverse()
        {
            if (!this.savedState || this.piecesLocation == null || this.chessPiecesByColor == null) throw new NoDataSavedException();

            foreach (ChessColor color in this.chessPiecesByColor.Keys)
            {
                foreach (ChessPiece piece in this.chessPiecesByColor[color])
                {
                    if (this.piecesLocation[piece.location.Item2 - 1, piece.location.Item1 - 'a'] != piece)
                    {
                        UpdatePieceLocation(piece);
                        if (this.firstMovePieces.Contains(piece) && !piece.firstMove) piece.firstMove = true;
                    }
                }
            }
            this.board.piecesLocation = this.piecesLocation;
            this.board.chessPiecesByColor = this.chessPiecesByColor;

            this.piecesLocation = null;
            this.chessPiecesByColor = null;
            this.savedState = false;
        }

        public void UpdatePieceLocation(ChessPiece piece)
        {
            for (int i = 0; i < this.piecesLocation.GetLength(0); i++)
            {
                for (int j = 0; j < this.piecesLocation.GetLength(1); j++)
                {
                    if (this.piecesLocation[i, j] == piece)
                    {
                        piece.location = ((char)('a' + j), i - 1);
                        return;
                    }
                }
            }
        }
    }

    public class NoDataSavedException : Exception {}
}
