using System;
using System.Collections.Generic;

using Movement;
using Players;

namespace ChessBoard
{
    public class Pawn : ChessPiece
    {
        private ((char, int), (char, int)) enPassantMove;

        public Pawn((char, int) location, ChessColor color, Board board, MovementBoard movementBoard) : base(location, color, board, movementBoard) { }

        public override List<(char, int)> GetMovementOptions(bool canPieceTakeOpponentKing)
        {
            List<(char, int)> movementOptions = this.ProcessMoves(this.movementBoard.Up(this.location, this.isFirstMove ? 2 : 1), canPieceTakeOpponentKing, false);

            this.enPassantMove = default;

            if (movementOptions.Count == 2)
                this.enPassantMove = (movementOptions[0], movementOptions[1]);

            List<List<(char, int)>> diagMovementOptions = this.movementBoard.DiagonalUp(this.location, 1);

            foreach (List<(char, int)> direction in diagMovementOptions)
                movementOptions.AddRange(this.ProcessMoves(direction, canPieceTakeOpponentKing, true, true));

            this.movementOptions = new List<(char, int)>(movementOptions);
            return movementOptions;
        }

        protected internal override void MovePieceOnBoardLocation((char, int) newLocation)
        {
            base.MovePieceOnBoardLocation(newLocation);

            if (newLocation != this.enPassantMove.Item2) return;

            EnPassantPawn enPassantPawn = new EnPassantPawn(this.enPassantMove.Item1, this.color, this.board, this.movementBoard, this);
            this.board.enPassantPawn = enPassantPawn;            
            this.enPassantMove = default;
        }
    }
}