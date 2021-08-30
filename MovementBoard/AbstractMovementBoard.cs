using System;
using System.Collections.Generic;

using Players;

namespace Movement
{
    abstract class MovementBoard
    {
        protected Dictionary<ChessColor, (char, int)[,]> MovementBoardByChessColor;
        protected ChessColor[] MovementChessColorByNumber;
        private int maxNumber;
        private char maxLetter;

        public MovementBoard(ChessColor playerColor, char maxLetter, int maxNumber)
        {
            this.PlayerColor = playerColor;
            this.maxNumber = maxNumber;
            this.maxLetter = maxLetter;
            this.MovementBoardByChessColor = new Dictionary<ChessColor, (char, int)[,]>();
            this.MovementChessColorByNumber = new ChessColor[this.MaxNumber];
            this.SetupMovementBoard();
        }
        public abstract void SetupMovementBoard();

        public ChessColor PlayerColor { get; set; }
        public int MaxNumber { get { return this.maxNumber; } }
        public char MaxLetter { get { return this.maxLetter; } }

        public List<(char, int)> Up((char, int) currentLocation) { return this.Up(currentLocation, -1); }
        public List<(char, int)> Down((char, int) currentLocation) { return this.Down(currentLocation, -1); }
        public List<(char, int)> Right((char, int) currentLocation) { return this.Right(currentLocation, -1); }
        public List<(char, int)> Left((char, int) currentLocation) { return this.Left(currentLocation, -1); }
        public List<List<(char, int)>> Diagonal((char, int) currentLocation) { return this.Diagonal(currentLocation, -1); }

        public abstract List<(char, int)> Up((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Down((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Right((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Left((char, int) currentLocation, int maxSteps);
        public abstract List<List<(char, int)>> Diagonal((char, int) currentLocation, int maxSteps);
        public abstract List<List<(char, int)>> Knight((char, int) currentLocation);

        protected abstract (int, int, ChessColor) ConvertToMovementLocation((char, int) location);
        protected abstract bool LocationInBorderLimits(int oldRow, int oldCol, int newRow, int newCol);

        protected List<(char, int)> GetMovementOptions((int, int, ChessColor) movementLocation, int maxSteps, int rowDiff, int colDiff, bool sensitiveMovement = true)
        {
            (char, int)[,] movementBoard = this.MovementBoardByChessColor[movementLocation.Item3];

            if (sensitiveMovement && movementLocation.Item3 != this.PlayerColor)
            {
                rowDiff *= -1;
                colDiff *= -1;
            }

            List<(char, int)> locations = new List<(char, int)>();
            int newRow = movementLocation.Item1;
            int newCol = movementLocation.Item2;

            // maxSteps!=0 and not maxSteps>0 because if maxsteps is -1 it will run until out of border
            while (maxSteps != 0)
            {
                if (!LocationInBorderLimits(newRow, newCol, newRow += rowDiff, newCol += colDiff)) { break; }
                locations.Add(movementBoard[newRow, newCol]);

                maxSteps--;
            }
            return locations;
        }

        public List<(char, int)> GetRowStartingLocations(int row)
        {
            List<(char, int)> startingLocations = new List<(char, int)>();
            for (int j = 0; j < 8; j++)
            {
                startingLocations.Add(this.MovementBoardByChessColor[this.PlayerColor][row, j]);
            }
            return startingLocations;
        }
    }
}
