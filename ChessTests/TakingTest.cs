using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class TakingTest
    {
        [TestMethod, TestCategory("Taking")]
        public void TestBlackDeadPieceCantMove()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];
            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wRightBishop, "");
            TestHelper.ValidateMovementResults(bRightBishop, "");
            
            wRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wRightBishop, "f5 e6 d7 f3 h5 h3");
            TestHelper.ValidateMovementResults(bRightBishop, "");
            
            bRightBishop.ForceMove(('e', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wRightBishop, "f5 e6 f3 h5 h3");
            TestHelper.ValidateMovementResults(bRightBishop, "f5 g4 d5 c4 b3 a2");
            
            wRightBishop.ForceMove(('e', 6));

            TestHelper.PrintAll(board);
            Assert.IsFalse(TestHelper.PieceExists(bRightBishop, PlayerNumber.SecondPlayer, board), $"Second player right bishop shouldn't be on board");
            Assert.IsTrue(TestHelper.PieceExists(wRightBishop, PlayerNumber.FirstPlayer, board), $"First player right bishop should be on board");
            TestHelper.ValidateMovementResults(wRightBishop, "d7 f7 f5 g4 h3 d5 c4 b3");
            TestHelper.ValidateMovementResults(bRightBishop, "");
        }
        
        [TestMethod, TestCategory("Taking")]
        public void TestWhiteDeadPieceCantMove()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn8 = secondPieces[(int)SecondPiecesNumber.Pawn8];
            ChessPiece bRightRook = secondPieces[(int)SecondPiecesNumber.RightRook];
            ChessPiece wPawn1 = firstPieces[(int)FirstPiecesNumber.Pawn1];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bRightRook, "");
            TestHelper.ValidateMovementResults(wPawn1, "a4 a3");

            bPawn8.Dispose();
            TestHelper.PrintAll(board);
            Assert.IsFalse(TestHelper.PieceExists(bPawn8, PlayerNumber.SecondPlayer, board), $"Second player pawn8 shouldn't be on board");
            TestHelper.ValidateMovementResults(bPawn8, "");
            TestHelper.ValidateMovementResults(bRightRook, "a7 a5 a6 a3 a4 a2");
            TestHelper.ValidateMovementResults(wPawn1, "a3 a4");
            
            board.Move(('a', 8), ('a', 2));
            TestHelper.PrintAll(board);
            Assert.IsFalse(TestHelper.PieceExists(wPawn1, PlayerNumber.FirstPlayer, board), $"First player pawn1 shouldn't be on board");
            TestHelper.ValidateMovementResults(bRightRook, "a7 a5 a6 a3 a4 a8 a1 b2");
            TestHelper.ValidateMovementResults(wPawn1, "");
        }
    }
}
