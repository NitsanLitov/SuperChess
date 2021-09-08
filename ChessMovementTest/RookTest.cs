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
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightRook], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            // TestHelper.MovePiece(firstPieces[7], ('d', 4));
            // TestHelper.PrintAll(board);
        }
        
        [TestMethod]
        public void TestBlackRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightRook], ('d', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(firstPieces[(int)FirstPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            
            TestHelper.MovePiece(secondPieces[(int)SecondPiecesNumber.RightBishop], ('g', 4));
            TestHelper.PrintAll(board);
            // TestHelper.MovePiece(firstPieces[7], ('d', 4));
            // TestHelper.PrintAll(board);
        }
    }
}
