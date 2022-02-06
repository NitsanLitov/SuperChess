using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class RookTest
    {
        [TestMethod, TestCategory("Movement"), TestCategory("Rook")]
        public void TestWhiteRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece rook = firstPieces[(int)FirstPiecesNumber.RightRook];
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];

            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];

            string movementOptions = "d5 d6 d7 d3 c4 b4 a4 e4 f4 ";

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, "");
            
            rook.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4 h4");
            
            wRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions);
            
            bRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4");
        }
        
        [TestMethod, TestCategory("Movement"), TestCategory("Rook")]
        public void TestBlackRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece rook = secondPieces[(int)SecondPiecesNumber.RightRook];
            string movementOptions = "d5 d6 d2 d3 c4 b4 a4 e4 f4 ";

            ChessPiece bRightBishop = secondPieces[(int)SecondPiecesNumber.RightBishop];
            
            ChessPiece wRightBishop = firstPieces[(int)FirstPiecesNumber.RightBishop];
            

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, "");
            
            rook.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4 h4");
            
            wRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4");
            
            bRightBishop.ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions);
        }
    }
}
