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
            
            // board.Move(('d', 5), ('e', 6));
            // TestHelper.PrintAll(board);
            // TestHelper.ValidateMovementResults(wPawn4, "d7 e7 f7");
            // TestHelper.ValidateMovementResults(bPawn4, "");
            
            board.Move(('d', 5), ('d', 6));
            TestHelper.PrintAll(board);
            TestHelper.ValidateMovementResults(wPawn4, "c7");
            TestHelper.ValidateMovementResults(bPawn4, "e5");
        }
    }
}
