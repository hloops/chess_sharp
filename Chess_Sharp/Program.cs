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
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board);
                        Console.WriteLine();
                        Console.WriteLine($"Turn: {chessMatch.Turn}");
                        Console.WriteLine($"Waiting for {chessMatch.CurrentPlayer} to play.");

                        Console.Write("Star position: ");
                        Position start = Screen.ReadChessPos().toChessPosition();

                        chessMatch.ValidateStartPosition(start);

                        bool[,] allowedPositioning = chessMatch.Board.SinglePiece(start).AllowedMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, allowedPositioning);

                        Console.Write("Destination position: ");
                        Position destination = Screen.ReadChessPos().toChessPosition();

                        chessMatch.MakeAPlay(start, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    {

                    }
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
