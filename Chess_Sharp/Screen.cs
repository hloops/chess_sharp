using System;
using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;

namespace Chess_Sharp
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.RowsBoard; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; j < board.ColumnsBoard; j++)
                {
                    if(board.SinglePiece(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.SinglePiece(i,j));
                        Console.Write(" ");
                        //Console.Write($"{board.SinglePiece(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
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
        }
    }
}
