using System;
using System.Collections.Generic;

using Players;
using Movement;
using ChessBoard;

namespace Test
{
    static class GeneralTests
    {
        public static void ShowBasicBoard()
        {
            TestHelper.PrintBoard(TestHelper.CreateTwoPlayerBoard());
        }
    }
}