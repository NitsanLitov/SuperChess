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

            board.Move(('c', 1), ('g', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "c1 d2 e3 f4 h4 h6 f6 e7");
            TestHelper.ValidateMovementResults(wRightBishop, "g2 h3 e2 d3 c4 b5 a6");

            board.Move(('f', 1), ('b', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftBishop, "c1 d2 e3 f4 h4 h6 f6 e7");
            TestHelper.ValidateMovementResults(wRightBishop, "a4 a6 c4 d3 e2 f1 c6 d7");
        }

        [TestMethod]
        public void TestBlackBishop()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            ChessPiece bLeftBishop = secondPieces[(int)SecondPiecesNumber.LeftBishop];
            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];

            TestHelper.ValidateMovementResults(bLeftBishop, "");
            TestHelper.ValidateMovementResults(bRightBishop, "");
            TestHelper.PrintAll(board);

            //black bishop
            board.Move(('b', 7), ('b', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "");
            TestHelper.ValidateMovementResults(bRightBishop, "b7 a6");

            board.Move(('g', 7), ('g', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6");
            TestHelper.ValidateMovementResults(bRightBishop, "b7 a6");

            board.Move(('d', 7), ('d', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6");
            TestHelper.ValidateMovementResults(bRightBishop, "b7 a6 d7 e6 f5 g4 h3");

            board.Move(('e', 7), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6 e7 d6 c5 b4 a3");
            TestHelper.ValidateMovementResults(bRightBishop, "b7 a6 d7 e6 f5 g4 h3");

            board.Move(('c', 8), ('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6 e7 d6 c5 b4 a3");
            TestHelper.ValidateMovementResults(bRightBishop, "c8 d7 e6 f5 h5 h3 f3 e2");

            board.Move(('f', 8), ('b', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftBishop, "a5 a3 c5 d6 e7 f8 c3 d2");
            TestHelper.ValidateMovementResults(bRightBishop, "c8 d7 e6 f5 h5 h3 f3 e2");
            throw new Exception();
        }
    }
}