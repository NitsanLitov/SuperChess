using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using System.Text.Json;

using Players;
using ChessBoard;

namespace Communication
{
    public class GameServer
    {
        private ChessServer server;
        private TcpClient client;
        private GameManager.GameManager gameManager;

        const string START_GAME_CATEGORY = "Start";
        const string UPDATE_MOVEMENT_CATEGORY = "UpdateMovementOptions";
        const string END_GAME_CATEGORY = "EndGame";
        const string MOVED_PIECE_CATEGORY = "MovedPiece";
        const string NOTIFY_MOVEMENT_CATEGORY = "NotifyMovementToAll";

        public GameServer(ChessServer server, TcpClient client)
        {
            this.server = server;
            this.client = client;

            RecievedMessage message = this.Read();
            ValidateMessageCategory(message, START_GAME_CATEGORY);
            StartData startData = JsonSerializer.Deserialize<StartData>(message.Data);

            this.gameManager = new GameManager.GameManager(this, startData.Nicknames);

            // List<Foo> json = JsonConvert.DeserializeObject<List<Foo>>(str)
        }

        public void UpdateMovementOptions(string nickname, Dictionary<ChessPiece, List<(char, int)>> movementOptions)
        {
            Dictionary<(char, int), List<(char, int)>> newMovementOptions = new Dictionary<(char, int), List<(char, int)>>();
            foreach (ChessPiece piece in movementOptions.Keys)
            {
                newMovementOptions[piece.location] = movementOptions[piece];
            }
            MovementOptionsData data = new MovementOptionsData(nickname, newMovementOptions);
            Message message = new MovementOptionsMessage(UPDATE_MOVEMENT_CATEGORY, data);
            this.Send(message);
        }

        public void EndGame(string nickname, string reason)
        {
            EndGameData data = new EndGameData(nickname, reason);
            Message message = new EndGameMessage(END_GAME_CATEGORY, data);
            this.Send(message);
        }

        public void NotifyMovementToAll(List<(ChessPiece, (char, int), (char, int))> movedPieces)
        {
            NotifyMovementData data = new NotifyMovementData(movedPieces);
            Message message = new NotifyMovementMessage(NOTIFY_MOVEMENT_CATEGORY, data);
            this.Send(message);
        }

        public ((char, int), (char, int)) GetMovedPiece(string nickname)
        {
            RecievedMessage message = this.Read();
            ValidateMessageCategory(message, MOVED_PIECE_CATEGORY);
            MovedPieceData movedPieceData = JsonSerializer.Deserialize<MovedPieceData>(message.Data);

            if (movedPieceData.Nickname != nickname) throw new BadNicknameException(nickname, movedPieceData.Nickname);

            return (movedPieceData.OldLocation, movedPieceData.NewLocation);
        }

        private RecievedMessage Read()
        {
            Console.WriteLine("Reading");
            try
            {
                Byte[] bytes = new Byte[1024];
                string json = "";

                NetworkStream stream = this.client.GetStream();
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    json += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    break;
                }
                return JsonSerializer.Deserialize<RecievedMessage>(json);
            }
            catch (SocketException e)
            {
                this.HandleException(e, this.client);
                throw;
            }
        }

        private void Send(Message message)
        {
            string data = JsonSerializer.Serialize(message);
            try
            {
                NetworkStream stream = this.client.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
            }
            catch (SocketException e)
            {
                this.HandleException(e, this.client);
                throw;
            }
        }

        public void CloseClient(TcpClient client)
        {
            try
            {
                client.Close();
            }
            catch (SocketException e)
            {
                this.HandleException(e);
            }
            Console.WriteLine("Game Deleted");
        }

        private void HandleException(SocketException e, TcpClient client = null)
        {
            Console.WriteLine("SocketException: {0}", e);
            if (client is TcpClient)
            {
                this.CloseClient(client);
            }
        }

        private void ValidateMessageCategory(Message message, string category)
        {
            if (message.Category != category) throw new MessageCategoryException(category, message.Category);
        }

        record StartData(List<string> Nicknames);
        record MovementOptionsData(string Nickname, Dictionary<(char, int), List<(char, int)>> MovementOptions);
        record EndGameData(string Nickname, string Reason);
        record MovedPieceData(string Nickname, (char, int) OldLocation, (char, int) NewLocation);

        class NotifyMovementData
        {
            public List<(PieceData piece, (char, int), (char, int))> MovedPieces { get; set; }
            public NotifyMovementData(List<(ChessPiece piece, (char, int), (char, int))> movedPieces)
            {
                List<(PieceData piece, (char, int), (char, int))> newMovedPieces = new List<(PieceData piece, (char, int), (char, int))>();
                foreach ((ChessPiece piece, (char, int) oldLocation, (char, int) newLocation) in movedPieces)
                {
                    newMovedPieces.Add((new PieceData(piece), oldLocation, newLocation));
                }
                this.MovedPieces = newMovedPieces;
            }
        }

        class PieceData
        {
            public string Color { get; set; }
            public string Type { get; set; }
            public PieceData(ChessPiece piece)
            {
                switch (piece.color)
                {
                    case ChessColor.White: this.Color = "w"; break;
                    case ChessColor.Black: this.Color = "b"; break;
                    case ChessColor.Green: this.Color = "g"; break;
                    default: this.Type = ""; break;
                }
                
                switch (piece)
                {
                    case Pawn p: this.Type = "pawn"; break;
                    case King p: this.Type = "king"; break;
                    case Queen p: this.Type = "queen"; break;
                    case Bishop p: this.Type = "bishop"; break;
                    case Rook p: this.Type = "rook"; break;
                    case Knight p: this.Type = "knight"; break;
                    default: this.Type = ""; break;
                }
            }
        }

        class Message
        {
            public string Category { get; set; }
            public Message(string category) { this.Category = category; }
        }
        class RecievedMessage : Message
        {
            public string Data { get; set; }
            public RecievedMessage(string category, JsonElement data) : base(category) { this.Data = JsonSerializer.Serialize(data); }
        }
        class MovementOptionsMessage : Message
        {
            public MovementOptionsData Data { get; set; }
            public MovementOptionsMessage(string category, MovementOptionsData data) : base(category) { this.Data = data; }
        }
        class EndGameMessage : Message
        {
            public EndGameData Data { get; set; }
            public EndGameMessage(string category, EndGameData data) : base(category) { this.Data = data; }
        }
        class NotifyMovementMessage : Message
        {
            public NotifyMovementData Data { get; set; }
            public NotifyMovementMessage(string category, NotifyMovementData data) : base(category) { this.Data = data; }
        }
    }

    public class MessageCategoryException : Exception
    {
        public MessageCategoryException() : base() { }
        public MessageCategoryException(string expectedCategory, string actualCategory) : base($"expected {expectedCategory} but got {actualCategory}") { }
    }

    public class BadNicknameException : Exception
    {
        public BadNicknameException() : base() { }
        public BadNicknameException(string expectedNickname, string actualNickname) : base($"expected {expectedNickname} but got {actualNickname}") { }
    }
}