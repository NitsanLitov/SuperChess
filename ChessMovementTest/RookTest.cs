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

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece rook = firstPieces[(int)FirstPiecesNumber.RightRook];
            string movementOptions = "d5 d6 d7 d3 c4 b4 a4 e4 f4 ";

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, "");
            
            rook.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4 h4");
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions);
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4");
        }
        
        [TestMethod]
        public void TestBlackRook()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            ChessPiece rook = secondPieces[(int)SecondPiecesNumber.RightRook];
            string movementOptions = "d5 d6 d2 d3 c4 b4 a4 e4 f4 ";
            

            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, "");
            
            rook.ForceMove(('d', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4 h4");
            
            firstPieces[(int)FirstPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions + "g4");
            
            secondPieces[(int)SecondPiecesNumber.RightBishop].ForceMove(('g', 4));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(rook, movementOptions);
        }
    }
}
