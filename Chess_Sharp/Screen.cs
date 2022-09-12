using System;
using Chess_Sharp.ChessBoard;

namespace Chess_Sharp
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.RowsBoard; i++)
            {
                for(int j = 0; j < board.ColumnsBoard; j++)
                {
                    if(board.SinglePiece(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{board.SinglePiece(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
