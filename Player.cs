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
        public ChessPiece[] pieces; // 16 pieces per player
        public ChessPiece king;


        public Player(string nickname)
        {
            this.nickname = nickname;
            this.pieces = new ChessPiece[16];
            InitPlayerColor();
        }

        ~Player()
        {
            this.pieces = null;
            GC.Collect();
        }
        
        private void InitPlayerColor()
        {
            PieceColorByPlayer.Add(PlayerNumber.FirstPlayer,PieceColor.White);
            PieceColorByPlayer.Add(PlayerNumber.SecondPlayer,PieceColor.Black);
            PieceColorByPlayer.Add(PlayerNumber.ThirdPlayer,PieceColor.Green);
        }

        public void AddPiece(ChessPiece piece)
        {
            int pieces_count = this.pieces.Length;

            if(pieces_count == 16)
                return;

            // King - The Class
            if(piece == King) // check if the piece is King Type
            {
                if(this.king == null) // check if theres already king
                    this.king = piece;
                else
                    return;
            }

            for (int i = 0; i < pieces_count; i++)
            {
                if(this.pieces[i] == null)
                    this.pieces[pieces_count] = piece;
            }
        }

        public void RemovePiece(ChessPiece piece)
        {
            int pieces_count = this.pieces.Length;

             if(piece == King) // King - The Class
                this.king = null;

            for(int i = 0; i < pieces_count; i++)
                if( this.pieces[i] == piece )
                    this.pieces[i] = null;
        }
    }
}
