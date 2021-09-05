using System;
using System.Collections.Generic;

using Players;
using Movement;
using ChessBoard;

namespace Test
{
    static class PawnTests
    {
        public static void CheckBlackMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintBoard(board);

            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            board.Move(('g', 7), ('g', 5));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('g', 2), ('g', 4));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('h', 7), ('h', 5));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('h', 5), ('g', 4));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));
        }

        public static void CheckWhiteMovement()
        {
            Board board = TestHelper.CreateTwoPlayerBoard();
            TestHelper.PrintBoard(board);

            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            board.Move(('d', 2), ('d', 4));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('d', 4), ('d', 5));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('e', 7), ('e', 6));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));

            
            board.Move(('d', 5), ('e', 6));
            TestHelper.PrintBoard(board);
            
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.SecondPlayer)));
            TestHelper.PrintMovementOptions(board.GetColorMovementOptions(PlayerColor.GetColor(PlayerNumber.FirstPlayer)));
        }
    }
}