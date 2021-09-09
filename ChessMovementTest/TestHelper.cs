using System;
using System.Collections.Generic;

using Players;
using ChessBoard;

namespace ChessMovementTest
{
    enum FirstPiecesNumber { LeftRook, LeftKnight, LeftBishop, Queen, King, RightBishop, RightKnight, RightRook, Pawn1, Pawn2, Pawn3, Pawn4, Pawn5, Pawn6, Pawn7, Pawn8 };
    enum SecondPiecesNumber { LeftRook, LeftKnight, LeftBishop, King, Queen, RightBishop, RightKnight, RightRook, Pawn1, Pawn2, Pawn3, Pawn4, Pawn5, Pawn6, Pawn7, Pawn8 };

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

        public static void ValidateMovementResults(ChessPiece piece, string movements)
        {
            Dictionary<ChessPiece, List<(char, int)>> result = piece.board.GetColorMovementOptions(piece.color);

            if (!result.ContainsKey(piece))
            {
                if (movements == "") return;
                throw new MovementOptionsException($"piece shows no options for movement");
            }

            List<(char, int)> resultMovementOptions = result[piece];

            foreach (string movement in movements.Split(' '))
            {
                if (movement == "") continue;

                (char, int) location = (movement[0], (int)(movement[1] - '0'));
                if (!resultMovementOptions.Contains(location))
                    throw new MovementOptionsException($"the llegal move {movement} isn't an option");
                else
                    resultMovementOptions.Remove(location);
            }

            if (resultMovementOptions.Count != 0)
                throw new MovementOptionsException($"too many movement options {resultMovementOptions[0]}, etc...");
        }

        public static bool PieceExists(ChessPiece piece, PlayerNumber playerNumber, Board board)
        {
            return board.chessPiecesByColor[PlayerColor.GetColor(playerNumber)].Contains(piece);
        }
    }

    class MovementOptionsException : Exception
    {
        public MovementOptionsException() : base() { }
        public MovementOptionsException(string message) : base(message) { }
        public MovementOptionsException(string message, Exception inner) : base(message, inner) { }
    }
}