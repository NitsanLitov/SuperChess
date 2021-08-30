using System;
using System.Collections.Generic;

using Players;
using Movement;

namespace ChessBoard
{
    class Board
    {
        private ChessPiece[,] locationBoard;

        public Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        public Dictionary<ChessColor, King> kingByColor = new Dictionary<ChessColor, King>();
        private int numberOfPlayers;

        public Board(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;

            this.chessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>();
            this.locationBoard = PiecesSetup.CreateLocationBoard(this);
            PiecesSetup.SetupPlayerBoard(this);
        }

        public int NumberOfPlayers { get { return this.numberOfPlayers; } }

        public Dictionary<ChessPiece, List<(char, int)>> GetColorMovementOptions(ChessColor color)
        {
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = new Dictionary<ChessPiece, List<(char, int)>>();
            foreach (ChessPiece piece in this.chessPiecesByColor[color])
            {
                colorMovementOption[piece] = piece.GetMovementOptions();
            }
            return colorMovementOption;
        }

        public bool IsKingThreatened(ChessColor color)
        {
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = this.GetColorMovementOptions(color);
            foreach (List<(char, int)> movementOptions in colorMovementOption.Values)
            {
                if (movementOptions.Contains(this.kingByColor[color].location)) return true;
            }
            return false;
        }

        public ChessPiece GetPieceByLocation((char, int) location)
        {
            return this.locationBoard[location.Item2 - 1, location.Item1 - 'a'];
        }

        public void SetPieceByLocation(ChessPiece piece, (char, int) location)
        {
            this.locationBoard[location.Item2 - 1, location.Item1 - 'a'] = piece;
        }

        public void Move((char, int) oldLocation, (char, int) newLocation)
        {
            GetPieceByLocation(oldLocation).Move(newLocation);
        }

        public bool KingWillBeThreatened(ChessPiece piece, (char, int) location)
        {
            return false;
        }
    }
}