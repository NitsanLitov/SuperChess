using System;

using Communication;

namespace SuperChess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessLobbyServer chessLobbyServer = new ChessLobbyServer(3030, "127.0.0.1");
            chessLobbyServer.Listen();
            chessLobbyServer.AcceptGames();
        }
    }
}
