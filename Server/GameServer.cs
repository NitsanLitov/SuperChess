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
        private ChessLobbyServer server;
        public TcpClient client;
        private GameManager gameManager;

        const string START_GAME_CATEGORY = "Start";
        const string UPDATE_PLAYERS_CATEGORY = "Players";
        const string UPDATE_MOVEMENT_CATEGORY = "UpdateMovementOptions";
        const string END_GAME_CATEGORY = "EndGame";
        const string MOVED_PIECE_CATEGORY = "MovedPiece";
        const string NOTIFY_MOVEMENT_CATEGORY = "NotifyMovementToAll";

        public GameServer(ChessLobbyServer server, TcpClient client)
        {
            this.server = server;
            this.client = client;
        }

        public void Start()
        {
            try
            {
                RecievedMessage message = this.Read();
                ValidateMessageCategory(message, START_GAME_CATEGORY);
                StartData startData = JsonSerializer.Deserialize<StartData>(message.dataStr);

                this.gameManager = new GameManager(this, startData.nicknames);

                this.UpdatePlayers();

                this.gameManager.StartGame();
            }
            catch (Exception e)
            {
                if (this.ClientConnected(this.client)) { this.EndGame($"Server error of type ({e.GetType()}) has occured, the game is finished"); }
                Console.WriteLine(e);
            }
            finally
            {
                this.Stop();
            }
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

        public void EndGame(string reason)
        {
            this.EndGame(reason, "");
        }

        public void EndGame(string reason, string nickname)
        {
            EndGameData data = new EndGameData(reason, nickname);
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
            MovedPieceData movedPieceData = JsonSerializer.Deserialize<MovedPieceData>(message.dataStr);

            // instead while until nickname is the right choise... send error message containing the error
            if (movedPieceData.nickname != nickname) throw new BadNicknameException(nickname, movedPieceData.nickname);

            return (movedPieceData.oldLocationTuple, movedPieceData.newLocationTuple);
        }

        private RecievedMessage Read()
        {
            if (!this.ClientConnected(this.client)) throw new ClientDisconnectedException();

            Console.WriteLine("Reading");

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

        private void Send(Message message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(EndGameMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(UpdatePlayersMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(NotifyMovementMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(MovementOptionsMessage message) { this.Send(JsonSerializer.Serialize(message)); }

        private void Send(string data)
        {
            if (!this.ClientConnected(this.client)) throw new ClientDisconnectedException();

            NetworkStream stream = this.client.GetStream();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            stream.Write(msg, 0, msg.Length);
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
        }

        private void ValidateMessageCategory(Message message, string category)
        {
            if (message.category != category) throw new MessageCategoryException(category, message.category);
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

        private static string LocationToStr((char, int) location)
        {
            return location != default ? $"{location.Item1}{location.Item2}" : "";
        }

        private static List<string> LocationsToStrList(List<(char, int)> locations)
        {
            List<string> list = new List<string>();
            foreach ((char, int) location in locations)
            {
                list.Add(LocationToStr(location));
            }
            return list;
        }

        record StartData(List<string> nicknames);
        record PlayersData(List<Dictionary<string, string>> players);
        record EndGameData(string reason, string nickname);

        class MovedPieceData
        {
            public string nickname { get; set; }
            public JsonElement internalOldLocation;
            public JsonElement internalNewLocation;
            public JsonElement oldLocation { get { return this.internalOldLocation; } set { this.internalOldLocation = value; this.oldLocationTuple = (value.GetString()[0], value.GetString()[1] - '0'); } }
            public JsonElement newLocation { get { return this.internalNewLocation; } set { this.internalNewLocation = value; this.newLocationTuple = (value.GetString()[0], value.GetString()[1] - '0'); } }
            public (char, int) oldLocationTuple { get; set; }
            public (char, int) newLocationTuple { get; set; }
            public MovedPieceData(string nickname, JsonElement oldLocation, JsonElement newLocation)
            {
                this.nickname = nickname;
                this.oldLocation = oldLocation;
                this.newLocation = newLocation;
            }
        }

        class NotifyMovementData
        {
            public List<object> movedPieces { get; set; }
            public NotifyMovementData(List<(ChessPiece piece, (char, int), (char, int))> movedPieces)
            {
                List<object> newMovedPieces = new List<object>();
                foreach ((ChessPiece piece, (char, int) oldLocation, (char, int) newLocation) in movedPieces)
                {
                    newMovedPieces.Add(new List<object>() { new PieceData(piece), LocationToStr(oldLocation), LocationToStr(newLocation) });
                }
                this.movedPieces = newMovedPieces;
            }
        }

        class MovementOptionsData
        {
            public string nickname { get; set; }
            public Dictionary<string, List<string>> movementOptions { get; set; }
            public MovementOptionsData(string nickname, Dictionary<(char, int), List<(char, int)>> movementOptions)
            {
                Dictionary<string, List<string>> newMovementOptions = new Dictionary<string, List<string>>();
                foreach ((char, int) location in movementOptions.Keys)
                {
                    newMovementOptions[LocationToStr(location)] = LocationsToStrList(movementOptions[location]);
                }
                this.movementOptions = newMovementOptions;
                this.nickname = nickname;
            }
        }

        class PieceData
        {
            public string color { get; set; }
            public string type { get; set; }
            public PieceData(ChessPiece piece)
            {
                this.color = ChessColorToString(piece.color);
                this.type = ChessPieceToString(piece);
            }
        }

        class Message
        {
            public string category { get; set; }
            public Message(string category) { this.category = category; }
        }
        class RecievedMessage : Message
        {
            public string dataStr { get; set; }
            private JsonElement internalData;
            public JsonElement data { get { return this.internalData; } set { this.internalData = value; this.dataStr = JsonSerializer.Serialize(value); } }
            public RecievedMessage(string category, JsonElement data) : base(category) { this.data = data; }
        }
        class UpdatePlayersMessage : Message
        {
            public PlayersData data { get; set; }
            public UpdatePlayersMessage(string category, PlayersData data) : base(category) { this.data = data; }
        }
        class MovementOptionsMessage : Message
        {
            public MovementOptionsData data { get; set; }
            public MovementOptionsMessage(string category, MovementOptionsData data) : base(category) { this.data = data; }
        }
        class EndGameMessage : Message
        {
            public EndGameData data { get; set; }
            public EndGameMessage(string category, EndGameData data) : base(category) { this.data = data; }
        }
        class NotifyMovementMessage : Message
        {
            public NotifyMovementData data { get; set; }
            public NotifyMovementMessage(string category, NotifyMovementData data) : base(category) { this.data = data; }
        }
    }

    public class ClientDisconnectedException : Exception
    {
        public ClientDisconnectedException() : base("The Client unexpectedly disconnected") { }
        public ClientDisconnectedException(string message) : base(message) { }
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