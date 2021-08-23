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
        public Player[] players;
        private Board board;
        private Server server;
        private Dictionary<string, Player> EndGameOptions;

        public GameManager(Server server, Board board, Player[] players)
        {
            this.players = players;
            this.board = board;
            this.server = server;

            this.EndGameOptions = new Dictionary<string, Player>();
        }

        void StartGame()
        {
            // server - update all that a piece moved
            // server - Update the movement of specific player
            bool is_running = true;
            while (is_running)
            {
                foreach (Player p in players)
                {
                    Dictionary<(char, int), List<(char, int)>> movements = board.GetColorMovementOptions(p.Color); //piece location -> all the movement options
                    if (movements.Keys.Count == 0)
                    {
                        if (board.IsKingThreatened(p)) // checkmate
                            this.EndGameOptions["checkmate"] = p;

                        this.server.EndGame(EndGameOptions);
                        is_running = false;
                        break;
                    }
                    this.server.UpdateMovementsOptions(p, movements); // update the movement of ea player

                    (ChessPiece piece, (char, int) newLocation) = server.GetMovedPiece(p);
                    this.board.Move(piece, newLocation);

                    this.server.NotifyMovementToAll();
                }
                if (!is_running) break; 
            }
        }
    }
}