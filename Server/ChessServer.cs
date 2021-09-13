using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Communication
{
    public class ChessServer
    {
        private TcpListener server;

        public ChessServer(int port, string ip)
        {
            // this.clients = new List<TcpClient>();
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
                this.server.Start();
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
        }

        // public void SendMovements()
        // {
        //     this.DisposeOfDisconnectedClients();

        //     foreach (TcpClient client in this.clients)
        //     {
        //         this.Send(client, "{\"message\":\"welcome!\"}");
        //     }
        // }

        // public string GetMovement()
        // {
        //     this.DisposeOfDisconnectedClients();
            
        //     return this.Read(this.clients[this.clients.Count-1]);
        // }

        // public void AcceptClients(int numClient)
        // {
        //     try
        //     {
        //         for (int i = 0; i < numClient; i++)
        //         {
        //             Console.WriteLine("asd!");
        //             this.clients.Add(this.server.AcceptTcpClient());
        //             Console.WriteLine("Connected!");
        //         }
        //     }
        //     catch (SocketException e)
        //     {
        //         this.HandleException(e);
        //         throw;
        //     }
        // }

        public void AcceptGame()
        {
            try
            {
                Console.WriteLine("waiting...");
                new GameServer(this, this.server.AcceptTcpClient());
                Console.WriteLine("Connected!");
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
        }

        // private void DisposeOfDisconnectedClients()
        // {
        //     foreach (TcpClient client in new List<TcpClient>(this.clients))
        //     {
        //         if (!ClientConnected(client))
        //         {
        //             this.CloseClient(client);
        //         }
        //     }
        // }

        // public bool ClientConnected(TcpClient client)
        // {
        //     var connection = IPGlobalProperties.GetIPGlobalProperties()
        //         .GetActiveTcpConnections()
        //         .FirstOrDefault(x => x.LocalEndPoint.Equals(client.Client.LocalEndPoint));
        //     TcpState state = connection != null ? connection.State : TcpState.Unknown;

        //     return state == TcpState.Established;
        // }

        public void Close()
        {
            try
            {
                // foreach (TcpClient client in this.clients)
                // {
                //     client.Close();
                // }
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
            finally
            {
                this.server.Stop();
            }
        }

        // public void CloseClient(TcpClient client)
        // {
        //     try
        //     {
        //         client.Close();
        //     }
        //     catch (SocketException e)
        //     {
        //         this.HandleException(e);
        //     }
        //     this.clients.Remove(client);
        //     Console.WriteLine("deleted");
        // }

        private void HandleException(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }
}