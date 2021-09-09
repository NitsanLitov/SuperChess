using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class BishopTest
    {
        [TestMethod]
        public void TestWhiteBishop()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];

            ChessPiece wLeftBishop = firstPieces[(int)FirstPiecesNumber.LeftBishop];
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "");
            TestHelper.ValidateMovementResults(wRightBishop, "");

            board.Move(('b', 2), ('b', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "b2 a3");
            TestHelper.ValidateMovementResults(wRightBishop, "");

            board.Move(('g', 2), ('g', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "b2 a3");
            TestHelper.ValidateMovementResults(wRightBishop, "g2 h3");

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "b2 a3 d2 e3 f4 g5 h6");
            TestHelper.ValidateMovementResults(wRightBishop, "g2 h3");
            
            board.Move(('e', 2), ('e', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "b2 a3 d2 e3 f4 g5 h6");
            TestHelper.ValidateMovementResults(wRightBishop, "g2 h3 e2 d3 c4 b5 a6");
            throw new Exception();
        }

        [TestMethod]
        public void TestBlackBishop()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            ChessPiece wLeftBishop = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece wRightBishop = secondPieces[(int)SecondPiecesNumber.King];

            TestHelper.ValidateMovementResults(wLeftBishop, "");
            TestHelper.ValidateMovementResults(wRightBishop, "");

            //black king
            board.Move(('d', 7), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "d7");
            TestHelper.ValidateMovementResults(wRightBishop, "d7");

            board.Move(('e', 8), ('d', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "c6 e6 e8");
            TestHelper.ValidateMovementResults(wRightBishop, "c6 e6 e8");

            board.Move(('d', 7), ('c', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "b6 d7 b5 c5 d5");
            TestHelper.ValidateMovementResults(wRightBishop, "b6 d7 b5 c5 d5");
            
            board.Move(('c', 6), ('b', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "a6 b6 c6 a5 c5 a4 b4 c4");
            TestHelper.ValidateMovementResults(wRightBishop, "a6 b6 c6 a5 c5 a4 b4 c4");
        }
    }
}