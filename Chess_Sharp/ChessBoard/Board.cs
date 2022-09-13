namespace Chess_Sharp.ChessBoard
{
    class Board
    {
        public int RowsBoard { get; set; }
        public int ColumnsBoard { get; set; }
        private Piece[,] pieces;

        public Board(int rowsBoard, int columnsBoars)
        {
            RowsBoard = rowsBoard;
            ColumnsBoard = columnsBoars;
            pieces = new Piece[rowsBoard, columnsBoars];

        }
        public Piece SinglePiece(int rowsBoard, int columnsBoard)
        {
            return pieces[rowsBoard, columnsBoard];
        }

        public Piece SinglePiece(Position pos)
        {
            return pieces[pos.Row, pos.Column];
        }

        public bool IsThereAPiece(Position pos)
        {
            ValidatePosition(pos);
            return SinglePiece(pos) != null;
        }

        public void AddPiece(Piece p, Position pos)
        {
            if (IsThereAPiece(pos))
            {
                throw new BoardException("Position already has a piece.");
            }
            pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }
        public Piece RemovePiece(Position pos)
        {
            if (SinglePiece(pos) == null)
            {
                return null;
            }
            Piece aux = SinglePiece(pos);
            aux.Position = null;
            pieces[pos.Row, pos.Column] = null;
            return aux;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Column < 0 || pos.Row >= RowsBoard || pos.Column >= ColumnsBoard)
            {
                return false;
            }
            return true;
        }
        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position");
            }
        }
    }
}