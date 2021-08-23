using System;

namespace Communication
{
    class Server
    {
        private PlayerNumber playerNumber;
        private ChessColor color;

        public int numberOfWins;
        public int numberOfloses;

        public Server()
        {
            this.Nickname = nickname;
            this.PlayerNumber = playerNumber;
            
            this.numberOfWins = 0;
            this.numberOfloses = 0;
        }
    }
}