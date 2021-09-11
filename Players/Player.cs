using System;

namespace Players
{
    public class Player
    {
        private PlayerNumber playerNumber;
        private ChessColor color;

        public int numberOfWins;
        public int numberOfloses;

        public Player(string nickname, PlayerNumber playerNumber)
        {
            this.Nickname = nickname;
            this.PlayerNumber = playerNumber;
            
            this.numberOfWins = 0;
            this.numberOfloses = 0;
        }

        public ChessColor Color { get { return this.color; } }

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

        public void AddWin() { numberOfWins++; }

        public void AddLose() { numberOfloses++; }
    }
}