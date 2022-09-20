using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class Pawn : Piece
    {
        private Mechanics ChessMatch;
        public Pawn(Board board, Color color, Mechanics chessMatch) : base(board, color)
        {
            ChessMatch = chessMatch;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool IsThereAnEnemy(Position pos)
        {
            Piece p = Board.SinglePiece(pos);
            return p!= null && p.Color != Color;
        }
        private bool IsAllowedToMove(Position pos)
        {
            return Board.SinglePiece(pos) == null;
        }
        public override bool[,] AllowedMoves()
        {
            bool[,] movesMatrix = new bool[Board.RowsBoard, Board.ColumnsBoard];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValue(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsAllowedToMove(pos) && MovementCount == 0)
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row-1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereAnEnemy(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereAnEnemy(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }

                //En Passant - White
                if (Position.Row == 3)
                {
                    Position left = new Position (Position.Row , Position.Column - 1);
                    if(Board.ValidPosition (left) && IsThereAnEnemy(left) && Board.SinglePiece(left) == ChessMatch.EnPassantVulnerability)
                    {
                        movesMatrix[left.Row -1, left.Column] = true;
                    }
                    Position right = new Position (Position.Row , Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereAnEnemy(right) && Board.SinglePiece(right) == ChessMatch.EnPassantVulnerability)
                    {
                        movesMatrix[right.Row -1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValue(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsAllowedToMove(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row + 2, Position.Column );
                if (Board.ValidPosition(pos) && IsAllowedToMove(pos) && MovementCount == 0)
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row +1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereAnEnemy(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }
                pos.SetValue(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereAnEnemy(pos))
                {
                    movesMatrix[pos.Row, pos.Column] = true;
                }

                //En Passant - Black
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereAnEnemy(left) && Board.SinglePiece(left) == ChessMatch.EnPassantVulnerability)
                    {
                        movesMatrix[left.Row +1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereAnEnemy(right) && Board.SinglePiece(right) == ChessMatch.EnPassantVulnerability)
                    {
                        movesMatrix[right.Row +1, right.Column] = true;
                    }
                }
            }
            return movesMatrix;
        }
    }
}