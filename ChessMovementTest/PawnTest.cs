using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class PawnTest
    {
        [TestMethod]
        public void TestWhitePawn()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wPawn4 = firstPieces[(int)FirstPiecesNumber.Pawn4];
            ChessPiece bPawn4 = secondPieces[(int)SecondPiecesNumber.Pawn4];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "d3 d4");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "d5");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");

            board.Move(('d', 4), ('d', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "d6");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");

            board.Move(('e', 7), ('e', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "d6 e6");
            TestHelper.ValidateMovementResults(bPawn4, "e5 d5");

            board.Move(('d', 5), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "c7");
            TestHelper.ValidateMovementResults(bPawn4, "e5");
        }

        [TestMethod]
        public void TestBlackPawn()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn2 = secondPieces[(int)SecondPiecesNumber.Pawn2];
            ChessPiece wPawn7 = firstPieces[(int)FirstPiecesNumber.Pawn7];
            ChessPiece bPawn1 = secondPieces[(int)SecondPiecesNumber.Pawn1];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g5 g6");
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            TestHelper.ValidateMovementResults(bPawn1, "h6 h5");

            board.Move(('g', 7), ('g', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g4");
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            TestHelper.ValidateMovementResults(bPawn1, "h6 h5");

            board.Move(('g', 2), ('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "");
            TestHelper.ValidateMovementResults(wPawn7, "");
            TestHelper.ValidateMovementResults(bPawn1, "h6 h5");

            board.Move(('h', 7), ('h', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "");
            TestHelper.ValidateMovementResults(wPawn7, "h5");
            TestHelper.ValidateMovementResults(bPawn1, "h4 g4");

            board.Move(('h', 5), ('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "");
            TestHelper.ValidateMovementResults(wPawn7, "");
            TestHelper.ValidateMovementResults(bPawn1, "g3");
        }

        private void PromotionBuildup(Board board, Type newPieceType, int startingNumber)
        {
            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wPawn2 = firstPieces[(int)FirstPiecesNumber.Pawn2];
            ChessPiece bKnight = secondPieces[(int)SecondPiecesNumber.RightKnight];
            ChessPiece bPawn7 = secondPieces[(int)SecondPiecesNumber.Pawn7];

            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            TestHelper.ValidateMovementResults(bPawn7, "b5 b6");
            TestHelper.ValidateMovementResults(wPawn2, "b3 b4");
            TestHelper.ValidateMovementResults(bKnight, "c6 a6");

            bKnight.Dispose();
            bPawn7.Dispose();
            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            Assert.IsFalse(TestHelper.PieceExists(bKnight, PlayerNumber.SecondPlayer, board), $"second player knight shouldn't be on board");
            Assert.IsFalse(TestHelper.PieceExists(bPawn7, PlayerNumber.SecondPlayer, board), $"second player pawn7 shouldn't be on board");
            TestHelper.ValidateMovementResults(wPawn2, "b3 b4");

            board.Move(('b', 2), ('b', 4));
            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            Assert.IsFalse(TestHelper.PieceExists(bKnight, PlayerNumber.SecondPlayer, board), $"second player knight shouldn't be on board");
            Assert.IsFalse(TestHelper.PieceExists(bPawn7, PlayerNumber.SecondPlayer, board), $"second player knight shouldn't be on board");
            TestHelper.ValidateMovementResults(wPawn2, "b5");

            board.Move(('b', 4), ('b', 5));
            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            TestHelper.ValidateMovementResults(wPawn2, "b6");

            board.Move(('b', 5), ('b', 6));
            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            TestHelper.ValidateMovementResults(wPawn2, "a7 b7 c7");

            board.Move(('b', 6), ('b', 7));
            TestHelper.PrintAll(board);
            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber, "First player should only have one queen");
            TestHelper.ValidateMovementResults(wPawn2, "a8 b8 c8");
        }
        
        [DataTestMethod]
        [DataRow(typeof(Queen), 1, true)]
        [DataRow(typeof(Bishop), 2, true)]
        [DataRow(typeof(Knight), 2, true)]
        [DataRow(typeof(Rook), 2, true)]
        [DataRow(typeof(King), 1, false)]
        [DataRow(typeof(Pawn), 8, false)]
        public void TestWhitePawnPromotion(Type newPieceType, int startingNumber, bool legalMove)
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            this.PromotionBuildup(board, newPieceType, startingNumber);

            if (!legalMove)
            {
                try
                {
                    board.Move(('b', 7), ('b', 8), newPieceType);
                    throw new Exception($"this move shouldn't be llegal, can't perform promotion to {newPieceType}");
                }
                catch (PawnPromotionException) { return; }
            }

            // Update player pieces
            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            ChessPiece wPawn2 = firstPieces[(int)FirstPiecesNumber.Pawn2];

            board.Move(('b', 7), ('b', 8), newPieceType);
            TestHelper.PrintAll(board);

            // Update player pieces
            firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];

            Assert.IsTrue(firstPieces.FindAll(p => p.GetType() == newPieceType).Count == startingNumber + 1, $"First player should have {startingNumber + 1} {newPieceType}");
            Assert.IsFalse(TestHelper.PieceExists(wPawn2, PlayerNumber.FirstPlayer, board), $"First player pawn2 shouldn't be on board");
            TestHelper.ValidateMovementResults(wPawn2, "");
            ChessPiece newPiece = firstPieces.FindLast(p=>true);
            Assert.IsTrue(newPiece.GetType() == newPieceType, $"First player new piece should be {newPieceType}, instead its a {newPiece.GetType()}");
            Assert.IsTrue(TestHelper.PieceExists(newPiece, PlayerNumber.FirstPlayer, board), $"First player new {newPieceType} should be on board");
        }
    }
}
