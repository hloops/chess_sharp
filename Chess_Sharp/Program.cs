using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;
using System;

namespace Chess_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            try
            {
                board.AddPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.AddPiece(new Rook(board, Color.Black), new Position(1, 3));
                board.AddPiece(new King(board, Color.Black), new Position(0, 9));


                Screen.PrintBoard(board);
                Console.ReadLine();
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
