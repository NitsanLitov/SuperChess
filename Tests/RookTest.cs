using System;
using System.Collections.Generic;

using Players;
using ChessBoard;

namespace Test
{
    static class RookTests
    {
        public static void CheckWhiteMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightRook], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            // TestHelper.MovePiece(firstPieces[7], ('d', 4));
            // TestHelper.PrintAll(board);
        }

        public static void CheckBlackMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightRook], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            // TestHelper.MovePiece(firstPieces[7], ('d', 4));
            // TestHelper.PrintAll(board);
        }
    }
}