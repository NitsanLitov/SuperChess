using System.Collections.Generic;

namespace Player
{
    public enum ChessColor { Black, White, Green }
    
    public static class PlayerNumbers
    {
        public enum PlayerNumber { FirstPlayer, SecondPlayer, ThirdPlayer }
        public static Dictionary<PlayerNumber, ChessColor> ChessColorByPlayer = new Dictionary<PlayerNumber, ChessColor>()
        {
            {PlayerNumber.FirstPlayer, ChessColor.White},
            {PlayerNumber.SecondPlayer, ChessColor.Black},
            {PlayerNumber.ThirdPlayer, ChessColor.Green}
        };
    }
}
