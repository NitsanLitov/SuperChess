using System;
using System.Collections.Generic;

namespace Players
{
    enum PieceColor { White, Black, Green };
    enum PlayerNumber { FirstPlayer, SecondPlayer, ThirdPlayer };

    static class PlayerColor
    {
        private static Dictionary<PlayerNumber, PieceColor> PieceColorByPlayer = new Dictionary<PlayerNumber, PieceColor>()
        {
            {PlayerNumber.FirstPlayer, PieceColor.White},
            {PlayerNumber.SecondPlayer, PieceColor.Black},
            {PlayerNumber.ThirdPlayer, PieceColor.Green}
        };

        public static PieceColor GetColor(PlayerNumber playerNumber)
        {
            return PieceColorByPlayer[playerNumber];
        }
    }
}