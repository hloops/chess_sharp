using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class King : Piece
    {
        public King (Board board, Color color) : base (board , color)
        {
        }
        public override string ToString()
        {
            return "K";
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

            pos.SetValue(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 1 , Position.Column + 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 1 , Position.Column);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row + 1 , Position.Column - 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row , Position.Column - 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            pos.SetValue(Position.Row - 1 , Position.Column - 1);
            if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
            }
            return movesMatrix;
        }
    }
}
