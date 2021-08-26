using System;
using System.Collections.Generic;

using Players;
using Movement;

namespace ChessBoard
{
    class Board
    {
        private ChessPiece[,] locationBoard;
        private List<Type> firstRowPiecesOrder = new List<Type> { typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook) };
        private List<Type> secondRowPiecesOrder = new List<Type> { typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn) };
        private string playerNumberErrorMessage = "Chess only supports 2 or 3 players";

        public Dictionary<ChessColor, List<ChessPiece>> chessPiecesByColor;
        public Dictionary<ChessColor, King> kingByColor = new Dictionary<ChessColor, King>();

        public Board(int numberOfPlayers)
        {
            this.chessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>();
            switch (numberOfPlayers)
            {
                case 2:
                    SetupPlayerBoard(new List<PlayerNumber>() { PlayerNumber.FirstPlayer, PlayerNumber.SecondPlayer });
                    break;
                case 3:
                    SetupPlayerBoard(new List<PlayerNumber>() { PlayerNumber.FirstPlayer, PlayerNumber.SecondPlayer, PlayerNumber.ThirdPlayer });
                    break;
                default:
                    throw new ArgumentException(playerNumberErrorMessage);
            }
        }

        private void SetupPlayerBoard(List<PlayerNumber> playerNumbers)
        {
            foreach (PlayerNumber playerNumber in playerNumbers)
            {
                ChessColor color = PlayerColor.GetColor(playerNumber);
                MovementBoard movementBoard;

                switch (playerNumbers.Count)
                {
                    case 2:
                        movementBoard = new TwoPlayerMovementBoard(color);
                        break;
                    case 3:
                        movementBoard = new ThreePlayerMovementBoard(color);
                        break;
                    default:
                        throw new ArgumentException(playerNumberErrorMessage);
                }

                if (this.locationBoard == default(ChessPiece[,]))
                {
                    this.locationBoard = new ChessPiece[movementBoard.MaxNumber, movementBoard.MaxLetter];
                }
                this.CreateChessPieces(movementBoard);
            }
        }

        private void CreateChessPieces(MovementBoard movementBoard)
        {
            ChessColor color = movementBoard.PlayerColor;
            chessPiecesByColor[color] = new List<ChessPiece>();
            List<ChessPiece> chessPieces = chessPiecesByColor[color];

            for (int row = 0; row < 2; row++)
            {
                List<(char, int)> firstRowStartingLocations = movementBoard.GetRowStartingLocations(row);
                for (int i = 0; i < firstRowStartingLocations.Count; i++)
                {
                    Type pieceType = this.firstRowPiecesOrder[i];
                    ChessPiece piece = Activator.CreateInstance(pieceType, new object[] { firstRowStartingLocations[i], color, this, movementBoard }) as ChessPiece;
                    chessPieces.Add(piece);

                    if (piece is King)
                    {
                        this.kingByColor[color] = (King)piece;
                    }
                }
            }
        }

        public Dictionary<ChessPiece, List<(char, int)>> GetColorMovementOption(ChessColor color)
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
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = this.GetColorMovementOption(color);
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