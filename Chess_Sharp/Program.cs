using Chess_Sharp.ChessBoard;
using System;

namespace Chess_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.PrintBoard(board);
            Console.ReadLine();
        }
    }
}
