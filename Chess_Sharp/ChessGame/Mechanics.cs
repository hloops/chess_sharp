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

            //Castling
            //King Side
            if(p is King && destination.Column == start.Column + 2)
            {
                Position RookStartPosition = new Position(start.Row, start.Column + 3);
                Position RookEndPosition = new Position(start.Row, start.Column + 1);

                Piece rook = Board.RemovePiece(RookStartPosition);
                rook.movementCountIncrement();
                Board.AddPiece(rook, RookEndPosition);
            }
            //Queen Side
            if (p is King && destination.Column == start.Column - 2)
            {
                Position RookStartPosition = new Position(start.Row, start.Column - 4);
                Position RookEndPosition = new Position(start.Row, start.Column - 1);

                Piece rook = Board.RemovePiece(RookStartPosition);
                rook.movementCountIncrement();
                Board.AddPiece(rook, RookEndPosition);
            }
            return capturedPiece;
        }
        public void UndoMove(Position start, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.movementCountDecrement();
            if (capturedPiece != null)
            {
                Board.AddPiece(capturedPiece, destination);
                PiecesCemetery.Remove(capturedPiece);
            }
            Board.AddPiece(p, start);

            //Undo Castling
            //King Side
            if (p is King && destination.Column == start.Column + 2)
            {
                Position RookStartPosition = new Position(start.Row, start.Column + 3);
                Position RookEndPosition = new Position(start.Row, start.Column + 1);

                Piece rook = Board.RemovePiece(RookEndPosition);
                rook.movementCountDecrement();
                Board.AddPiece(rook, RookStartPosition);
            }
            //Queen Side
            if (p is King && destination.Column == start.Column - 2)
            {
                Position RookStartPosition = new Position(start.Row, start.Column - 4);
                Position RookEndPosition = new Position(start.Row, start.Column - 1);

                Piece rook = Board.RemovePiece(RookEndPosition);
                rook.movementCountDecrement();
                Board.AddPiece(rook, RookStartPosition);
            }


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
            if (IsInCheckmate(Opponent(CurrentPlayer)))
            {
                IsFinished = true;
            }
            else
            {
                Turn++;
                SwitchPlayer();
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
        public bool IsInCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesOnBoard(color))
            {
                bool[,] matrix = x.AllowedMoves();
                for (int i = 0; i < Board.RowsBoard; i++)
                {
                    for (int j = 0; j < Board.ColumnsBoard; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position start = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = PlayMove(start, destination);
                            bool checkTest = IsInCheck(color);
                            UndoMove(start, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        public void AddPieceToBoard(char column, int row, Piece piece)
        {
            Board.AddPiece(piece, new ChessPositioning(column, row).toChessPosition());
            Pieces.Add(piece);
        }
        private void PieceInitialization()
        {
            AddPieceToBoard('a', 1, new Rook(Board, Color.White));
            AddPieceToBoard('b', 1, new Knight(Board, Color.White));
            AddPieceToBoard('c', 1, new Bishop(Board, Color.White));
            AddPieceToBoard('d', 1, new Queen(Board, Color.White));
            AddPieceToBoard('e', 1, new King(Board, Color.White, this));
            AddPieceToBoard('f', 1, new Knight(Board, Color.White));
            AddPieceToBoard('g', 1, new Bishop(Board, Color.White));
            AddPieceToBoard('h', 1, new Rook(Board, Color.White));
            /*AddPieceToBoard('a', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('b', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('c', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('d', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('e', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('f', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('g', 2, new Pawn(Board, Color.White));
            AddPieceToBoard('h', 2, new Pawn(Board, Color.White));


            AddPieceToBoard('a', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('b', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('c', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('d', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('e', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('f', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('g', 7, new Pawn(Board, Color.Black));
            AddPieceToBoard('h', 7, new Pawn(Board, Color.Black));*/
            AddPieceToBoard('a', 8, new Rook(Board, Color.Black));
            AddPieceToBoard('b', 8, new Knight(Board, Color.Black));
            AddPieceToBoard('c', 8, new Bishop(Board, Color.Black));
            AddPieceToBoard('d', 8, new Queen(Board, Color.Black));
            AddPieceToBoard('e', 8, new King(Board, Color.Black, this));
            AddPieceToBoard('f', 8, new Bishop(Board, Color.Black));
            AddPieceToBoard('g', 8, new Knight(Board, Color.Black));
            AddPieceToBoard('h', 8, new Rook(Board, Color.Black));
        }
    }
}
