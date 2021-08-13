using System;
using System.Collections.Generic;

using Player;

namespace MovementBoard
{
    class ThreePlayerMovementBoard : MovementBoard
    {
        public ThreePlayerMovementBoard(ChessColor playerColor) : base(playerColor, 'l', 12) { }

        public override void SetupMovementBoard()
        {
            AddWhiteMovementBoard();
            AddBlackMovementBoard();
            AddGreenMovementBoard();
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
            // Front side
            (char, int)[,] whiteMovementBoard = new (char, int)[8, 16];

            // First 4 rows
            for (int number = 1; number <= 4; number++)
            {
                for (char letter = 'a'; letter <= 'h'; letter++)
                {
                    whiteMovementBoard[number - 1, letter - 'a'] = (letter, number);
                    whiteMovementBoard[number - 1, letter - 'a' + 8] = (letter, number);
                }
            }

            // Upward movement for both halfs
            for (int number = 5; number <= 8; number++)
            {
                for (char letter = 'a'; letter <= 'd'; letter++)
                {
                    whiteMovementBoard[number - 1, letter - 'a'] = (letter, number);
                    whiteMovementBoard[number - 1, letter - 'a' + 8 + 4] = ((char)(letter + 4), number + 4);
                }
            }

            // Fifth diagonal
            for (char letter = 'i'; letter <= 'l'; letter++)
            {
                int row = letter - 'i' + 4;
                whiteMovementBoard[row, row] = (letter, row + 1);
                whiteMovementBoard[row, 15 - row] = (letter, row + 5);
            }
            this.MovementBoardByChessColor.Add(ChessColor.White, whiteMovementBoard);

            for (int number = 0; number <= 3; number++)
            {
                this.MovementChessColorByNumber[number] = ChessColor.White;
            }
        }

        private void AddBlackMovementBoard()
        {
            // Right side
            (char, int)[,] blackMovementBoard = new (char, int)[8, 16];

            // First 4 rows
            for (int number = 12; number >= 9; number--)
            {
                for (char letter = 'h'; letter >= 'e'; letter--)
                {
                    blackMovementBoard[12 - number, 'h' - letter] = (letter, number);
                    blackMovementBoard[12 - number, 'h' - letter + 8] = (letter, number);
                }
                for (char letter = 'i'; letter <= 'l'; letter++)
                {
                    blackMovementBoard[12 - number, letter - 'i' + 4] = (letter, number);
                    blackMovementBoard[12 - number, letter - 'i' + 4 + 8] = (letter, number);
                }
            }

            // Upward movement for both halfs
            for (int number = 4; number >= 1; number--)
            {
                for (char letter = 'h'; letter >= 'e'; letter--)
                {
                    blackMovementBoard[8 - number, 'h' - letter] = (letter, number);
                }
            }
            for (int number = 5; number <= 8; number++)
            {
                for (char letter = 'i'; letter <= 'l'; letter++)
                {
                    blackMovementBoard[number - 1, letter - 'i' + 4 + 8] = (letter, number);
                }
            }

            // Fifth diagonal
            for (char letter = 'd'; letter >= 'a'; letter--)
            {
                int row = 'd' - letter + 4;
                blackMovementBoard[row, row] = (letter, 8 - row);
                blackMovementBoard[row, 15 - row] = (letter, row + 1);
            }
            this.MovementBoardByChessColor.Add(ChessColor.Black, blackMovementBoard);

            for (int number = 11; number >= 8; number--)
            {
                this.MovementChessColorByNumber[number] = ChessColor.Black;
            }
        }

        private void AddGreenMovementBoard()
        {
            // Left side
            (char, int)[,] greenMovementBoard = new (char, int)[8, 16];

            // First 4 rows
            for (int number = 8; number >= 5; number--)
            {
                for (char letter = 'l'; letter >= 'i'; letter--)
                {
                    greenMovementBoard[8 - number, 'l' - letter] = (letter, number);
                    greenMovementBoard[8 - number, 'l' - letter + 8] = (letter, number);

                    greenMovementBoard[8 - number, 'l' - letter + 4] = ((char)(letter - 8), number);
                    greenMovementBoard[8 - number, 'l' - letter + 8 + 4] = ((char)(letter - 8), number);
                }
            }

            // Upward movement for both halfs
            for (int number = 9; number <= 12; number++)
            {
                for (char letter = 'l'; letter >= 'i'; letter--)
                {
                    greenMovementBoard[number - 5, 'l' - letter] = (letter, number);
                }
            }
            for (int number = 4; number >= 1; number--)
            {
                for (char letter = 'd'; letter >= 'a'; letter--)
                {
                    greenMovementBoard[8 - number, 'd' - letter + 4 + 8] = (letter, number);
                }
            }

            // Fifth diagonal
            for (char letter = 'e'; letter <= 'h'; letter++)
            {
                int row = letter - 'e' + 4;
                greenMovementBoard[row, row] = (letter, row + 5);
                greenMovementBoard[row, 15 - row] = (letter, 8 - row);
            }
            this.MovementBoardByChessColor.Add(ChessColor.Green, greenMovementBoard);

            for (int number = 4; number <= 7; number++)
            {
                this.MovementChessColorByNumber[number] = ChessColor.Green;
            }
        }
    }
}
