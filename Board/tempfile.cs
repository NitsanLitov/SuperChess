using System;
using System.Collections.Generic;

using ChessBoard;
using Movement;
using Players;

namespace ChessBoard
{
    class ChessPiece
    {
        public (char, int) location;

        public List<(char, int)> GetMovementOption()
        {
            return new List<(char, int)>();
        }
        
        public void Move((char, int) location)
        {
        }
    }
    class Pawn : ChessPiece
    {
        public Pawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
    class Bishop : ChessPiece
    {
        public Bishop((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
    class Rook : ChessPiece
    {
        public Rook((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
    class Queen : ChessPiece
    {
        public Queen((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
    class King : ChessPiece
    {
        public King((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
    class Knight : ChessPiece
    {
        public Knight((char, int) location, ChessColor color, Board board, MovementBoard movementBoard)
        {

        }
    }
}