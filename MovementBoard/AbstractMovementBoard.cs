using System;
using System.Collections.Generic;

using Players;

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
        public List<List<(char, int)>> ConvertListToDoubleList(List<(char, int)> locations)
        {
            List<List<(char, int)>> finalLocations = new List<List<(char, int)>>();
            foreach ((char, int) location in locations) finalLocations.Add(new List<(char, int)>{location});

            return finalLocations;
        }

        public abstract (int, int, ChessColor) ConvertToMovementLocation((char, int) location);
        public abstract bool LocationInBorderLimits(int oldRow, int oldCol, int newRow, int newCol);

        protected List<(char, int)> GetMovementOptions((int, int, ChessColor) movementLocation, int maxSteps, int rowDiff, int colDiff, bool sensitiveMovement=true)
        {
            (char, int)[,] movementBoard = this.MovementBoardByChessColor[movementLocation.Item3];

            if (sensitiveMovement && movementLocation.Item3 != this.playerColor)
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
    }
}
