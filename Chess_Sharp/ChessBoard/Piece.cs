namespace Chess_Sharp.ChessBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }

        public int MovementCount { get; protected set; }
        public Board Board { get; protected set; }

        
        public Piece(Board board, Color color )
        {
            Position = null;
            Board = board;
            Color = color;
            MovementCount = 0;
        }

        public void movementCountIncrement()
        {
            MovementCount++;
        }

        public bool IsThereAnyMovesPossible()
        {
            bool[,] matrix = AllowedMoves();
            for (int i = 0; i < Board.RowsBoard; i++)
            {
                for (int j = 0; j < Board.ColumnsBoard; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return AllowedMoves()[pos.Row, pos.Column];
        }
        public abstract bool[,] AllowedMoves();
    }
}
