using System;
using System.Collections.Generic;

using Players;
using ChessBoard;

namespace Test
{
    static class QueenTests
    {
        public static void CheckWhiteMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.Queen], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);

            board.Move(('f', 7), ('f', 6));
            TestHelper.PrintAll(board);

            board.Move(('e', 7), ('e', 5));
            TestHelper.PrintAll(board);
        }

        public static void CheckBlackMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.Queen], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);

            board.Move(('e', 2), ('e', 3));
            TestHelper.PrintAll(board);

            board.Move(('e', 3), ('e', 4));
            TestHelper.PrintAll(board);

            board.Move(('d', 4), ('d', 3));
            TestHelper.PrintAll(board);
        }
    }
}