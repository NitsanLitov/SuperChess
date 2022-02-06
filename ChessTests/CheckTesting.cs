using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class CheckTest
    {
        //basic movement arent allowed, taking threatning piece disable check
        [TestMethod, TestCategory("Movement"), TestCategory("Check")]
        public void TestWhiteMovesWhileChecked()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];
            ChessPiece wRightKnight = firstPieces[(int)FirstPiecesNumber.RightKnight];
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];
            ChessPiece wLeftBishop = firstPieces[(int)FirstPiecesNumber.LeftBishop];
            ChessPiece wPawn1 = firstPieces[(int)FirstPiecesNumber.Pawn1];
            ChessPiece wPawn5 = firstPieces[(int)FirstPiecesNumber.Pawn5];

            ChessPiece bQueen = secondPieces[(int)SecondPiecesNumber.Queen];
            ChessPiece bPawn4 = secondPieces[(int)SecondPiecesNumber.Pawn4];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(bQueen, "");

            wPawn5.Dispose();
            bPawn4.Dispose();
            wLeftBishop.ForceMove(('g', 5));

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "e2");
            TestHelper.ValidateMovementResults(bQueen, "e7 f6 g5");

            board.Move(('d', 8), ('e', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(wQueen, "e2");
            TestHelper.ValidateMovementResults(wRightBishop, "e2");
            TestHelper.ValidateMovementResults(wRightKnight, "e2");
            TestHelper.ValidateMovementResults(wPawn1, "");
            TestHelper.ValidateMovementResults(wLeftBishop, "e3 e7");

            board.Move(('g', 5), ('e', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "e2");
            TestHelper.ValidateMovementResults(wQueen, "c1 e2 f3 g4 h5");
            TestHelper.ValidateMovementResults(wRightBishop, "e2 d3 c4 b5 a6");
            TestHelper.ValidateMovementResults(wRightKnight, "e2 f3 h3");
            TestHelper.ValidateMovementResults(wPawn1, "a3 a4");
            TestHelper.ValidateMovementResults(wLeftBishop, "d8 f8 d6 c5 b4 a3 f6 g5 h4");
        }

        //checking moving into check
        [TestMethod, TestCategory("Movement"), TestCategory("Check")]
        public void TestBlackMovingintoCheck()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];
            ChessPiece bPawn4 = secondPieces[(int)SecondPiecesNumber.Pawn4];
            ChessPiece bPawn5 = secondPieces[(int)SecondPiecesNumber.Pawn5];

            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(wQueen, "");
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");

            wQueen.ForceMove(('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(wQueen, "c7 d7 e7 a6 b6 c6 e6 f6 g6 h6 a3 b4 c5 e5 f4 g3 d5 d4 d3");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5 d6");

            board.Move(('e', 7), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "");
            TestHelper.ValidateMovementResults(wQueen, "c7 d7 e7 f8 a6 b6 c6 e6 f6 g6 h6 a3 b4 c5 e5 d5 d4 d3");
            TestHelper.ValidateMovementResults(bPawn4, "e4");
            TestHelper.ValidateMovementResults(bPawn5, "");

            board.Move(('d', 6), ('c', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bKing, "e7");
            TestHelper.ValidateMovementResults(wQueen, "b7 c7 d7 a6 b6 d6 e6 f6 g6 h6 c5 c4 c3 b5 a4 d5 e4 f3");
            TestHelper.ValidateMovementResults(bPawn4, "e4");
            TestHelper.ValidateMovementResults(bPawn5, "c6");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Check")]
        public void TestBlackCheckmate()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wPawn6 = firstPieces[(int)FirstPiecesNumber.Pawn6];
            ChessPiece wPawn7 = firstPieces[(int)FirstPiecesNumber.Pawn7];

            ChessPiece bQueen = secondPieces[(int)SecondPiecesNumber.Queen];
            ChessPiece bPawn4 = secondPieces[(int)SecondPiecesNumber.Pawn4];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(wPawn6, "f3 f4");
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            TestHelper.ValidateMovementResults(bQueen, "");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");

            board.Move(('f', 2), ('f', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "f2");
            TestHelper.ValidateMovementResults(wPawn6, "f4");
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            TestHelper.ValidateMovementResults(bQueen, "");
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");

            board.Move(('e', 7), ('e', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "f2");
            TestHelper.ValidateMovementResults(wPawn6, "f4");
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            TestHelper.ValidateMovementResults(bQueen, "e7 f6 g5 h4");
            TestHelper.ValidateMovementResults(bPawn4, "e5");

            board.Move(('g', 2), ('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "f2");
            TestHelper.ValidateMovementResults(wPawn6, "f4");
            TestHelper.ValidateMovementResults(wPawn7, "g5");
            TestHelper.ValidateMovementResults(bQueen, "e7 f6 g5 h4");
            TestHelper.ValidateMovementResults(bPawn4, "e5");

            board.Move(('d', 8), ('h', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(wPawn6, "");
            TestHelper.ValidateMovementResults(wPawn7, "");
            TestHelper.ValidateMovementResults(bQueen, "h3 h2 g3 f2 e1 g4 g5 f6 e7 d8 h5 h6");
            TestHelper.ValidateMovementResults(bPawn4, "e5");

            Assert.IsTrue(board.GetColorMovementOptions(wKing.color).Keys.Count == 0, "White shouldn't have movement options but does");
            Assert.IsTrue(board.IsKingThreatened(wKing.color), "White King should be threatened but isn't");
        }

        [TestMethod, TestCategory("Movement"), TestCategory("Check")]
        public void TestWhitePat()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece wKing = firstPieces[(int)FirstPiecesNumber.King];
            ChessPiece wQueen = firstPieces[(int)FirstPiecesNumber.Queen];

            ChessPiece bKing = secondPieces[(int)SecondPiecesNumber.King];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "");
            TestHelper.ValidateMovementResults(wQueen, "");
            TestHelper.ValidateMovementResults(bKing, "");

            foreach (ChessPiece piece in new List<ChessPiece>(firstPieces))
            {
                if (piece.GetType() != typeof(Queen) && piece.GetType() != typeof(King))
                    piece.Dispose();
            }

            foreach (ChessPiece piece in new List<ChessPiece>(secondPieces))
            {
                if (piece.GetType() != typeof(King))
                    piece.Dispose();
            }

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d2 e2 f2 f1");
            TestHelper.ValidateMovementResults(wQueen, "a1 b1 c1 c2 b3 a4 d2 d3 d4 d5 d6 d7 d8 e2 f3 g4 h5");
            TestHelper.ValidateMovementResults(bKing, "e7 f7 f8");

            bKing.ForceMove(('a', 8));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d2 e2 f2 f1");
            TestHelper.ValidateMovementResults(wQueen, "a1 b1 c1 c2 b3 a4 d2 d3 d4 d5 d6 d7 d8 e2 f3 g4 h5");
            TestHelper.ValidateMovementResults(bKing, "a7 b8 b7");

            wQueen.ForceMove(('c', 7));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wKing, "d1 d2 e2 f2 f1");
            TestHelper.ValidateMovementResults(wQueen, "a7 b7 b8 c8 d8 d7 e7 f7 g7 h7 b6 a5 c6 c5 c4 c3 c2 c1 d6 e5 f4 g3 h2");
            TestHelper.ValidateMovementResults(bKing, "");

            Assert.IsTrue(board.GetColorMovementOptions(bKing.color).Keys.Count == 0, "Black shouldn't have movement options but does");
            Assert.IsFalse(board.IsKingThreatened(bKing.color), "Black King shouldn't be threatened but is");
        }
    }
}