using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;
using System;

namespace Chess_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Mechanics chessMatch = new Mechanics();
                while (!chessMatch.IsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);

                    Console.Write("Star position: ");
                    Position start = Screen.ReadChessPos().toChessPosition();

                    bool[,] allowedPositioning = chessMatch.Board.SinglePiece(start).AllowedMoves();

                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board, allowedPositioning);

                    Console.Write("Destination position: ");
                    Position destination = Screen.ReadChessPos().toChessPosition();

                    chessMatch.PlayMove(start, destination);
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
