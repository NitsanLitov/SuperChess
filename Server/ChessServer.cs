using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Communication
{
    public class ChessServer
    {
        private List<GameServer> games;
        private TcpListener server;

        public ChessServer(int port, string ip)
        {
            this.games = new List<GameServer>();
            try
            {
                this.server = new TcpListener(IPAddress.Parse(ip), port);
            }
            catch (SocketException e)
            {
                this.HandleException(e);
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
                this.HandleException(e);
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
                this.HandleException(e);
            }
        }

        private void AcceptGame()
        {
            try
            {
                Console.WriteLine("Waiting for new client...");
                TcpClient client = this.server.AcceptTcpClient();
                Thread game = new Thread(this.StartGame);
                game.Start(client);

                Console.WriteLine("Connected!");
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
        }

        private void StartGame(object clientObj)
        {
            if (clientObj is not TcpClient) throw new ArgumentException("expected TcpClient as asn argument");

            TcpClient client = clientObj as TcpClient;
            GameServer game = new GameServer(this, client);
            this.games.Add(game);
            Console.WriteLine("Starting Game, may the odds be in your favor!");
            game.Start();
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
                    this.HandleException(e);
                }
            }
            this.server.Stop();
        }

        private void HandleException(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }
}