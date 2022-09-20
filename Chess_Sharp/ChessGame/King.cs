using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class King : Piece
    {
        private Mechanics ChessMatch;
        public King (Board board, Color color, Mechanics chessMatch) : base (board , color)
        {
            ChessMatch = chessMatch;
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
        private bool IsRookAllowedToCastle(Position pos)
        {
            Piece p = Board.SinglePiece(pos);
            return p != null && p is Rook && p.Color == Color && p.MovementCount == 0;
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

            //Castling
            if (MovementCount == 0 && !ChessMatch.CheckStatus)
            {
                //King side
                Position expectedRookPositionK = new Position(Position.Row, Position.Column + 3);
                if (IsRookAllowedToCastle(expectedRookPositionK))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.SinglePiece(p1) == null && Board.SinglePiece(p2) == null)
                    {
                        movesMatrix[Position.Row, Position.Column + 2] = true;
                    }
                }
                //Queen side
                Position expectedRookPositionQ = new Position(Position.Row, Position.Column - 4);
                if (IsRookAllowedToCastle(expectedRookPositionQ))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.SinglePiece(p1) == null && Board.SinglePiece(p2) == null && Board.SinglePiece(p3) == null )
                    {
                        movesMatrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }
            return movesMatrix;
        }
    }
}
