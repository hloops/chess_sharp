using System;
using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;

namespace Chess_Sharp.ChessGame
{
    class Mechanics
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool IsFinished { get; private set; }


        public Mechanics()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            PieceInitialization();
        }
        public void PlayMove(Position start, Position destination)
        {
            Piece p = board.RemovePiece(start);
            p.movementCountIncrement();
            Piece capturedPiece = board.RemovePiece(destination);
            board.AddPiece(p, destination);
        }
        private void PieceInitialization()
        {
            board.AddPiece(new Rook(board, Color.White), new ChessPositioning('a', 1).toChessPosition());
            board.AddPiece(new Rook(board, Color.White), new ChessPositioning('h', 1).toChessPosition());
            board.AddPiece(new Rook(board, Color.Black), new ChessPositioning('a', 8).toChessPosition());
            board.AddPiece(new Rook(board, Color.Black), new ChessPositioning('h', 8).toChessPosition());
        }
    }
}
