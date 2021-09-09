using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class KnightTest
    {
        [TestMethod]
        public void TestWhiteKnight()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];

            ChessPiece wLeftKnight = firstPieces[(int)FirstPiecesNumber.LeftKnight];
            ChessPiece wRightKnight = firstPieces[(int)FirstPiecesNumber.RightKnight];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "a3 c3");
            TestHelper.ValidateMovementResults(wRightKnight, "f3 h3");

            board.Move(('b', 1), ('c', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "b1 a4 b5 d5 e4");
            TestHelper.ValidateMovementResults(wRightKnight, "f3 h3");

            board.Move(('g', 1), ('f', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "b1 a4 b5 d5 e4");
            TestHelper.ValidateMovementResults(wRightKnight, "g1 h4 g5 e5 d4");

            board.Move(('c', 3), ('d', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "c3 b4 b6 c7 e7 f6 f4 e3");
            TestHelper.ValidateMovementResults(wRightKnight, "g1 h4 g5 e5 d4");

            board.Move(('f', 3), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "c3 b4 b6 c7 e7 f6 f4 e3");
            TestHelper.ValidateMovementResults(wRightKnight, "d3 c4 c6 d7 f7 g6 g4 f3");

            board.Move(('d', 5), ('c', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "a6 a8 e8 e6 d5 b5");
            TestHelper.ValidateMovementResults(wRightKnight, "d3 c4 c6 d7 f7 g6 g4 f3");

            board.Move(('e', 5), ('f', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wLeftKnight, "a6 a8 e8 e6 d5 b5");
            TestHelper.ValidateMovementResults(wRightKnight, "h8 h6 g5 e5 d6 d8");
        }

        [TestMethod]
        public void TestBlackKnight()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bLeftKnight = secondPieces[(int)SecondPiecesNumber.LeftKnight];
            ChessPiece bRightKnight = secondPieces[(int)SecondPiecesNumber.RightKnight];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "f6 h6");
            TestHelper.ValidateMovementResults(bRightKnight, "a6 c6");

            board.Move(('b', 8), ('c', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "f6 h6");
            TestHelper.ValidateMovementResults(bRightKnight, "b8 a5 b4 d4 e5");

            board.Move(('g', 8), ('f', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "g8 h5 g4 e4 d5");
            TestHelper.ValidateMovementResults(bRightKnight, "b8 a5 b4 d4 e5");

            board.Move(('c', 6), ('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "g8 h5 g4 e4 d5");
            TestHelper.ValidateMovementResults(bRightKnight, "c6 b5 b3 c2 e2 f3 f5 e6");

            board.Move(('f', 6), ('e', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "d6 c5 c3 d2 f2 g3 g5 f6");
            TestHelper.ValidateMovementResults(bRightKnight, "c6 b5 b3 c2 e2 f3 f5 e6");

            board.Move(('d', 4), ('c', 2));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "d6 c5 c3 d2 f2 g3 g5 f6");
            TestHelper.ValidateMovementResults(bRightKnight, "a3 a1 e1 e3 d4 b4");

            board.Move(('e', 4), ('f', 2));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bLeftKnight, "h1 h3 g4 e4 d3 d1");
            TestHelper.ValidateMovementResults(bRightKnight, "a3 a1 e1 e3 d4 b4");
        }
    }
}