using System;
using System.Collections.Generic;

namespace SuperChess
{
    enum PieceColor { Black, White, Green };

    class Player
    {
        public enum PlayerNumber { FirstPlayer, SecondPlayer, ThirdPlayer }
        public static Dictionary<PlayerNumber, PieceColor> PieceColorByPlayer = new Dictionary<PlayerNumber, PieceColor>();

        public string nickname;
        public int number_Of_Wins;
        public int number_Of_loses;
       

        public Player(string nickname)
        {
            this.nickname = nickname;
            this.number_Of_Wins = 0;
            this.number_Of_loses = 0;
            InitPlayerColor();
        }

        private void InitPlayerColor()
        {
            PieceColorByPlayer.Add(PlayerNumber.FirstPlayer, PieceColor.White);
            PieceColorByPlayer.Add(PlayerNumber.SecondPlayer, PieceColor.Black);
            PieceColorByPlayer.Add(PlayerNumber.ThirdPlayer, PieceColor.Green);
        }
    
        public void AddWin()
        {
            number_Of_Wins++;
        }

        public void AddLose()
        {
            number_Of_loses++;
        }
    }
}
