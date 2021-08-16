using System;
using System.Collections.Generic;

namespace Board
{
    class Board
    {
        public ChessPiece[,] piecesLocation;
        public Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        public TempBoard tempBoard;
        public Board()
        {
            this.tempBoard = new TempBoard(this);
        }
        public void TempMove(ChessPiece piece, (char, int) newLocation) {}
        public bool IsKingThreatened(ChessColor color) {return true;}

        public bool KingWillBeThreatened(ChessPiece piece, (char, int) newLocation)
        {
            tempBoard.Save();

            // piece location is updated&isfirstmove
            // pieces may be removed from pieces list - but not deleted forever or updated to be killed
            // piecelocation is updated
            TempMove(piece, newLocation);
            bool kingWillBeThreatened = IsKingThreatened(piece.color);

            tempBoard.Reverse();
            return kingWillBeThreatened;
        }
    }
}
