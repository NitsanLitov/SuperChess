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
        public TcpClient client;
        private GameManager.GameManager gameManager;

        const string START_GAME_CATEGORY = "Start";
        const string UPDATE_PLAYERS_CATEGORY = "Players";
        const string UPDATE_MOVEMENT_CATEGORY = "UpdateMovementOptions";
        const string END_GAME_CATEGORY = "EndGame";
        const string MOVED_PIECE_CATEGORY = "MovedPiece";
        const string NOTIFY_MOVEMENT_CATEGORY = "NotifyMovementToAll";

        public GameServer(ChessServer server, TcpClient client)
        {
            this.server = server;
            this.client = client;
        }

        public void Start()
        {
            RecievedMessage message = this.Read();
            ValidateMessageCategory(message, START_GAME_CATEGORY);
            StartData startData = JsonSerializer.Deserialize<StartData>(message.DataStr);

            this.gameManager = new GameManager.GameManager(this, startData.Nicknames);

            this.UpdatePlayers();

            this.gameManager.StartGame();
        }

        public void Stop()
        {
            this.CloseClient();
            this.gameManager = null;
        }

        public bool ClientConnected(TcpClient client)
        {
            var connection = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .FirstOrDefault(x => x.LocalEndPoint.Equals(client.Client.LocalEndPoint));
            TcpState state = connection != null ? connection.State : TcpState.Unknown;

            return state == TcpState.Established;
        }

        public void UpdatePlayers()
        {
            List<Dictionary<string, string>> playersData = new List<Dictionary<string, string>>();
            foreach (Player p in this.gameManager.players)
            {
                playersData.Add(new Dictionary<string, string>()
                {
                    ["color"] = ChessColorToString(p.Color),
                    ["nickname"] = p.Nickname
                });
            }

            PlayersData data = new PlayersData(playersData);
            UpdatePlayersMessage message = new UpdatePlayersMessage(UPDATE_PLAYERS_CATEGORY, data);
            this.Send(message);
        }

        public void UpdateMovementOptions(string nickname, Dictionary<ChessPiece, List<(char, int)>> movementOptions)
        {
            Dictionary<(char, int), List<(char, int)>> newMovementOptions = new Dictionary<(char, int), List<(char, int)>>();
            foreach (ChessPiece piece in movementOptions.Keys)
            {
                newMovementOptions[piece.location] = movementOptions[piece];
            }
            MovementOptionsData data = new MovementOptionsData(nickname, newMovementOptions);
            MovementOptionsMessage message = new MovementOptionsMessage(UPDATE_MOVEMENT_CATEGORY, data);
            this.Send(message);
        }

        public void EndGame(string nickname, string reason)
        {
            EndGameData data = new EndGameData(nickname, reason);
            EndGameMessage message = new EndGameMessage(END_GAME_CATEGORY, data);
            this.Send(message);
        }

        public void NotifyMovementToAll(List<(ChessPiece, (char, int), (char, int))> movedPieces)
        {
            NotifyMovementData data = new NotifyMovementData(movedPieces);
            NotifyMovementMessage message = new NotifyMovementMessage(NOTIFY_MOVEMENT_CATEGORY, data);
            this.Send(message);
        }

        public ((char, int), (char, int)) GetMovedPiece(string nickname)
        {
            RecievedMessage message = this.Read();
            ValidateMessageCategory(message, MOVED_PIECE_CATEGORY);
            MovedPieceData movedPieceData = JsonSerializer.Deserialize<MovedPieceData>(message.DataStr);

            // instead while until nickname is the right choise... send error message containing the error
            if (movedPieceData.Nickname != nickname) throw new BadNicknameException(nickname, movedPieceData.Nickname);

            return (movedPieceData.OldLocationTuple, movedPieceData.NewLocationTuple);
        }

        private RecievedMessage Read()
        {
            if (!this.ClientConnected(this.client)) { this.Stop(); throw new ClientDisconnectedException(); }

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
                Console.WriteLine(json);
                return JsonSerializer.Deserialize<RecievedMessage>(json);
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
        }

        private void Send(Message message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(EndGameMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(UpdatePlayersMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(NotifyMovementMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(MovementOptionsMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(string data)
        {
            if (!this.ClientConnected(this.client)) { this.Stop(); throw new ClientDisconnectedException(); }

            try
            {
                NetworkStream stream = this.client.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
            }
            catch (SocketException e)
            {
                this.HandleException(e);
                throw;
            }
        }

        private void CloseClient()
        {
            try
            {
                this.client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("Game Deleted");
        }

        private void HandleException(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            this.CloseClient();
        }

        private void ValidateMessageCategory(Message message, string category)
        {
            if (message.Category != category) throw new MessageCategoryException(category, message.Category);
        }

        private static string ChessColorToString(ChessColor color)
        {
            switch (color)
            {
                case ChessColor.White: return "w";
                case ChessColor.Black: return "b";
                case ChessColor.Green: return "g";
                default: return "";
            }
        }

        private static string ChessPieceToString(ChessPiece piece)
        {
            switch (piece)
            {
                case Pawn p: return "pawn";
                case King p: return "king";
                case Queen p: return "queen";
                case Bishop p: return "bishop";
                case Rook p: return "rook";
                case Knight p: return "knight";
                default: return "";
            }
        }

        private static List<object> LocationToList((char, int) location)
        {
            return new List<object>() { location.Item1, location.Item2 };
        }

        private static List<object> LocationsToList(List<(char, int)> locations)
        {
            List<object> list = new List<object>();
            foreach ((char, int) location in locations)
            {
                list.Add(LocationToList(location));
            }
            return list;
        }

        record StartData(List<string> Nicknames);
        record PlayersData(List<Dictionary<string, string>> Nicknames);
        record EndGameData(string Nickname, string Reason);

        class MovedPieceData
        {
            public string Nickname { get; set; }
            public List<JsonElement> oldLocation;
            public List<JsonElement> newLocation;
            public List<JsonElement> OldLocation { get { return this.oldLocation; } set { this.oldLocation = value; this.OldLocationTuple = (value[0].GetString()[0], value[1].GetInt32()); } }
            public List<JsonElement> NewLocation { get { return this.newLocation; } set { this.newLocation = value; this.NewLocationTuple = (value[0].GetString()[0], value[1].GetInt32()); } }
            public (char, int) OldLocationTuple { get; set; }
            public (char, int) NewLocationTuple { get; set; }
            public MovedPieceData(string nickname, List<JsonElement> oldLocation, List<JsonElement> newLocation)
            {
                this.Nickname = nickname;
                this.OldLocation = oldLocation;
                this.NewLocation = newLocation;
            }
        }

        class NotifyMovementData
        {
            public List<object> MovedPieces { get; set; }
            public NotifyMovementData(List<(ChessPiece piece, (char, int), (char, int))> movedPieces)
            {
                List<object> newMovedPieces = new List<object>();
                foreach ((ChessPiece piece, (char, int) oldLocation, (char, int) newLocation) in movedPieces)
                {
                    newMovedPieces.Add(new List<object>() { new PieceData(piece), LocationToList(oldLocation), LocationToList(newLocation) });
                }
                this.MovedPieces = newMovedPieces;
            }
        }

        class MovementOptionsData
        {
            public string Nickname { get; set; }
            public List<Dictionary<string, List<object>>> MovementOptions { get; set; }
            public MovementOptionsData(string nickname, Dictionary<(char, int), List<(char, int)>> movementOptions)
            {
                List<Dictionary<string, List<object>>> newMovementOptions = new List<Dictionary<string, List<object>>>();
                foreach ((char, int) location in movementOptions.Keys)
                {
                    Dictionary<string, List<object>> movement = new Dictionary<string, List<object>>();
                    movement["from"] = LocationToList(location);
                    movement["to"] = LocationsToList(movementOptions[location]);
                    newMovementOptions.Add(movement);
                }
                this.MovementOptions = newMovementOptions;
                this.Nickname = nickname;
            }
        }

        class PieceData
        {
            public string Color { get; set; }
            public string Type { get; set; }
            public PieceData(ChessPiece piece)
            {
                this.Color = ChessColorToString(piece.color);
                this.Type = ChessPieceToString(piece);
            }
        }

        class Message
        {
            public string Category { get; set; }
            public Message(string Category) { this.Category = Category; }
        }
        class RecievedMessage : Message
        {
            public string DataStr { get; set; }
            private JsonElement data;
            public JsonElement Data { get { return this.data; } set { this.data = value; this.DataStr = JsonSerializer.Serialize(value); } }
            public RecievedMessage(string Category, JsonElement Data) : base(Category) { this.Data = Data; }
        }
        class UpdatePlayersMessage : Message
        {
            public PlayersData Data { get; set; }
            public UpdatePlayersMessage(string Category, PlayersData Data) : base(Category) { this.Data = Data; }
        }
        class MovementOptionsMessage : Message
        {
            public MovementOptionsData Data { get; set; }
            public MovementOptionsMessage(string Category, MovementOptionsData Data) : base(Category) { this.Data = Data; }
        }
        class EndGameMessage : Message
        {
            public EndGameData Data { get; set; }
            public EndGameMessage(string Category, EndGameData Data) : base(Category) { this.Data = Data; }
        }
        class NotifyMovementMessage : Message
        {
            public NotifyMovementData Data { get; set; }
            public NotifyMovementMessage(string Category, NotifyMovementData Data) : base(Category) { this.Data = Data; }
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

    public class ClientDisconnectedException : Exception
    {
        public ClientDisconnectedException() : base() { }
        public ClientDisconnectedException(string message) : base(message) { }
    }
}