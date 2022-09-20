using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class Knight : Piece
    {
        public Knight (Board board, Color color) : base (board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool IsAllowedToMove(Position pos)
        {
            Piece p = Board.SinglePiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] AllowedMoves()
        {
            bool[,] movesMatrix = new bool[Board.RowsBoard, Board.ColumnsBoard];

            Position pos = new Position(0, 0);

            pos.SetValue(Position.Row - 1, Position.Column -2);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row +1, Position.Column -2);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            return movesMatrix;
        }
    }
}
