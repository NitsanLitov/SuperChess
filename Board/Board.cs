using System;
using System.Collections.Generic;

using Players;
using Movement;

namespace ChessBoard
{
    class Board
    {
        ChessPiece[,] locationBoard;
        List<Type> firstRowPiecesOrder = new List<Type> { typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook) };
        List<Type> secondRowPiecesOrder = new List<Type> { typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn) };

        Dictionary<ChessColor, List<ChessPiece>> ChessPiecesByColor;
        Dictionary<ChessColor, King> KingByColor = new Dictionary<ChessColor, King>();

        string playerNumberErrorMessage = "Chess only supports 2 or 3 players";

        public Board(int numberOfPlayers)
        {
            this.ChessPiecesByColor = new Dictionary<ChessColor, List<ChessPiece>>();
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
            ChessPiecesByColor[color] = new List<ChessPiece>();
            List<ChessPiece> chessPieces = ChessPiecesByColor[color];

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
                        this.KingByColor[color] = (King)piece;
                    }
                }
            }
        }

        public Dictionary<ChessPiece, List<(char, int)>> GetColorMovementOption(ChessColor color)
        {
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = new Dictionary<ChessPiece, List<(char, int)>>();
            foreach (ChessPiece piece in this.ChessPiecesByColor[color])
            {
                colorMovementOption[piece] = piece.GetMovementOption();
            }
            return colorMovementOption;
        }

        public bool IsKingThreatened(ChessColor color)
        {
            Dictionary<ChessPiece, List<(char, int)>> colorMovementOption = this.GetColorMovementOption(color);
            foreach (List<(char, int)> movementOptions in colorMovementOption.Values)
            {
                if (movementOptions.Contains(this.KingByColor[color].location)) return true;
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
    }
}