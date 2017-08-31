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
        /*public bool CheckMate
        {
            get
            {
                Piece king = Pieces.Where(x => x.Name == "King").FirstOrDefault();
                List<Coordinates> moves = king.GetAvailableMoves();
                return (king as King).IsInCheck && moves == null;
            }
        }*/


        public Player(string name, Color color, Game game)
        {
            Game = game;
            Name = name;
            Color = color;
            GameBoard = game.GameBoard;
            PlacePieces();
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