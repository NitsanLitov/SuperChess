using System;
using System.Collections.Generic;

using Player;

namespace MovementBoard
{
    class TwoPlayerMovementBoard : MovementBoard
    {
        public TwoPlayerMovementBoard(ChessColor playerColor) : base(playerColor, 'h', 8) { }

        public override void SetupMovementBoard()
        {
            AddWhiteMovementBoard();
        }

        public override List<(char, int)> Up((char, int) currentLocation, int maxSteps)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            return GetMovementOptions(movementLocation, maxSteps, 1, 0);
        }

        public override List<(char, int)> Down((char, int) currentLocation, int maxSteps)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            return GetMovementOptions(movementLocation, maxSteps, -1, 0);
        }

        public override List<(char, int)> Right((char, int) currentLocation, int maxSteps)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            return GetMovementOptions(movementLocation, maxSteps, 0, 1);
        }

        public override List<(char, int)> Left((char, int) currentLocation, int maxSteps)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            return GetMovementOptions(movementLocation, maxSteps, 0, -1);
        }

        public override List<List<(char, int)>> Diagonal((char, int) currentLocation, int maxSteps)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            List<(char, int)> l1 = GetMovementOptions(movementLocation, maxSteps, 1, 1);
            List<(char, int)> l2 = GetMovementOptions(movementLocation, maxSteps, -1, 1);
            List<(char, int)> l3 = GetMovementOptions(movementLocation, maxSteps, -1, -1);
            List<(char, int)> l4 = GetMovementOptions(movementLocation, maxSteps, 1, -1);
            return new List<List<(char, int)>> { l1, l2, l3, l4 };
        }

        public override List<(char, int)> Knight((char, int) currentLocation)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            List<(char, int)> locations = GetMovementOptions(movementLocation, 1, 1, 2);
            locations.AddRange(GetMovementOptions(movementLocation, 1, 1, -2));
            locations.AddRange(GetMovementOptions(movementLocation, 1, -1, 2));
            locations.AddRange(GetMovementOptions(movementLocation, 1, -1, -2));
            locations.AddRange(GetMovementOptions(movementLocation, 1, 2, 1));
            locations.AddRange(GetMovementOptions(movementLocation, 1, 2, -1));
            locations.AddRange(GetMovementOptions(movementLocation, 1, -2, 1));
            locations.AddRange(GetMovementOptions(movementLocation, 1, -2, -1));
            return locations;
        }

        public override (int, int, ChessColor) ConvertToMovementLocation((char, int) location)
        {
            ChessColor movementChessColor = this.MovementChessColorByNumber[location.Item2 - 1];
            (char, int)[,] movementBoard = this.MovementBoardByChessColor[movementChessColor];
            for (int i = 0; i < movementBoard.GetLength(0); i++)
            {
                for (int j = 0; j < movementBoard.GetLength(1); j++)
                {
                    if (movementBoard[i, j] == location) return (i, j, movementChessColor);
                }
            }

            throw new ArgumentOutOfRangeException("location", "Location wasn't in the movement board");
        }

        private void AddWhiteMovementBoard()
        {
            (char, int)[,] whiteMovementBoard = new (char, int)[this.maxNumber, this.maxLetter - 'a' + 1];
            for (int number = 0; number < this.maxNumber; number++)
            {
                for (char letter = 'a'; letter <= this.maxLetter; letter++)
                {
                    whiteMovementBoard[number, letter - 'a'] = (letter,number + 1);
                }
            }
            this.MovementBoardByChessColor.Add(ChessColor.White, whiteMovementBoard);

            for (int number = 0; number < this.maxNumber; number++)
            {
                this.MovementChessColorByNumber[number] = ChessColor.White;
            }
        }
    }
}
