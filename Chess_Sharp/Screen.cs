using System;
using System.Collections.Generic;
using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;

namespace Chess_Sharp
{
    class Screen
    {
        public static void PrintMatch(Mechanics match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine($"Turn: {match.Turn}\n");
            if (!match.IsFinished)
            {
                Console.WriteLine($"Waiting for {match.CurrentPlayer} to play.");
                if (match.CheckStatus)
                {
                    Console.WriteLine("\n***** Check! *****\n");
                }
            }
            else
            {
                Console.WriteLine($"----Checkmate!----\nWinner: {match.CurrentPlayer}!");
            }
        }
        public static void PrintCapturedPieces (Mechanics match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            PrintHashSet(match.CapturedPieces(Color.White));
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            PrintHashSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintHashSet(HashSet<Piece> hashSet)
        {
            Console.Write("[");
            foreach (Piece piece in hashSet)
            {
                Console.Write(piece + " ");
            }
            Console.WriteLine("]");
        }
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.RowsBoard; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.ColumnsBoard; j++)
                {
                    PrintPiece(board.SinglePiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintBoard(Board board, bool[,] allowedPositioning)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackgorund = ConsoleColor.DarkGray;

            for (int i = 0; i < board.RowsBoard; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.ColumnsBoard; j++)
                {
                    if (allowedPositioning[i, j])
                    {
                        Console.BackgroundColor = alteredBackgorund;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.SinglePiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }
        public static ChessPositioning ReadChessPos()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPositioning(column, row);
        }
        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
