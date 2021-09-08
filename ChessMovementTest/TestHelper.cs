using System;
using System.Collections.Generic;

using Players;
using ChessBoard;

namespace ChessMovementTest
{
    public enum FirstPiecesNumber { LeftRook, LeftKnight, LeftBishop, Queen, King, RightBishop, RightKnight, RightRook, Pawn1, Pawn2, Pawn3, Pawn4, Pawn5, Pawn6, Pawn7, Pawn8 };
    public enum SecondPiecesNumber { LeftRook, LeftKnight, LeftBishop, King, Queen, RightBishop, RightKnight, RightRook, Pawn1, Pawn2, Pawn3, Pawn4, Pawn5, Pawn6, Pawn7, Pawn8 };

    static class TestHelper
    {
        public static void PrintBoard(Board board)
        {
            for (int number = 8; number >= 0; number--)
            {
                if (number != 0) Console.Write($"{number}   ");
                else Console.Write("    ");
                
                for (char letter = 'a'; letter <= 'h'; letter++)
                {
                    if (number == 0)
                    {
                        Console.Write($" {letter}  ");
                        continue;
                    }
                    ChessPiece piece = board.GetPieceByLocation((letter, number));
                    Console.Write($"{PieceToStr(piece)} ");
                }
                Console.WriteLine();
            }
        }

        public static string PieceToStr(ChessPiece piece)
        {
            string pieceStr = "";
            if (piece == null) return "   ";

            if (piece.color == ChessColor.Black) pieceStr += "B";
            if (piece.color == ChessColor.Green) pieceStr += "G";
            if (piece.color == ChessColor.White) pieceStr += "W";
            
            pieceStr += "-";
            
            if (piece is Rook) pieceStr += "R";
            if (piece is Bishop) pieceStr += "B";
            if (piece is Knight) pieceStr += "N";
            if (piece is King) pieceStr += "K";
            if (piece is Queen) pieceStr += "Q";
            if (piece is Pawn) pieceStr += "P";

            return pieceStr;
        }

        public static Board CreateTwoPlayerBoard()
        {
            // Player[] players = new Player[2]{new Player("second", PlayerNumber.SecondPlayer), new Player("first", PlayerNumber.FirstPlayer)};
            Board board = new Board(2);
            return board;
        }

        public static void PrintMovementOptions(Dictionary<ChessPiece, List<(char, int)>> options)
        {
            foreach (ChessPiece piece in options.Keys)
            {
                Console.Write($"{PieceToStr(piece)} ");
                foreach ((char letter, int number) in options[piece])
                {
                    Console.Write($"{letter}{number} ");
                }
                Console.Write(" || ");
            }
            Console.WriteLine();
        }

        public static void PrintAll(Board board)
        {
            PrintBoard(board);

            PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));
        }
    }
}