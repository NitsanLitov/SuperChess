using System;
using System.Collections.Generic;

namespace SuperChess
{
    class Player
    {
        private static int playerCount = 0;
        private enum PlayerColor { Black, White, Green };
        public int color;
        public string nickname;
        public ChessPiece[] pieces; // 16 pieces per player
        public ChessPiece king; // I wasent sure what to do with the king...


        Player(string nickname) // public
        {
            this.nickname = nickname;
            this.pieces = new ChessPiece[16];
            playerCount++;
            this.SetColor(playerCount);
        }

        ~Player()
        {
            this.pieces = null;
            GC.Collect();
        }

        private void SetColor(int playerCount)
        {
            if(playerCount == 1) this.color = (int)PlayerColor.White;
            if(playerCount == 2) this.color = (int)PlayerColor.Black;
            else this.color = (int)PlayerColor.Green;
        }

        public void AddPiece(ChessPiece piece)
        {
            int pieces_count = this.pieces.Length;
            this.pieces[pieces_count] = piece;
        }

        public void RemovePiece(ChessPiece piece)
        {
            int pieces_count = this.pieces.Length;

            for( int i = 0; i < pieces_count; i++ )
                if( this.pieces[i] == piece )
                    this.pieces[i] = null;
        }
    }
}