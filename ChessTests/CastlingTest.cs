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
        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
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

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
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

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
        public void TestBlackThreatCastlingAfter()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wLeftRook = firstPieces[(int)FirstPiecesNumber.LeftRook];

            ChessPiece bLeftBishop = secondPieces[(int)SecondPiecesNumber.LeftBishop];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            firstPieces[(int)FirstPiecesNumber.Queen].Dispose();
            firstPieces[(int)FirstPiecesNumber.LeftBishop].Dispose();
            firstPieces[(int)FirstPiecesNumber.LeftKnight].Dispose();
            secondPieces[(int)SecondPiecesNumber.Pawn1].Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(bLeftBishop, "g7 h6");

            //threat the space between the king and the rook
            board.Move(('f', 8), ('h', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 c1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");
            TestHelper.ValidateMovementResults(bLeftBishop, "f8 g7 g5 f4 e3 d2");

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1");
            TestHelper.ValidateMovementResults(wLeftRook, "b1 c1 d1");
            TestHelper.ValidateMovementResults(bLeftBishop, "f8 g7 g5 f4 e3 d2 c1");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
        public void TestWhiteThreatCastlingInBetween()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bLeftRook = secondPieces[(int)SecondPiecesNumber.RightRook];
            ChessPiece bPawn6 = secondPieces[(int)SecondPiecesNumber.Pawn6];

            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bLeftRook, "");
            TestHelper.ValidateMovementResults(wQueen, "");

            secondPieces[(int)SecondPiecesNumber.Queen].Dispose();
            secondPieces[(int)SecondPiecesNumber.RightBishop].Dispose();
            secondPieces[(int)SecondPiecesNumber.RightKnight].Dispose();
            firstPieces[(int)FirstPiecesNumber.Pawn3].Dispose();

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
            TestHelper.ValidateMovementResults(wQueen, "c2 b3 a4");

            board.Move(('d', 1), ('b', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
            TestHelper.ValidateMovementResults(wQueen, "a3 a4 b4 b5 b6 b7 c4 d5 e6 f7 c3 d3 e3 f3 g3 h3 c2 d1");

            board.Move(('b', 7), ('b', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
            TestHelper.ValidateMovementResults(wQueen, "a3 a4 b4 b5 b6 c4 d5 e6 f7 c3 d3 e3 f3 g3 h3 c2 d1");

            board.Move(('b', 3), ('b', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "d8 c8");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
            TestHelper.ValidateMovementResults(wQueen, "a5 a6 a7 b7 b8 c7 c6 d6 e6 f6 g6 h6 c5 d4 e3 b5 b4 b3");
            TestHelper.ValidateMovementResults(bPawn6, "c6 c5 b6");

            board.Move(('c', 7), ('c', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bPawn6, "c5");
            TestHelper.ValidateMovementResults(bLeftRook, "b8 c8 d8");
            TestHelper.ValidateMovementResults(wQueen, "a5 a6 a7 b7 b8 c7 d8 c6 c5 d4 e3 b5 b4 b3");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
        public void TestWhiteCastlingPathIsNotEmpty()
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

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1");
            TestHelper.ValidateMovementResults(wLeftRook, "");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
        public void TestCastlingWhiteThreatBlackKing()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bRightRook = secondPieces[(int)SecondPiecesNumber.LeftRook];
            ChessPiece bPawn5 = secondPieces[(int)SecondPiecesNumber.Pawn5];
            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(bRightRook, "");
            TestHelper.ValidateMovementResults(wQueen, "");
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");

            firstPieces[(int)FirstPiecesNumber.Pawn3].Dispose();
            secondPieces[(int)SecondPiecesNumber.LeftKnight].Dispose();
            secondPieces[(int)SecondPiecesNumber.LeftBishop].Dispose();

            board.Move(('d', 7), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn5, "d5");
            TestHelper.ValidateMovementResults(bKing, "d7 f8 g8");
            TestHelper.ValidateMovementResults(bRightRook, "g8 f8");
            TestHelper.ValidateMovementResults(wQueen, "c2 b3 a4");

            board.Move(('d', 1), ('a', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "f8");
            TestHelper.ValidateMovementResults(bRightRook, "");
            TestHelper.ValidateMovementResults(wQueen, "a3 a5 a6 a7 b3 c2 d1 b4 c4 d4 e4 f4 g4 h4 b5 c6 d7 e8");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
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

        [TestMethod, TestCategory("Movement"), TestCategory("Castling")]
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