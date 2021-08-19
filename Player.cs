using System;
using System.Collections.Generic;

namespace Players
{
    enum PieceColor { White, Black, Green };
    enum PlayerNumber { FirstPlayer, SecondPlayer, ThirdPlayer };

    static class PlayerColor
    {
        public static Dictionary<PlayerNumber, PieceColor> PieceColorByPlayer = new Dictionary<PlayerNumber, PieceColor>()
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

    class Player
    {
        private PlayerNumber playerNumber;
        private PieceColor color;

        public int numberOfWins;
        public int numberOfloses;

        public Player(string nickname, PlayerNumber playerNumber)
        {
            this.Nickname = nickname;
            this.PlayerNumber = playerNumber;
            
            this.numberOfWins = 0;
            this.numberOfloses = 0;
        }

        public PieceColor Color { get { return this.color; } }

        public string Nickname {get; set;}

        public PlayerNumber PlayerNumber
        {
            get { return this.playerNumber; }
            set
            {
                this.playerNumber = value;
                this.color = PlayerColor.GetColor(value);
            }
        }

        public void AddWin()
        {
            numberOfWins++;
        }

        public void AddLose()
        {
            numberOfloses++;
        }

    }
}