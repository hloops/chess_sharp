using Chess_Sharp.ChessBoard;


namespace Chess_Sharp.ChessGame
{
    internal class ChessPositioning
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPositioning (char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position toChessPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }
        public override string ToString()
        {
            return $"{Column}{Row}";
        }

    }
}
