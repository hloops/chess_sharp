using System;
using System.Collections.Generic;
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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> PiecesCemetery;
        public bool CheckStatus { get; private set; }


        public Mechanics()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            PiecesCemetery = new HashSet<Piece>();
            PieceInitialization();
        }
        public Piece PlayMove(Position start, Position destination)
        {
            Piece p = Board.RemovePiece(start);
            p.movementCountIncrement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.AddPiece(p, destination);
            if (capturedPiece != null)
            {
                PiecesCemetery.Add(capturedPiece);
            }
            return capturedPiece;
        }
        public void UndoMove (Position start, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.movementCountDencrement();
            if (capturedPiece != null)
            {
                Board.AddPiece(capturedPiece, destination);
                PiecesCemetery.Remove(capturedPiece);
            }
            Board.AddPiece(p, start);


        }
        public void MakeAPlay(Position start, Position destination)
        {
            Piece capturedPiece = PlayMove(start, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(start, destination, capturedPiece);
                throw new BoardException("You cannot be in check!");

            }
            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                CheckStatus = true;
            }
            else
            {
                CheckStatus = false;
            }

            Turn++;
            SwitchPlayer();
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
        public void ValidateDestinationPosition(Position start, Position destination)
        {
            if (!Board.SinglePiece(start).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination");
            }

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
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in PiecesCemetery)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Piece> PiecesOnBoard(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }
        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece King(Color color)
        {
            foreach (Piece x in PiecesOnBoard(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool IsInCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException($"There's no {color} King on the board!");
            }
            foreach (Piece x in PiecesOnBoard(Opponent(color)))
            {
                bool[,] matrix = x.AllowedMoves();
                if (matrix[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public void AddPieceToBoard(char column, int row, Piece piece)
        {
            Board.AddPiece(piece, new ChessPositioning(column, row).toChessPosition());
            Pieces.Add(piece);
        }
        private void PieceInitialization()
        {
            AddPieceToBoard('a', 1, new Rook(Board, Color.White));
            AddPieceToBoard('h', 1, new Rook(Board, Color.White));
            AddPieceToBoard('e', 1, new King(Board, Color.White));

            AddPieceToBoard('a', 8, new Rook(Board, Color.Black));
            AddPieceToBoard('h', 8, new Rook(Board, Color.Black));
            AddPieceToBoard('e', 8, new King(Board, Color.Black));
        }
    }
}
