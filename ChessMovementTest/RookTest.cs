using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class RookTest
    {   
        [TestMethod]
        public void TestWhiteRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            firstPieces[(int)FirstPiecesNumber.RightRook].ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
        }
        
        [TestMethod]
        public void TestBlackRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            secondPieces[(int)SecondPiecesNumber.RightRook].ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
        }
    }
}
