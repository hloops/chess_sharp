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
    }
}