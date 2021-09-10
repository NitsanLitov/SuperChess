using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass, TestCategory("Movement")]
    public class EnPassantTest
    {
        private void EnPassantBuildup(Board board)
        {
            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn2 = secondPieces[(int)SecondPiecesNumber.Pawn2];
            ChessPiece wPawn7 = firstPieces[(int)FirstPiecesNumber.Pawn7];
            ChessPiece bPawn1 = secondPieces[(int)SecondPiecesNumber.Pawn1];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g6 g5");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "h6 h5");
            AssertEnPassantPawn(('h', 6), false, board);

            board.Move(('g', 7), ('g', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g4");
            AssertEnPassantPawn(('g', 6), true, board);
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "h6 h5");
            AssertEnPassantPawn(('h', 6), false, board);

            board.Move(('h', 7), ('h', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g4");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "h4");
            AssertEnPassantPawn(('h', 6), true, board);

            board.Move(('h', 5), ('h', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g4");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "g3 g4");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "h3");
            AssertEnPassantPawn(('h', 6), false, board);

            board.Move(('g', 2), ('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "");
            AssertEnPassantPawn(('g', 3), true, board);
            TestHelper.ValidateMovementResults(bPawn1, "h3 g3");
            AssertEnPassantPawn(('h', 6), false, board);
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("EnPassant")]
        public void TestBlackEnPassant()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn2 = secondPieces[(int)SecondPiecesNumber.Pawn2];
            ChessPiece wPawn7 = firstPieces[(int)FirstPiecesNumber.Pawn7];
            ChessPiece bPawn1 = secondPieces[(int)SecondPiecesNumber.Pawn1];
            
            this.EnPassantBuildup(board);

            board.Move(('h', 4), ('g', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "g4");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "f2 g2 h2");
            AssertEnPassantPawn(('h', 6), false, board);
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("EnPassant")]
        public void TestBlackEnPassantPassingTurn()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn2 = secondPieces[(int)SecondPiecesNumber.Pawn2];
            ChessPiece wPawn7 = firstPieces[(int)FirstPiecesNumber.Pawn7];
            ChessPiece bPawn1 = secondPieces[(int)SecondPiecesNumber.Pawn1];
            
            this.EnPassantBuildup(board);

            board.Move(('a', 7), ('a', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn2, "");
            AssertEnPassantPawn(('g', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn7, "");
            AssertEnPassantPawn(('g', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn1, "h3");
            AssertEnPassantPawn(('h', 6), false, board);
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("EnPassant")]
        public void TestEnPassantDoesntActivateOneStep()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn4 = secondPieces[(int)SecondPiecesNumber.Pawn4];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn4, "e6 e5");
            AssertEnPassantPawn(('e', 6), false, board);

            board.Move(('e', 7), ('e', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn4, "e5");
            AssertEnPassantPawn(('e', 6), false, board);

            board.Move(('e', 6), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(bPawn4, "e4");
            AssertEnPassantPawn(('e', 6), false, board);
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("EnPassant")]
        public void TestDoubleTakeEnPassant()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn5 = secondPieces[(int)SecondPiecesNumber.Pawn5];
            ChessPiece wPawn3 = firstPieces[(int)FirstPiecesNumber.Pawn3];
            ChessPiece wPawn5 = firstPieces[(int)FirstPiecesNumber.Pawn5];

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c4 c3");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "e3 e4");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");
            AssertEnPassantPawn(('d', 6), false, board);

            board.Move(('c', 2), ('c', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c5");
            AssertEnPassantPawn(('c', 3), true, board);
            TestHelper.ValidateMovementResults(wPawn5, "e3 e4");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");
            AssertEnPassantPawn(('d', 6), false, board);

            board.Move(('e', 2), ('e', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c5");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "e5");
            AssertEnPassantPawn(('e', 3), true, board);
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");
            AssertEnPassantPawn(('d', 6), false, board);

            board.Move(('e', 4), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c5");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "e6");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");
            AssertEnPassantPawn(('d', 6), false, board);

            board.Move(('c', 4), ('c', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c6");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "e6");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "d6 d5");
            AssertEnPassantPawn(('d', 6), false, board);

            board.Move(('d', 7), ('d', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c6 d6");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "e6 d6");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "d4");
            AssertEnPassantPawn(('d', 6), true, board);

            board.Move(('e', 5), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "c6");
            AssertEnPassantPawn(('c', 3), false, board);
            TestHelper.ValidateMovementResults(wPawn5, "c7 d7 e7");
            AssertEnPassantPawn(('e', 3), false, board);
            TestHelper.ValidateMovementResults(bPawn5, "");
            AssertEnPassantPawn(('d', 6), false, board);
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("EnPassant")]
        public void TestEnPassantInvisibleToOtherPieces()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece bPawn6 = secondPieces[(int)SecondPiecesNumber.Pawn6];
            ChessPiece queen = firstPieces[(int)FirstPiecesNumber.Queen];
            ChessPiece wPawn3 = firstPieces[(int)FirstPiecesNumber.Pawn3];
            ChessPiece wPawn4 = firstPieces[(int)FirstPiecesNumber.Pawn4];

            TestHelper.PrintAll(board);

            board.Move(('c', 2), ('c', 4));
            TestHelper.PrintAll(board);

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);

            board.Move(('d', 1), ('a', 4));
            TestHelper.PrintAll(board);

            board.Move(('d', 4), ('d', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "d6");

            board.Move(('c', 7), ('c', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn3, "");
            TestHelper.ValidateMovementResults(bPawn6, "");
            AssertEnPassantPawn(('c', 6), true, board);
            TestHelper.ValidateMovementResults(wPawn4, "d6 c6");
            TestHelper.ValidateMovementResults(queen, "a3 a5 a6 a7 b4 b3 c2 d1 b5 c6 d7");
            
            board.Move(('a', 4), ('c', 6));
            TestHelper.PrintAll(board);
            Assert.IsTrue(board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)].Contains(bPawn6),"Black pawn 6 is not on board");
            TestHelper.ValidateMovementResults(wPawn3, "");
            TestHelper.ValidateMovementResults(bPawn6, "");
            AssertEnPassantPawn(('c', 6), false, board);
            TestHelper.ValidateMovementResults(wPawn4, "d6");
            TestHelper.ValidateMovementResults(queen, "b7 c7 c8 d7 a6 b6 d6 e6 f6 g6 h6 b5 a4 c5");


            //throw new Exception();
        }

        private void AssertEnPassantPawn((char, int) location, bool shouldExist, Board board)
        {
            if (shouldExist) Assert.IsTrue(board.GetPieceByLocation(location) is EnPassantPawn, "EnPassant should existence");
            else Assert.IsFalse(board.GetPieceByLocation(location) is EnPassantPawn, "EnPassant shouldn't exist");
        }
    }
}
