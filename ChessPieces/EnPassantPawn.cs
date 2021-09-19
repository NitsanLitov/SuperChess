using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public class EnPassantPawn : ChessPiece
    {
        private Pawn originalPawn;

        public EnPassantPawn((char, int) location, Pawn originalPawn) : base(location, originalPawn.color, originalPawn.board, originalPawn.movementBoard)
        {
            this.originalPawn = originalPawn;
        }

        public Pawn OriginalPawn { get { return this.originalPawn; } }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            return new List<(char, int)>();
        }

        public override void Dispose()
        {
            this.board.SetPieceByLocation(null, this.location);
            this.board.enPassantPawn = null;
        }
    }
}