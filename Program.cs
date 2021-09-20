﻿using System;

using Communication;

namespace SuperChess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessServer asd = new ChessServer(3030, "127.0.0.1");
            asd.Listen();
            asd.AcceptGames();
        }
    }
}
