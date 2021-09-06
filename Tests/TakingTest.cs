using System;
using System.Collections.Generic;

using Players;
using ChessBoard;

namespace Test
{
    static class TakingTests
    {
        public static void ValidateDeadPieceCantMove()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('f', 5));
            TestHelper.PrintAll(board);

            board.Move(secondPieces[(int)SecondPiecesNumber.RightBishop].location, ('g', 4));
            TestHelper.PrintAll(board);
        }
    }
}