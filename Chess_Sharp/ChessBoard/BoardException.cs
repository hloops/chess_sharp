using System;

namespace Chess_Sharp.ChessBoard
{
    internal class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
