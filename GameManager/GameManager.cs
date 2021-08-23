using System;
using System.Collections.Generic;

using Players;
using ChessBoard;
using GameServer;
using ChessPieces;

namespace GameManager
{
    class GameManager
    {
        const string CHECKMATE = "checkmate";
        const string PAT = "pat";

        public Player[] players;
        private Board board;
        private Server server;
        private Dictionary<string, Player> EndGameOptions;

        public GameManager(Server server, Player[] players)
        {
            this.players = players;
            this.server = server;
            this.board = new Board(players.Length);

            this.EndGameOptions = new Dictionary<string, Player>();
        }

        void StartGame()
        {
            bool gameInProgress = true;

            while (gameInProgress)
            {
                foreach (Player p in players)
                {
                    Dictionary<(char, int), List<(char, int)>> movements = this.board.GetColorMovementOptions(p.Color);
                    if (movements.Keys.Count == 0)
                    {
                        // ToDo: when there is more then 2 players state which player won
                        if (this.board.IsKingThreatened(p.Color))
                            this.EndGameOptions[CHECKMATE] = p;
                        else
                            this.EndGameOptions[PAT] = null;

                        this.server.EndGame(EndGameOptions);
                        gameInProgress = false;
                        break;
                    }
                    this.server.UpdateMovementsOptions(p, movements);

                    (ChessPiece piece, (char, int) newLocation) = this.server.GetMovedPiece(p);
                    this.board.Move(piece, newLocation);

                    this.server.NotifyMovementToAll();
                }
                if (!gameInProgress) break;
            }
        }
    }
}