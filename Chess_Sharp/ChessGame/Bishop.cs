using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }
        public override string ToString()
        {
            return "B";
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

            pos.SetValue(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
                if (Board.SinglePiece(pos) != null && Board.SinglePiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValue(pos.Row -1, pos.Column -1);
            }
            pos.SetValue(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
                if (Board.SinglePiece(pos) != null && Board.SinglePiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValue(pos.Row +1, pos.Column +1); ;
            }
            pos.SetValue(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
                if (Board.SinglePiece(pos) != null && Board.SinglePiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValue(pos.Row -1, pos.Column+1); ;
            }
            pos.SetValue(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && IsAllowedToMove(pos))
            {
                movesMatrix[pos.Row, pos.Column] = true;
                if (Board.SinglePiece(pos) != null && Board.SinglePiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValue(pos.Row+1, pos.Column-1); ;
            }
            return movesMatrix;
        }
    }
}
