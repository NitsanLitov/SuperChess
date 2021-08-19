using System;
using System.Collections.Generic;

namespace Players
{
    public enum ChessColor { White, Black, Green };
    public enum PlayerNumber { FirstPlayer, SecondPlayer, ThirdPlayer };

    static class PlayerColor
    {
        private static Dictionary<PlayerNumber, ChessColor> ChessColorByPlayer = new Dictionary<PlayerNumber, ChessColor>()
        {
            {PlayerNumber.FirstPlayer, ChessColor.White},
            {PlayerNumber.SecondPlayer, ChessColor.Black},
            {PlayerNumber.ThirdPlayer, ChessColor.Green}
        };

        public static ChessColor GetColor(PlayerNumber playerNumber)
        {
            return ChessColorByPlayer[playerNumber];
        }
    }
}