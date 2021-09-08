using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    class EnPassantPawn : ChessPiece
    {
        private Pawn originalPawn;

        public EnPassantPawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard, Pawn originalPawn) : base(location, color, board, movementBoard)
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
            this.originalPawn.Dispose();
            this.board.SetPieceByLocation(null, this.location);
            this.board.enPassantPawn = null;
        }
    }
}