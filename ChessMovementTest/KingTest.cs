using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class KingTest
    {
        [TestMethod]
        public void TestWhiteKing()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");

            board.Move(('d', 2), ('d', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d2");

            board.Move(('e', 1), ('d', 2));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "c3 e3 e1");

            board.Move(('d', 2), ('c', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "b3 d2 b4 c4 d4");
            
            board.Move(('c', 3), ('b', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "a3 b3 c3 a4 c4 a5 b5 c5");
        }

        [TestMethod]
        public void TestBlackKing()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];

            TestHelper.ValidateMovementResults(bKing, "");

            board.Move(('d', 7), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d7");

            board.Move(('e', 8), ('d', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "c6 e6 e8");

            board.Move(('d', 7), ('c', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "b6 d7 b5 c5 d5");
            
            board.Move(('c', 6), ('b', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "a6 b6 c6 a5 c5 a4 b4 c4");
            
            board.Move(('c', 2), ('c', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "a5 a6 b6 c6 c5 c4 b4");
            
            board.Move(('b', 5), ('c', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "b4 b5 c5 d5 d4");

            throw new Exception();
        }
    }
}