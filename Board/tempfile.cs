using System;
using System.Collections.Generic;

namespace Board
{
    public enum ChessColor { Black, White, Green }

    class EmptyPiece : ChessPiece {}
    class ChessPiece
    {
        public (char, int) location;
        public ChessColor color;
        public Board board;

        public bool specialMovement(ChessPiece piece) {return true;}

        public List<(char, int)> ProcessOptions(List<(char, int)> locations)
        {
            List<(char, int)> finalLocations = new List<(char, int)>();
            foreach ((char, int) l in locations)
            {
                ChessPiece piece =this.board.piecesLocation[l.Item2 - 1, l.Item1 - 'a'];
                if (!specialMovement(piece) && piece.color == this.color) break;
                
                if (board.KingWillBeThreatened(this, l)) continue;

                finalLocations.Add(l);
                if (piece is EmptyPiece) continue;

                break;
            }
            return finalLocations;
        }

        public List<(char, int)> GetMovementOptions()
        {
            List<(char, int)> finalLocations = new List<(char, int)>();
            finalLocations.AddRange(this.ProcessOptions(movement.Up()));
            finalLocations.AddRange(this.ProcessOptions(movement.Down()));
            foreach (List<(char, int)> locations in movement.Diag()) finalLocations.AddRange(this.ProcessOptions(locations));
            foreach (List<(char, int)> locations in movement.Knight()) finalLocations.AddRange(this.ProcessOptions(locations));

            return finalLocations;
        }


    }
}
