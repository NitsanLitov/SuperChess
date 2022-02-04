using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Communication
{
    public class ChessLobbyServer
    {
        private List<GameServer> games;
        private TcpListener server;

        public ChessLobbyServer(int port, string ip)
        {
            this.games = new List<GameServer>();
            try
            {
                this.server = new TcpListener(IPAddress.Parse(ip), port);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Listen()
        {
            try
            {
                Console.WriteLine("Starting Server...");
                this.server.Start();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AcceptGames()
        {
            while (true) this.AcceptGame();
        }

        private void AcceptGame()
        {
            try
            {
                Console.WriteLine("Waiting for new client...");
                TcpClient client = this.server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                Thread game = new Thread(this.StartGame);
                game.Start(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void StartGame(object clientObj)
        {
            if (clientObj is not TcpClient) throw new ArgumentException("expected TcpClient as argument");

            TcpClient client = clientObj as TcpClient;
            GameServer game = new GameServer(this, client);
            this.games.Add(game);
            Console.WriteLine("Starting Game, may the odds be in your favor!");
            game.Start();
            this.games.Remove(game);
            Console.WriteLine("Game Deleted");
        }

        public void Close()
        {
            foreach (GameServer game in this.games)
            {
                try
                {
                    game.Stop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            this.server.Stop();
        }
    }
}