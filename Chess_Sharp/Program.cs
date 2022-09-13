using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;
using System;

namespace Chess_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Board board = new Board(8,8);
            Console.WriteLine(board);
        }
    }
}
