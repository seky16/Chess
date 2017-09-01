using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;

namespace Chess
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public List<Piece> Pieces => (from pan in GameBoard.Panels where pan.IsPiece where pan.Piece.Color == Color select pan.Piece).ToList();
        public GameBoard GameBoard { get; set; }
        public Game Game { get; set; }
        public Player Opponent => GetOpponent();
        public King King => (from p in Pieces where p.Name == "King" select p as King).FirstOrDefault();

        public bool CheckMate
        { get
            {
                if (!King.IsInCheck) { return false; }
                var moves = King.GetAvailableMoves();
                return (moves?.Count() ?? 0) == 0;
            }
        }
        public int BaseRow
        { get
            {
                switch (Color)
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
        public bool CanLeftCastle
        {
            get
            {
                if (KingMoved) { return false; }
                if (LeftRookMoved) { return false; }
                var p1 = GameBoard.GetPanel(BaseRow, 1);
                var p2 = GameBoard.GetPanel(BaseRow, 2);
                if (p1.IsPiece || p2.IsPiece) { return false; }
                var opponentMoves = King.GetOpponentPanels();
                return !opponentMoves.Contains(p1) && !opponentMoves.Contains(p2);
            }
        }
        public bool CanRightCastle
        {
            get
            {
                if (KingMoved) { return false; }
                if (RightRookMoved) { return false; }
                var p1 = GameBoard.GetPanel(BaseRow, 5);
                var p2 = GameBoard.GetPanel(BaseRow, 6);
                if (p1.IsPiece || p2.IsPiece) { return false; }
                var opponentMoves = King.GetOpponentPanels();
                return !opponentMoves.Contains(p1) && !opponentMoves.Contains(p2);
            }
        }

        public Player(string name, Color color, Game game)
        {
            Game = game;
            Name = name;
            Color = color;
            GameBoard = game.GameBoard;
            KingMoved = false;
            LeftRookMoved = false;
            RightRookMoved = false;
        }

        private Player GetOpponent()
        {
            if (this == Game.Player1) { return Game.Player2; }
            if (this == Game.Player2) { return Game.Player1; }
            throw new Exception("Game is not set properly!");
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void PlacePieces()
        {
            if (Color == Color.White)
            {
                new Rook(GameBoard, new Coordinates(7, 0), Color);
                new Knight(GameBoard, new Coordinates(7, 1), Color);
                new Bishop(GameBoard, new Coordinates(7, 2), Color);
                new Queen(GameBoard, new Coordinates(7, 3), Color);
                new King(GameBoard, new Coordinates(7, 4), Color);
                new Bishop(GameBoard, new Coordinates(7, 5), Color);
                new Knight(GameBoard, new Coordinates(7, 6), Color);
                new Rook(GameBoard, new Coordinates(7, 7), Color);
                new Pawn(GameBoard, new Coordinates(6, 0), Color);
                new Pawn(GameBoard, new Coordinates(6, 1), Color);
                new Pawn(GameBoard, new Coordinates(6, 2), Color);
                new Pawn(GameBoard, new Coordinates(6, 3), Color);
                new Pawn(GameBoard, new Coordinates(6, 4), Color);
                new Pawn(GameBoard, new Coordinates(6, 5), Color);
                new Pawn(GameBoard, new Coordinates(6, 6), Color);
                new Pawn(GameBoard, new Coordinates(6, 7), Color);
            }
            else
            {
                new Rook(GameBoard, new Coordinates(0, 0), Color);
                new Knight(GameBoard, new Coordinates(0, 1), Color);
                new Bishop(GameBoard, new Coordinates(0, 2), Color);
                new Queen(GameBoard, new Coordinates(0, 3), Color);
                new King(GameBoard, new Coordinates(0, 4), Color);
                new Bishop(GameBoard, new Coordinates(0, 5), Color);
                new Knight(GameBoard, new Coordinates(0, 6), Color);
                new Rook(GameBoard, new Coordinates(0, 7), Color);
                new Pawn(GameBoard, new Coordinates(1, 0), Color);
                new Pawn(GameBoard, new Coordinates(1, 1), Color);
                new Pawn(GameBoard, new Coordinates(1, 2), Color);
                new Pawn(GameBoard, new Coordinates(1, 3), Color);
                new Pawn(GameBoard, new Coordinates(1, 4), Color);
                new Pawn(GameBoard, new Coordinates(1, 5), Color);
                new Pawn(GameBoard, new Coordinates(1, 6), Color);
                new Pawn(GameBoard, new Coordinates(1, 7), Color);
            }
        }
    }
}