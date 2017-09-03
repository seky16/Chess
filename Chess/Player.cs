// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Player
    {
        public Player(string name, Color color, Game game)
        {
            this.Game = game;
            this.Name = name;
            this.Color = color;
            this.GameBoard = game.GameBoard;
            this.KingMoved = false;
            this.LeftRookMoved = false;
            this.RightRookMoved = false;
            this.Castled = false;
        }

        public string Name { get; }

        public Color Color { get; }

        public List<Piece> Pieces =>
            (from pan in this.GameBoard.Panels where pan.IsPiece where pan.Piece.Color == this.Color select pan.Piece).ToList();

        public GameBoard GameBoard { get; }

        public Player Opponent => this.GetOpponent();

        public bool CheckMate
        {
            get
            {
                if (!this.King.IsInCheck)
                {
                    return false;
                }

                var moves = this.King.GetAvailableMoves();
                return (moves?.Count ?? 0) == 0;
            }
        }

        public int BaseRow
        {
            get
            {
                switch (this.Color)
                {
                    case Color.White:
                        return 7;
                    case Color.Black:
                        return 0;
                    default:
                        return -1;
                }
            }
        }

        public bool KingMoved { get; set; }

        public bool LeftRookMoved { get; set; }

        public bool RightRookMoved { get; set; }

        public bool Castled { get; set; }

        public bool CanLeftCastle
        {
            get
            {
                if (this.KingMoved)
                {
                    return false;
                }

                if (this.LeftRookMoved)
                {
                    return false;
                }

                var p1 = this.GameBoard.GetPanel(this.BaseRow, 1);
                var p2 = this.GameBoard.GetPanel(this.BaseRow, 2);
                if (p1.IsPiece || p2.IsPiece)
                {
                    return false;
                }

                var opponentMoves = this.King.GetOpponentPanels();
                return !opponentMoves.Contains(p1) && !opponentMoves.Contains(p2);
            }
        }

        public bool CanRightCastle
        {
            get
            {
                if (this.KingMoved)
                {
                    return false;
                }

                if (this.RightRookMoved)
                {
                    return false;
                }

                var p1 = this.GameBoard.GetPanel(this.BaseRow, 5);
                var p2 = this.GameBoard.GetPanel(this.BaseRow, 6);
                if (p1.IsPiece || p2.IsPiece)
                {
                    return false;
                }

                var opponentMoves = this.King.GetOpponentPanels();
                return !opponentMoves.Contains(p1) && !opponentMoves.Contains(p2);
            }
        }

        private Game Game { get; }

        private King King => (from p in this.Pieces where p.Name == "King" select p as King).FirstOrDefault();

        // ReSharper disable once MissingSuppressionJustification
        // ReSharper disable once StyleCop.SA1404
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void PlacePieces()
        {
            if (this.Color == Color.White)
            {
                new Rook(this.GameBoard, new Coordinates(7, 0), this.Color);
                new Knight(this.GameBoard, new Coordinates(7, 1), this.Color);
                new Bishop(this.GameBoard, new Coordinates(7, 2), this.Color);
                new Queen(this.GameBoard, new Coordinates(7, 3), this.Color);
                new King(this.GameBoard, new Coordinates(7, 4), this.Color);
                new Bishop(this.GameBoard, new Coordinates(7, 5), this.Color);
                new Knight(this.GameBoard, new Coordinates(7, 6), this.Color);
                new Rook(this.GameBoard, new Coordinates(7, 7), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 0), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 1), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 2), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 3), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 4), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 5), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 6), this.Color);
                new Pawn(this.GameBoard, new Coordinates(6, 7), this.Color);
            }
            else
            {
                new Rook(this.GameBoard, new Coordinates(0, 0), this.Color);
                new Knight(this.GameBoard, new Coordinates(0, 1), this.Color);
                new Bishop(this.GameBoard, new Coordinates(0, 2), this.Color);
                new Queen(this.GameBoard, new Coordinates(0, 3), this.Color);
                new King(this.GameBoard, new Coordinates(0, 4), this.Color);
                new Bishop(this.GameBoard, new Coordinates(0, 5), this.Color);
                new Knight(this.GameBoard, new Coordinates(0, 6), this.Color);
                new Rook(this.GameBoard, new Coordinates(0, 7), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 0), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 1), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 2), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 3), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 4), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 5), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 6), this.Color);
                new Pawn(this.GameBoard, new Coordinates(1, 7), this.Color);
            }
        }

        private Player GetOpponent()
        {
            if (this == this.Game.Player1)
            {
                return this.Game.Player2;
            }

            if (this == this.Game.Player2)
            {
                return this.Game.Player1;
            }

            // ReSharper disable once UnthrowableException
            throw new Exception("Game is not set properly!");
        }
    }
}