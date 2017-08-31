using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess
{
    public abstract class Piece
    {
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
        public List<Coordinates> AvailableMoves { get { return GetAvailableMoves(); } }
        public Color Color { get; set; }
        protected char Character { get; set; }
        public GameBoard GameBoard { get; set; }

        public char Show()
        {
            char r = Character;
                 if (Color == Color.White) { r = Char.ToUpper(r); }
            else if (Color == Color.Black) { r = Char.ToLower(r); }
            else throw new Exception();
            return r;
        }

        public bool IsOpponent(Panel pan)
        {
            if ((!pan?.IsPiece) ?? false) { return false; }
            return pan.Piece.Color != Color;
        }

        public List<Coordinates> Cross()
        {
            return Cross(7);
        }

        //todo: shorten?
        public List<Coordinates> Cross(int radius)
        {
            var board = GameBoard;
            var coords = Coordinates;
            var output = new List<Coordinates>();
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row - i;
                Panel pan = board.GetPanel(r, coords.Column);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row + i;
                Panel pan = board.GetPanel(r, coords.Column);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int c = coords.Column - i;
                Panel pan = board.GetPanel(coords.Row, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int c = coords.Column + i;
                Panel pan = board.GetPanel(coords.Row, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            return output;
        }

        public List<Coordinates> Diagonal()
        {
            return Diagonal(7);
        }

        //todo: shorten?
        public List<Coordinates> Diagonal(int radius)
        {
            var board = GameBoard;
            var coords = Coordinates;
            var output = new List<Coordinates>();
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row - i;
                int c = coords.Column - i;
                Panel pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row - i;
                int c = coords.Column + i;
                Panel pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row + i;
                int c = coords.Column + i;
                Panel pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            for (int i = 1; i <= radius; i++)
            {
                int r = coords.Row + i;
                int c = coords.Column - i;
                Panel pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            return output;
        }

        //todo: review - should be ok
        public void Place(Coordinates coords)
        {
            var panel = GameBoard.GetPanel(coords);
            panel.Piece = this;
            Coordinates = panel.Coordinates;
        }

        public void Remove(Coordinates coords)
        {
            var panel = GameBoard.GetPanel(coords);
            panel.Piece = null;
        }

        protected virtual List<Coordinates> GetAvailableMoves()
        {
            return null;
        }
    }

    public class Rook : Piece
    {
        public Rook(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "Rook";
            Character = 'R';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            return Cross();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "Bishop";
            Character = 'B';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            return Diagonal();
        }
    }

    public class Queen : Piece
    {
        public Queen(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "Queen";
            Character = 'Q';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            return Cross().Concat(Diagonal()).ToList();
        }
    }

    public class Knight : Piece
    {
        public Knight(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "Knight";
            Character = 'N';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            var board = GameBoard;
            var r = Coordinates.Row;
            var c = Coordinates.Column;
            var output = new List<Coordinates>();
            var possible = new List<Coordinates>()
            {
                board.GetPanel(r - 2, c + 1)?.Coordinates,
                board.GetPanel(r - 1, c + 2)?.Coordinates,
                board.GetPanel(r + 1, c + 2)?.Coordinates,
                board.GetPanel(r + 2, c + 1)?.Coordinates,
                board.GetPanel(r + 2, c - 1)?.Coordinates,
                board.GetPanel(r + 1, c - 2)?.Coordinates,
                board.GetPanel(r - 1, c - 2)?.Coordinates,
                board.GetPanel(r - 2, c - 1)?.Coordinates
            };


            foreach (var coords in possible)
            {
                if (coords?.Valid ?? false)
                {
                    var pan = board.GetPanel(coords);
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); }
                }
            }
            return output;
        }
    }

    public class Pawn : Piece
    {
        public Pawn(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "Pawn";
            Character = 'P';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            var output = new List<Coordinates>();
            if (Color == Color.White)
            {
                var up = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column);
                if ((up?.Coordinates?.Valid ?? false) && ((!up?.IsPiece) ?? false)) { output.Add(up.Coordinates); }
                var upL = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column - 1);
                if ((upL?.Coordinates?.Valid ?? false) && IsOpponent(upL)) { output.Add(upL.Coordinates); }
                var upR = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column + 1);
                if ((upR?.Coordinates?.Valid ?? false) && IsOpponent(upR)) { output.Add(upR.Coordinates); }
                if (Coordinates.Row == 6)
                {
                    var up2 = GameBoard.GetPanel(Coordinates.Row - 2, Coordinates.Column);
                    if (up2.Coordinates.Valid && !up2.IsPiece) { output.Add(up2.Coordinates); }
                }
            }
            else
            {
                var down = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column);
                if ((down?.Coordinates?.Valid ?? false) && ((!down?.IsPiece) ?? false)) { output.Add(down.Coordinates); }
                var downL = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column - 1);
                if ((downL?.Coordinates?.Valid ?? false) && IsOpponent(downL)) { output.Add(downL.Coordinates); }
                var downR = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column + 1);
                if ((downR?.Coordinates?.Valid ?? false) && IsOpponent(downR)) { output.Add(downR.Coordinates); }
                if (Coordinates.Row == 1)
                {
                    var down2 = GameBoard.GetPanel(Coordinates.Row + 2, Coordinates.Column);
                    if (down2.Coordinates.Valid && !down2.IsPiece) { output.Add(down2.Coordinates); }
                }
            }
            //en passant?
            return output;
        }
    }

    public class King : Piece
    {
        public King(GameBoard board, Coordinates coords, Color color)
        {
            GameBoard = board;
            Color = color;
            Coordinates = coords;
            Name = "King";
            Character = 'K';
            Place(coords);
        }

        protected override List<Coordinates> GetAvailableMoves()
        {
            var output = new List<Coordinates>();
            List<Coordinates> possible = Cross(1).Concat(Diagonal(1)).ToList();
            foreach (var coords in possible)
            {
                if (coords.Valid)
                {
                    var pan = GameBoard.GetPanel(coords);
                    List<Panel> opponentPanels = GameBoard.GetOpponentPanels(GameBoard.GetPanel(Coordinates));
                    if (!opponentPanels.Contains(pan)) { output.Add(pan.Coordinates); }
                }
            }
            return output;
        }

        public bool IsInCheck
        {
            get
            {
                var pan = GameBoard.GetPanel(Coordinates);
                List<Panel> opponentPanels = GameBoard.GetOpponentPanels(pan);
                return opponentPanels.Contains(pan);
            }
        }
    }
}
