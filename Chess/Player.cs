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
        public List<Piece> Pieces { get; set; }
        /*public bool CheckMate
        {
            get
            {
                Piece king = Pieces.Where(x => x.Name == "King").FirstOrDefault();
                List<Coordinates> moves = king.GetAvailableMoves();
                return (king as King).IsInCheck && moves.Count() == 0;
            }
        }*/


        public Player(string name, Color color, GameBoard board)
        {
            Name = name;
            Color = color;
            PlacePieces();
        }

        public void PlacePieces()
        {
            List<Piece> pieces;

            if (color == Color.White)
            {
                pieces = new List<Piece>
                {
                    new Rook(board, new Coordinates(7, 0), color),
                    new Knight(board, new Coordinates(7, 1), color),
                    new Bishop(board, new Coordinates(7, 2), color),
                    new Queen(board, new Coordinates(7, 3), color),
                    new King(board, new Coordinates(7, 4), color),
                    new Bishop(board, new Coordinates(7, 5), color),
                    new Knight(board, new Coordinates(7, 6), color),
                    new Rook(board, new Coordinates(7, 7), color),
                    new Pawn(board, new Coordinates(6, 0), color),
                    new Pawn(board, new Coordinates(6, 1), color),
                    new Pawn(board, new Coordinates(6, 2), color),
                    new Pawn(board, new Coordinates(6, 3), color),
                    new Pawn(board, new Coordinates(6, 4), color),
                    new Pawn(board, new Coordinates(6, 5), color),
                    new Pawn(board, new Coordinates(6, 6), color),
                    new Pawn(board, new Coordinates(6, 7), color)
                };
            }
            else
            {
                pieces = new List<Piece>
                {
                    new Rook(board, new Coordinates(0, 0), color),
                    new Knight(board, new Coordinates(0, 1), color),
                    new Bishop(board, new Coordinates(0, 2), color),
                    new Queen(board, new Coordinates(0, 3), color),
                    new King(board, new Coordinates(0, 4), color),
                    new Bishop(board, new Coordinates(0, 5), color),
                    new Knight(board, new Coordinates(0, 6), color),
                    new Rook(board, new Coordinates(0, 7), color),
                    new Pawn(board, new Coordinates(1, 0), color),
                    new Pawn(board, new Coordinates(1, 1), color),
                    new Pawn(board, new Coordinates(1, 2), color),
                    new Pawn(board, new Coordinates(1, 3), color),
                    new Pawn(board, new Coordinates(1, 4), color),
                    new Pawn(board, new Coordinates(1, 5), color),
                    new Pawn(board, new Coordinates(1, 6), color),
                    new Pawn(board, new Coordinates(1, 7), color)
                };
            }
            Pieces = pieces;
        }
    }
}