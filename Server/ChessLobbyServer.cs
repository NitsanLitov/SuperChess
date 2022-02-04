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
                this.HandleSocketException(e);
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
                this.HandleSocketException(e);
                throw;
            }
        }

        public void AcceptGames()
        {
            try
            {
                while (true) { this.AcceptGame(); }
            }
            catch (SocketException e)
            {
                this.HandleSocketException(e);
            }
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
            catch (SocketException e)
            {
                this.HandleSocketException(e);
            }
        }

        private void StartGame(object clientObj)
        {
            GameServer game = null;
            try
            {
                if (clientObj is not TcpClient) throw new ArgumentException("expected TcpClient as argument");

                TcpClient client = clientObj as TcpClient;
                game = new GameServer(this, client);
                this.games.Add(game);
                Console.WriteLine("Starting Game, may the odds be in your favor!");
                game.Start();
            }
            catch (SocketException e)
            {
                HandleSocketException(e);
            }
        }

        public void Close()
        {
            foreach (GameServer game in this.games)
            {
                try
                {
                    game.Stop();
                }
                catch (SocketException e)
                {
                    this.HandleSocketException(e);
                }
            }
            this.server.Stop();
        }

        private void HandleSocketException(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }
}