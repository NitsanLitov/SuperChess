using System;
using System.Collections.Generic;

using Players;

namespace ChessBoard
{
    class Board
    {
        public Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        public Dictionary<ChessColor, King> kingByColor = new Dictionary<ChessColor, King>();
        public EnPassantPawn enPassantPawn;
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
            return this.GetColorMovementOptions(color, false);
        }

        private Dictionary<ChessPiece, List<(char, int)>> GetColorMovementOptions(ChessColor color, bool canPieceTakeOpponentKing)
        {
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = new Dictionary<ChessPiece, List<(char, int)>>();
            foreach (ChessPiece piece in new List<ChessPiece>(this.chessPiecesByColor[color]))
            {
                List<(char, int)> movementOptions = piece.GetMovementOptions(canPieceTakeOpponentKing);
                if (movementOptions.Count != 0) colorMovementOption[piece] = movementOptions;
            }
            return colorMovementOption;
        }

        public bool IsKingThreatened(ChessColor color)
        {
            (char, int) kingLocation = this.kingByColor[color].location;

            foreach (ChessColor opponentColor in this.chessPiecesByColor.Keys)
            {
                if (opponentColor == color) continue;

                Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = this.GetColorMovementOptions(opponentColor, true);
                foreach (List<(char, int)> movementOptions in colorMovementOption.Values)
                {
                    if (movementOptions.Contains(kingLocation)) return true;
                }
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
            EnPassantPawn oldEnPassantPawn = this.enPassantPawn;
            GetPieceByLocation(oldLocation).Move(newLocation);

            if (oldEnPassantPawn != null && this.enPassantPawn != null)
            {
                SetPieceByLocation(null, oldEnPassantPawn.location);
                if (oldEnPassantPawn == this.enPassantPawn)
                    this.enPassantPawn = null;
            }
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
