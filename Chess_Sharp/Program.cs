using Chess_Sharp.ChessBoard;
using System;

namespace Chess_Sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            foreach (var item in board)
            {
                Console.Write(item);    
            }
            Console.WriteLine();
        }
    }
}
