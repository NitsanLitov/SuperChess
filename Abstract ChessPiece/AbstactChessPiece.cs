using System;
using System.Collections.Generic;

using ChessBoard;
using MovementBoard;

namespace AbstractPiece
{
    public enum ChessColor { White, Black, Green };

    abstract class AbstractPieces
    {
        public bool firstMove;
        public (char, int) pieceLocation;
        public ChessColor pieceColor;
        private Board board;
        private MovementBoard.MovementBoard movementBoard;

        public AbstractPieces((char, int) pieceLocation, ChessColor pieceColor, Board board, MovementBoard.MovementBoard movementBoard) 
        {
            this.firstMove = true;
            this.pieceLocation = pieceLocation;
            this.pieceColor = pieceColor;
            this.board = board;
            this.movementBoard = movementBoard;
        }
    }
}