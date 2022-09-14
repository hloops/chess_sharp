using System;
using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;

namespace Chess_Sharp.ChessGame
{
    class Mechanics
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool IsFinished { get; private set; }


        public Mechanics()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            PieceInitialization();
        }
        public void PlayMove(Position start, Position destination)
        {
            Piece p = Board.RemovePiece(start);
            p.movementCountIncrement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.AddPiece(p, destination);
        }
        private void PieceInitialization()
        {
            Board.AddPiece(new Rook(Board, Color.White), new ChessPositioning('a', 1).toChessPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPositioning('h', 1).toChessPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPositioning('a', 8).toChessPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPositioning('h', 8).toChessPosition());
        }
    }
}
