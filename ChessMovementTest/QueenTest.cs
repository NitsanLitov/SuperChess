using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using ChessBoard;
using Players;

namespace ChessMovementTest
{
    [TestClass]
    public class QueenTest
    {   
        [TestMethod]
        public void TestWhiteQueen()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
                        
            firstPieces[(int)FirstPiecesNumber.Queen].ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);

            board.Move(('f', 7), ('f', 6));
            TestHelper.PrintAll(board);

            board.Move(('e', 7), ('e', 5));
            TestHelper.PrintAll(board);
        }
        
        [TestMethod]
        public void TestBlackQueen()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];
            
            secondPieces[(int)SecondPiecesNumber.Queen].ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);

            board.Move(('e', 2), ('e', 3));
            TestHelper.PrintAll(board);

            board.Move(('e', 3), ('e', 4));
            TestHelper.PrintAll(board);

            board.Move(('d', 4), ('d', 3));
            TestHelper.PrintAll(board);
        }
    }
}
