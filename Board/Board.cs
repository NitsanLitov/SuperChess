using System;
using System.Collections.Generic;

using Players;

namespace ChessBoard
{
    class Board
    {
        public Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        public Dictionary<ChessColor, King> kingByColor = new Dictionary<ChessColor, King>();
        private int numberOfPlayers;

        public TempBoard tempBoard;

        public Board(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
            this.tempBoard = new TempBoard(this);

            this.chessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>();
            PiecesSetup.Setup(this);
        }

        public ChessPiece[,] LocationBoard { get; set; }

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
            return this.LocationBoard[location.Item2 - 1, location.Item1 - 'a'];
        }

        public void SetPieceByLocation(ChessPiece piece, (char, int) location)
        {
            this.LocationBoard[location.Item2 - 1, location.Item1 - 'a'] = piece;
        }

        public void Move((char, int) oldLocation, (char, int) newLocation)
        {
            GetPieceByLocation(oldLocation).Move(newLocation);
        }

        private void ForceMove(ChessPiece piece, (char, int) newLocation)
        {
            piece.ForceMove(newLocation);
        }

        public bool KingWillBeThreatened(ChessPiece piece, (char, int) newLocation)
        {
            tempBoard.Save();

            this.ForceMove(piece, newLocation);
            bool kingWillBeThreatened = IsKingThreatened(piece.color);

            tempBoard.Reverse();
            return kingWillBeThreatened;
        }
    }
}
