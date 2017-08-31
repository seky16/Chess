using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public List<Piece> Pieces { get
            {
                var output = new List<Piece>();
                foreach (var pan in GameBoard.Panels)
                {
                    if (pan.IsPiece)
                    {
                        if (pan.Piece.Color == Color) { output.Add(pan.Piece); }
                    }
                }
                return output;
            }
        }
        public GameBoard GameBoard { get; set; }
        public Game Game { get; set; }
        public Player Opponent => Game.GetOpponent(this);
        public King King =>
        {
            foreach (var p in Pieces)
            {
                if (p.Name == "King") { return p as King; }
            }
        }
        public bool CheckMate =>
        {
            List<Coordinates> moves = King.AvailableMoves;
            return King.IsInCheck && (moves?.Count() ?? 0) == 0);
        }
        public int BaseRow => 
        {
            if (Color == Color.White) { return 7; }
            else if (Color == Color.Black) { return 0; }
            else { return -1; }
        }
        public bool KingMoved { get; set; }
        public bool LeftRookMoved { get; set; }
        public bool RightRookMoved { get; set; }
        public bool CanLeftCastle =>
        {
            if (KingMoved) { return false; }
            if (LeftRookMoved) { return false; }
            var p1 = GameBoard.GetPanel(BaseRow, 1);
            var p2 = GameBoard.GetPanel(BaseRow, 2);
            if (p1.IsPiece || p2.IsPiece) { return false; }
            var opponentMoves = GameBoard.GetOpponentPanels();
            if (opponentMoves.Contains(p1) || opponentMoved.Contains(p2)) { return false; }
            return true;
        }
        public bool CanRightCastle =>
        {
            if (KingMoved) { return false; }
            if (RightRookMoved) { return false; }
            var p1 = GameBoard.GetPanel(BaseRow, 5);
            var p2 = GameBoard.GetPanel(BaseRow, 6);
            if (p1.IsPiece || p2.IsPiece) { return false; }
            var opponentMoves = GameBoard.GetOpponentPanels();
            if (opponentMoves.Contains(p1) || opponentMoved.Contains(p2)) { return false; }
            return true;
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

        public void PlacePieces()
        {
            List<Piece> pieces;

            if (Color == Color.White)
            {
                pieces = new List<Piece>
                {
                    new Rook(GameBoard, new Coordinates(7, 0), Color),
                    new Knight(GameBoard, new Coordinates(7, 1), Color),
                    new Bishop(GameBoard, new Coordinates(7, 2), Color),
                    new Queen(GameBoard, new Coordinates(7, 3), Color),
                    new King(GameBoard, new Coordinates(7, 4), Color),
                    new Bishop(GameBoard, new Coordinates(7, 5), Color),
                    new Knight(GameBoard, new Coordinates(7, 6), Color),
                    new Rook(GameBoard, new Coordinates(7, 7), Color),
                    new Pawn(GameBoard, new Coordinates(6, 0), Color),
                    new Pawn(GameBoard, new Coordinates(6, 1), Color),
                    new Pawn(GameBoard, new Coordinates(6, 2), Color),
                    new Pawn(GameBoard, new Coordinates(6, 3), Color),
                    new Pawn(GameBoard, new Coordinates(6, 4), Color),
                    new Pawn(GameBoard, new Coordinates(6, 5), Color),
                    new Pawn(GameBoard, new Coordinates(6, 6), Color),
                    new Pawn(GameBoard, new Coordinates(6, 7), Color)
                };
            }
            else
            {
                pieces = new List<Piece>
                {
                    new Rook(GameBoard, new Coordinates(0, 0), Color),
                    new Knight(GameBoard, new Coordinates(0, 1), Color),
                    new Bishop(GameBoard, new Coordinates(0, 2), Color),
                    new Queen(GameBoard, new Coordinates(0, 3), Color),
                    new King(GameBoard, new Coordinates(0, 4), Color),
                    new Bishop(GameBoard, new Coordinates(0, 5), Color),
                    new Knight(GameBoard, new Coordinates(0, 6), Color),
                    new Rook(GameBoard, new Coordinates(0, 7), Color),
                    new Pawn(GameBoard, new Coordinates(1, 0), Color),
                    new Pawn(GameBoard, new Coordinates(1, 1), Color),
                    new Pawn(GameBoard, new Coordinates(1, 2), Color),
                    new Pawn(GameBoard, new Coordinates(1, 3), Color),
                    new Pawn(GameBoard, new Coordinates(1, 4), Color),
                    new Pawn(GameBoard, new Coordinates(1, 5), Color),
                    new Pawn(GameBoard, new Coordinates(1, 6), Color),
                    new Pawn(GameBoard, new Coordinates(1, 7), Color)
                };
            }
        }
    }
}