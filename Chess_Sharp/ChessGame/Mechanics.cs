using System;
using Chess_Sharp.ChessBoard;
using Chess_Sharp.ChessGame;

namespace Chess_Sharp.ChessGame
{
    class Mechanics
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
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
        public void MakeAPlay  ( Position start, Position destination)
        {
            PlayMove(start, destination);
            Turn++;
            SwitchPlayer();
        }
        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
        public void ValidateStartPosition(Position pos)
        {
            if (Board.SinglePiece(pos) == null)
            {
                throw new BoardException("There's no piece on the selected spot.");
            } 
            if (CurrentPlayer != Board.SinglePiece(pos).Color)
            {
                throw new BoardException("This piece is not yours.");
            }
            if (!Board.SinglePiece(pos).IsThereAnyMovesPossible()) 
            {
                throw new BoardException("There's no possible moves.");
            }

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
