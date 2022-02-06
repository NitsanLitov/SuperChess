using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class QueenTest
    {
        [TestMethod, TestCategory("Movement"), TestCategory("Queen")]
        public void TestWhiteQueen()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece queen = firstPieces[(int)FirstPiecesNumber.Queen];
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];
            
            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];
            
            string movementOptions = "d5 d6 d7 d3 c4 b4 a4 e4 f4 e5 c5 b6 a7 e3 c3 ";
            
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, "");

            queen.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f6 g7 g4 h4");
            
            wRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f6 g7");
            
            bRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f6 g7 g4");

            board.Move(('f', 7), ('f', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f6 g4");

            board.Move(('e', 7), ('e', 5));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "g4");
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("Queen")]
        public void TestBlackQueen()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            ChessPiece queen = secondPieces[(int)SecondPiecesNumber.Queen];
            string movementOptions = "d5 d6 d2 d3 c4 b4 a4 e4 e5 c5 b6 f6 e3 c3 b2 ";

            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];
            
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];

            
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, "");

            queen.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f4 f2 g4 h4");
            
            bRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f4 f2");
            
            wRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f4 f2 g4");

            board.Move(('e', 2), ('e', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f4 g4");

            board.Move(('e', 3), ('e', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, movementOptions + "f2");

            board.Move(('d', 4), ('d', 3));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(queen, "d5 d6 d2 d4 c3 b3 a3 e3 e4 c4 b5 a6 e2 f1 f3 c2 g3 h3");
        }
    }
}
