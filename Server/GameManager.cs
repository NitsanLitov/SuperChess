using System.Collections.Generic;

using Players;
using ChessBoard;

namespace Communication
{
    public class GameManager
    {
        const string CHECKMATE = "checkmate";
        const string PAT = "pat";

        public Player[] players;
        private Board board;
        private GameServer server;

        public GameManager(GameServer server, List<string> playersNicknames)
        {
            this.server = server;
            this.board = new Board(playersNicknames.Count);
            
            this.players = new Player[playersNicknames.Count];
            PlayerNumber[] playerNumbers = new PlayerNumber[]{PlayerNumber.FirstPlayer, PlayerNumber.SecondPlayer, PlayerNumber.ThirdPlayer};
            for (int i = 0; i < playersNicknames.Count; i++)
            {
                this.players[i] = new Player(playersNicknames[i], playerNumbers[i]);
            }
        }

        public void StartGame()
        {
            bool gameInProgress = true;
            List<(ChessPiece, (char, int), (char, int))> startingLocations = new List<(ChessPiece, (char, int), (char, int))>();
            foreach (ChessColor color in this.board.chessPiecesByColor.Keys)
            {
                foreach (ChessPiece piece in this.board.chessPiecesByColor[color])
                {
                    startingLocations.Add((piece, piece.location, piece.location));
                }
            }
            this.server.NotifyMovementToAll(startingLocations);

            while (gameInProgress)
            {
                foreach (Player p in players)
                {
                    Dictionary<ChessPiece, List<(char, int)>> movements = this.board.GetColorMovementOptions(p.Color);
                    if (movements.Keys.Count == 0)
                    {
                        // ToDo: when there is more then 2 players state which player won
                        if (this.board.IsKingThreatened(p.Color))
                            this.server.EndGame(CHECKMATE, p.Nickname);
                        else
                            this.server.EndGame(PAT);

                        gameInProgress = false;
                        break;
                    }
                    this.server.UpdateMovementOptions(p.Nickname, movements);

                    ((char, int) oldLocation, (char, int) newLocation) = this.server.GetMovedPiece(p.Nickname);
                    List<(ChessPiece, (char, int), (char, int))> movedPieces = this.board.Move(oldLocation, newLocation);

                    this.server.NotifyMovementToAll(movedPieces);
                }
                if (!gameInProgress) break;
            }
        }
    }
}