using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;
using System;

namespace Chess_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPositioning pos = new ChessPositioning('a', 1);

            Console.WriteLine(pos);
            Console.WriteLine(pos.toChessPosition());
        }
    }
}
