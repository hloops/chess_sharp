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
                        Screen.PrintMatch(chessMatch);

                        Console.Write("Star position: ");
                        Position start = Screen.ReadChessPos().toChessPosition();

                        chessMatch.ValidateStartPosition(start);

                        bool[,] allowedPositioning = chessMatch.Board.SinglePiece(start).AllowedMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, allowedPositioning);

                        Console.Write("Destination position: ");
                        Position destination = Screen.ReadChessPos().toChessPosition();
                        chessMatch.ValidateDestinationPosition(start, destination);

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
                Console.Clear();
                Screen.PrintMatch(chessMatch);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
