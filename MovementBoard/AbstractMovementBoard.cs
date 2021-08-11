using System;
using System.Collections.Generic;

using Player;

namespace MovementBoard
{
    abstract class MovementBoard
    {
        public Dictionary<ChessColor, (char, int)[,]> MovementBoardByChessColor;
        public ChessColor[] MovementChessColorByNumber;
        public ChessColor playerColor;
        public int maxNumber;
        public char maxLetter;

        public MovementBoard(ChessColor playerColor, char maxLetter, int maxNumber)
        {
            this.playerColor = playerColor;
            this.maxNumber = maxNumber;
            this.maxLetter = maxLetter;
            this.MovementBoardByChessColor = new Dictionary<ChessColor, (char, int)[,]>();
            this.MovementChessColorByNumber = new ChessColor[this.maxNumber];
            this.SetupMovementBoard();
        }
        public abstract void SetupMovementBoard();

        public List<(char, int)> Up((char, int) currentLocation) { return this.Up(currentLocation, this.maxNumber - 1); }
        public List<(char, int)> Down((char, int) currentLocation) { return this.Down(currentLocation, this.maxNumber - 1); }
        public List<(char, int)> Right((char, int) currentLocation) { return this.Right(currentLocation, this.maxLetter - 1); }
        public List<(char, int)> Left((char, int) currentLocation) { return this.Left(currentLocation, this.maxLetter - 1); }
        public List<List<(char, int)>> Diagonal((char, int) currentLocation) { return this.Diagonal(currentLocation, this.maxNumber - 1); }

        public abstract List<(char, int)> Up((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Down((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Right((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Left((char, int) currentLocation, int maxSteps);
        public abstract List<List<(char, int)>> Diagonal((char, int) currentLocation, int maxSteps);
        public abstract List<(char, int)> Knight((char, int) currentLocation);

        public abstract (int, int, ChessColor) ConvertToMovementLocation((char, int) location);
        
        protected List<(char, int)> GetMovementOptions((int, int, ChessColor) movementLocation, int maxSteps, int rowDiff, int colDiff)
        {
            (char, int)[,] movementBoard = this.MovementBoardByChessColor[movementLocation.Item3];

            if (movementLocation.Item3 != this.playerColor)
            {
                rowDiff *= -1;
                colDiff *= -1;
            }

            List<(char, int)> locations = new List<(char, int)>();
            int newRow = movementLocation.Item1;
            int newCol = movementLocation.Item2;
            for (int i = 0; i < maxSteps; i++)
            {
                newRow += rowDiff;
                if (newRow >= movementBoard.GetLength(0) || newRow < 0) { break; }
                newCol += colDiff;
                if (newCol >= movementBoard.GetLength(1) || newCol < 0) { break; }

                locations.Add(movementBoard[newRow, newCol]);
            }
            return locations;
        }
    }
}
