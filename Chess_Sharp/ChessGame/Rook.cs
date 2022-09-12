using Chess_Sharp.ChessBoard;

namespace Chess_Sharp.ChessGame
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
