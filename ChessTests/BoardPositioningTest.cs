using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;
using Players;

namespace ChessTests
{
    [TestClass]
    public class BoardPositioningTest
    {
        [TestMethod, TestCategory("BoardPositioning")]
        public void TestBasicTwoPlayerBoardPositioning()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();

            List<ChessPiece> firstPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)];
            List<ChessPiece> secondPieces = board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.SecondPlayer)];

            System.Console.WriteLine(firstPieces.Count);
            System.Console.WriteLine(secondPieces.Count);
            TestHelper.PrintAll(board);
            System.Console.WriteLine(firstPieces.Count);
            System.Console.WriteLine(secondPieces.Count);

            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Queen], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.King], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.RightBishop], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.LeftBishop], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.RightRook], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.LeftRook], "");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.RightKnight], "f3 h3");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.LeftKnight], "a3 c3");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn1], "a3 a4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn2], "b3 b4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn3], "c3 c4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn4], "d3 d4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn5], "e3 e4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn6], "f3 f4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn7], "g3 g4");
            TestHelper.ValidateMovementResults(firstPieces[(int)FirstPiecesNumber.Pawn8], "h3 h4");

            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Queen], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.King], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.RightBishop], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.LeftBishop], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.RightRook], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.LeftRook], "");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.RightKnight], "a6 c6");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.LeftKnight], "f6 h6");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn1], "h6 h5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn2], "g6 g5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn3], "f6 f5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn4], "e6 e5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn5], "d6 d5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn6], "c6 c5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn7], "b6 b5");
            TestHelper.ValidateMovementResults(secondPieces[(int)SecondPiecesNumber.Pawn8], "a6 a5");
        }
    }
}
