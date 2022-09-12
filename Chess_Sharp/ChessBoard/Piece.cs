namespace Chess_Sharp.ChessBoard
{
    class Piece
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
    }
}
