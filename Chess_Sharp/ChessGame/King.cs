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
    }
}
