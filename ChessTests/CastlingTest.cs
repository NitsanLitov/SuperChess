using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class CastlingTest
    {
        [TestMethod]
        public void TestWhiteCastling()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wRightRook = firstPieces[(int)FirstPiecesNumber.RightRook];

            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];
            ChessPiece wRightKnight = firstPieces[(int)FirstPiecesNumber.RightKnight];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");

            wRightKnight.Dispose();
            wRightBishop.Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "f1 g1");

            board.Move(('e', 1), ('g', 1));
            TestHelper.PrintAll(board);
            Assert.IsTrue(wKing.location == ('g', 1), "King should be at g1");
            Assert.IsTrue(wRightRook.location == ('f', 1), "Rook should be at f1");
            TestHelper.ValidateMovementResults(wKing, "h1");
            TestHelper.ValidateMovementResults(wRightRook, "e1");
        }

        [TestMethod]
        public void TestBlackCastling()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bQueen = secondPieces[(int)SecondPiecesNumber.Queen];

            ChessPiece bRightRook = secondPieces[(int)SecondPiecesNumber.RightRook];
            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];
            ChessPiece bRightKnight = secondPieces[(int)SecondPiecesNumber.RightKnight];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bQueen, "");

            bRightBishop.Dispose();
            bRightKnight.Dispose();
            bQueen.Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");

            board.Move(('e', 8), ('c', 8));
            TestHelper.PrintAll(board);
            Assert.IsTrue(bKing.location == ('c', 8), "King should be at c8");
            Assert.IsTrue(bRightRook.location == ('d', 8), "Rook should be at d8");
            TestHelper.ValidateMovementResults(bKing, "b8");
            TestHelper.ValidateMovementResults(bRightRook, "e8");
        }

        [TestMethod]
        public void TestBlackThreatCastlingAfter()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wLeftRook = firstPieces[(int)FirstPiecesNumber.LeftRook];

            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];
            ChessPiece wLeftBishop = firstPieces[(int)FirstPiecesNumber.LeftBishop];
            ChessPiece wLeftKnight = firstPieces[(int)FirstPiecesNumber.LeftKnight];
            ChessPiece bPawn2 = secondPieces[(int)SecondPiecesNumber.Pawn2];
            ChessPiece bLeftBishop = secondPieces[(int)SecondPiecesNumber.LeftBishop];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");

            wQueen.Dispose();
            wLeftBishop.Dispose();
            wLeftKnight.Dispose();
            bPawn2.Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6");

            //threat the space between the king and the rook
            board.Move(('f', 8), ('g', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");

            board.Move(('g', 7), ('h', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");
        }

        [TestMethod]
        public void TestWhiteThreatCastlingInBetween()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bLeftRook = secondPieces[(int)SecondPiecesNumber.RightRook];

            ChessPiece bQueen = secondPieces[(int)SecondPiecesNumber.Queen];
            ChessPiece bLeftBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];
            ChessPiece bLeftKnight = secondPieces[(int)SecondPiecesNumber.RightKnight];
            ChessPiece wPawn3 = firstPieces[(int)FirstPiecesNumber.Pawn3];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bLeftRook, "");

            bQueen.Dispose();
            bLeftBishop.Dispose();
            bLeftKnight.Dispose();
            wPawn3.Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");

            board.Move(('d', 1), ('b', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");

            board.Move(('b', 7), ('b', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");

            board.Move(('b', 3), ('b', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
        }

        [TestMethod]
        public void TestWhiteCastlingPathIsNotEmpty()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wLeftRook = firstPieces[(int)FirstPiecesNumber.LeftRook];

            firstPieces[(int)FirstPiecesNumber.Queen].Dispose();
            firstPieces[(int)FirstPiecesNumber.LeftBishop].Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1");
            TestHelper.ValidateMovementResults(wLeftRook, "");
        }

        [TestMethod]
        public void TestWhiteCastlingKingNotFirstMove()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wLeftRook = firstPieces[(int)FirstPiecesNumber.LeftRook];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(wLeftRook, "");

            firstPieces[(int)FirstPiecesNumber.Queen].Dispose();
            firstPieces[(int)FirstPiecesNumber.LeftBishop].Dispose();
            firstPieces[(int)FirstPiecesNumber.LeftKnight].Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");

            board.Move(('e', 1), ('d', 1));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "c1 e1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1");

            board.Move(('d', 1), ('e', 1));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");
        }

        [TestMethod]
        public void TestBlackCastlingRookNotFirstMove()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bRightRook = secondPieces[(int)SecondPiecesNumber.LeftRook];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bRightRook, "");

            secondPieces[(int)SecondPiecesNumber.LeftBishop].Dispose();
            secondPieces[(int)SecondPiecesNumber.LeftKnight].Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "f8 g8");
            TestHelper.ValidateMovementResults(bRightRook, "f8 g8");

            board.Move(('h', 8), ('g', 8));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "f8");
            TestHelper.ValidateMovementResults(bRightRook, "f8 h8");

            board.Move(('g', 8), ('h', 8));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "f8");
            TestHelper.ValidateMovementResults(bRightRook, "g8 f8");
        }
    }
}