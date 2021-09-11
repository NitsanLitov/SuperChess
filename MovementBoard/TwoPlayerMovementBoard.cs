using System;
using System.Collections.Generic;

using Players;

namespace Movement
{
    public class TwoPlayerMovementBoard : MovementBoard
    {
        public const int maxNumber = 8;
        public const char maxLetter = 'h';
        public TwoPlayerMovementBoard(ChessColor playerColor) : base(playerColor, maxLetter, maxNumber) { }

        public override void SetupMovementBoard()
        {
            AddWhiteMovementBoard();
            AddBlackMovementBoard();
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

        protected override List<List<(char, int)>> DiagonalUp((int, int, ChessColor) movementLocation, int maxSteps)
        {
            List<List<(char, int)>> locationsList = new List<List<(char, int)>>();
            locationsList.Add(GetMovementOptions(movementLocation, maxSteps, 1, 1, false));
            locationsList.Add(GetMovementOptions(movementLocation, maxSteps, 1, -1, false));
            
            return locationsList;
        }

        protected override List<List<(char, int)>> DiagonalDown((int, int, ChessColor) movementLocation, int maxSteps)
        {
            List<List<(char, int)>> locationsList = new List<List<(char, int)>>();
            locationsList.Add(GetMovementOptions(movementLocation, maxSteps, -1, 1, false));
            locationsList.Add(GetMovementOptions(movementLocation, maxSteps, -1, -1, false));
            
            return locationsList;
        }

        public override List<List<(char, int)>> Knight((char, int) currentLocation)
        {
            (int, int, ChessColor) movementLocation = ConvertToMovementLocation(currentLocation);
            
            List<List<(char, int)>> locations = new List<List<(char, int)>>();
            locations.Add(GetMovementOptions(movementLocation, 1, 1, 2));
            locations.Add(GetMovementOptions(movementLocation, 1, 1, -2));
            locations.Add(GetMovementOptions(movementLocation, 1, -1, 2));
            locations.Add(GetMovementOptions(movementLocation, 1, -1, -2));
            locations.Add(GetMovementOptions(movementLocation, 1, 2, 1));
            locations.Add(GetMovementOptions(movementLocation, 1, 2, -1));
            locations.Add(GetMovementOptions(movementLocation, 1, -2, 1));
            locations.Add(GetMovementOptions(movementLocation, 1, -2, -1));
            return locations;
        }

        protected override (int, int, ChessColor) ConvertToMovementLocation((char, int) location)
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

        protected override bool LocationInBorderLimits(int oldRow, int oldCol, int newRow, int newCol)
        {
            if (newRow >= this.MaxNumber || newRow < 0) return false;
            if (newCol >= (this.MaxLetter - 'a' + 1) || newCol < 0) return false;
            return true;
        }

        private void AddWhiteMovementBoard()
        {
            (char, int)[,] whiteMovementBoard = new (char, int)[this.MaxNumber, this.MaxLetter - 'a' + 1];
            for (int number = 0; number < this.MaxNumber; number++)
            {
                for (char letter = 'a'; letter <= this.MaxLetter; letter++)
                {
                    whiteMovementBoard[number, letter - 'a'] = (letter,number + 1);
                }
            }
            this.MovementBoardByChessColor.Add(ChessColor.White, whiteMovementBoard);

            for (int number = 0; number < this.MaxNumber; number++)
            {
                this.MovementChessColorByNumber[number] = ChessColor.White;
            }
        }

        // Used only for getting starting locations
        private void AddBlackMovementBoard()
        {
            (char, int)[,] blackMovementBoard = new (char, int)[this.MaxNumber, this.MaxLetter - 'a' + 1];
            for (int number = this.MaxNumber; number >= 1; number--)
            {
                for (char letter = this.MaxLetter; letter >= 'a'; letter--)
                {
                    blackMovementBoard[8 - number, this.MaxLetter - letter] = (letter,number);
                }
            }
            this.MovementBoardByChessColor.Add(ChessColor.Black, blackMovementBoard);
        }
    }
}
