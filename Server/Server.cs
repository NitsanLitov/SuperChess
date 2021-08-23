using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Communication
{
    class Server
    {
        private TcpListener server;
        private List<TcpClient> clients;

        public Server(int port, string ip)
        {
            this.clients = new List<TcpClient>();
            try
            {
                this.server = new TcpListener(IPAddress.Parse(ip), port);
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw e;
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
                throw e;
            }
        }

        private string Read(TcpClient client)
        {
            try
            {
                Byte[] bytes = new Byte[1024];
                String data = null;

                NetworkStream stream = client.GetStream();
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    return data;
                }
                return "";
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw e;
            }
        }

        private void Send(TcpClient client, string data)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw e;
            }
        }

        public void AcceptClients(int numClient)
        {
            try
            {
                this.clients.Add(this.server.AcceptTcpClient());
                Console.WriteLine("Connected!");
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw e;
            }
        }

        public void Close()
        {
            try
            {
                foreach (TcpClient client in this.clients)
                {
                    client.Close();
                }
                this.server.Stop();
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw e;
            }
        }

        private void HandleException(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            this.Close();
        }
    }
}