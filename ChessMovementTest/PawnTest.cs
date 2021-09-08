using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessBoard;

// Assert.AreEqual
// Assert.AreSame
// Assert.IsFalse
// Assert.IsNotNull

namespace ChessMovementTest
{
    [TestClass]
    public class PawnTest
    {
        [TestMethod]
        public void TestBlackPawn()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            board.Move(('g', 7), ('g', 5));
            TestHelper.PrintAll(board);
            
            board.Move(('g', 2), ('g', 4));
            TestHelper.PrintAll(board);
            
            board.Move(('h', 7), ('h', 5));
            TestHelper.PrintAll(board);
            
            board.Move(('h', 5), ('g', 4));
            TestHelper.PrintAll(board);
        }
        
        [TestMethod]
        public void TestWhitePawn()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintAll(board);

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintAll(board);
            
            board.Move(('d', 4), ('d', 5));
            TestHelper.PrintAll(board);
            
            board.Move(('e', 7), ('e', 6));
            TestHelper.PrintAll(board);
            
            board.Move(('d', 5), ('e', 6));
            TestHelper.PrintAll(board);
        }
    }
}
