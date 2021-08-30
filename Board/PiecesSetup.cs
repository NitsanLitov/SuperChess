using System;
using System.Linq;
using System.Collections.Generic;

using Players;
using Movement;

namespace ChessBoard
{
    static class PiecesSetup
    {
        private static List<Type> firstRowPiecesOrder = new List<Type> { typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook) };
        private static List<Type> secondRowPiecesOrder = new List<Type> { typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn) };
        private static List<Type>[] twoRowsPiecesOrder = new List<Type>[piecesRowsCount]{firstRowPiecesOrder, secondRowPiecesOrder};
        private const int piecesRowsCount = 2;

        private static Dictionary<int, Dictionary<ChessColor, List<Type>[]>> piecesOrder = new Dictionary<int, Dictionary<ChessColor, List<Type>[]>>{
            {3, new Dictionary<ChessColor, List<Type>[]>{
                {ChessColor.White, twoRowsPiecesOrder},
                {ChessColor.Black, twoRowsPiecesOrder},
                {ChessColor.Green, twoRowsPiecesOrder}
            }},
            {2, new Dictionary<ChessColor, List<Type>[]>{
                {ChessColor.White, twoRowsPiecesOrder},
                {ChessColor.Black, new List<Type>[piecesRowsCount]{Enumerable.Reverse(firstRowPiecesOrder).ToList(), secondRowPiecesOrder}},
            }},
        };

        static private string playerNumberErrorMessage = "Chess only supports 2 or 3 players";

        static public ChessPiece[,] CreateLocationBoard(Board board)
        {
            switch (board.NumberOfPlayers)
            {
                case 2:
                    return new ChessPiece[TwoPlayerMovementBoard.maxNumber, TwoPlayerMovementBoard.maxLetter];
                case 3:
                    return new ChessPiece[ThreePlayerMovementBoard.maxNumber, ThreePlayerMovementBoard.maxLetter];
                default:
                    throw new ArgumentException(playerNumberErrorMessage);
            }
        }

        static public void SetupPlayerBoard(Board board)
        {
            List<PlayerNumber> playerNumbers;
            Type movementType;

            switch (board.NumberOfPlayers)
            {
                case 2:
                    playerNumbers = new List<PlayerNumber>() { PlayerNumber.FirstPlayer, PlayerNumber.SecondPlayer };
                    movementType = typeof(TwoPlayerMovementBoard);
                    break;
                case 3:
                    playerNumbers = new List<PlayerNumber>() { PlayerNumber.FirstPlayer, PlayerNumber.SecondPlayer, PlayerNumber.ThirdPlayer };
                    movementType = typeof(ThreePlayerMovementBoard);
                    break;
                default:
                    throw new ArgumentException(playerNumberErrorMessage);
            }

            foreach (PlayerNumber playerNumber in playerNumbers)
            {
                ChessColor color = PlayerColor.GetColor(playerNumber);
                MovementBoard movementBoard = Activator.CreateInstance(movementType, new object[] { color }) as MovementBoard;

                CreateChessPieces(board, movementBoard);
            }
        }

        static private void CreateChessPieces(Board board, MovementBoard movementBoard)
        {
            ChessColor color = movementBoard.PlayerColor;
            List<ChessPiece> chessPieces = new List<ChessPiece>();
            board.chessPiecesByColor[color] = chessPieces;

            for (int row = 0; row < piecesRowsCount; row++)
            {
                List<(char, int)> rowStartingLocations = movementBoard.GetRowStartingLocations(row);
                for (int i = 0; i < rowStartingLocations.Count; i++)
                {
                    Type pieceType = piecesOrder[board.NumberOfPlayers][color][row][i];

                    ChessPiece piece = Activator.CreateInstance(pieceType, new object[] { rowStartingLocations[i], color, board, movementBoard }) as ChessPiece;
                    chessPieces.Add(piece);

                    if (piece is King)
                    {
                        board.kingByColor[color] = (King)piece;
                    }
                }
            }
        }
    }
}