using Players;
using ChessBoard;

namespace Test
{
    static class RookTests
    {
        public static void CheckWhiteMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintBoard(board);

            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            TestHelper.MovePiece(board.chessPiecesByColor[PlayerColor.GetColor(PlayerNumber.FirstPlayer)][7], ('d', 4));
            TestHelper.PrintBoard(board);

            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));
        }
    }
}