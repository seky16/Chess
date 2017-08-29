using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    public enum Col { A = 0, B, C, D, E, F, G, H }
    public enum Color { White, Black }

    public class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Valid
        {
            get
            {
                return (Row >= 0 && Row < 8) && (Column >= 0 && Column < 8);
            }
        }

        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public class Panel
    {
        public Coordinates Coordinates { get; set; }
        public bool IsPiece { get { return Piece != null; } }
        public Piece Piece { get; set; }

        public Panel(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
        }
    }

    public class Move
    {
        public Coordinates StartCoordinates { get; set; }
        public Coordinates EndCoordinates { get; set; }

        public Move(string start, string end)
        {
            StartCoordinates = ParseCoords(start);
            EndCoordinates = ParseCoords(end);
        }

        private Coordinates ParseCoords(string coords)
        {
            if (coords.Length != 2) { throw new InvalidMoveException(); }
            int row;
            if (!Int32.TryParse(coords[1].ToString(), out row)) { throw new InvalidMoveException(); };
            row = 8 - row;
            if (!(row >= 0 && row < 8)) { throw new InvalidMoveException(); };
            /*int row;
            char rowChar = coords[1];
                 if rowChar = '8' { column = 0; }
            else if rowChar = '7' { column = 1; }
            else if rowChar = '6' { column = 2; }
            else if rowChar = '5' { column = 3; }
            else if rowChar = '4' { column = 4; }
            else if rowChar = '3' { column = 5; }
            else if rowChar = '2' { column = 6; }
            else if rowChar = '1' { column = 7; }
            else { throw new InvalidMoveException(); };*/
            int column;
            char columnChar = coords[0];
            if (columnChar == 'A') { column = 0; }
            else if (columnChar == 'B') { column = 1; }
            else if (columnChar == 'C') { column = 2; }
            else if (columnChar == 'D') { column = 3; }
            else if (columnChar == 'E') { column = 4; }
            else if (columnChar == 'F') { column = 5; }
            else if (columnChar == 'G') { column = 6; }
            else if (columnChar == 'H') { column = 7; }
            else { throw new InvalidMoveException(); };
            return new Coordinates(row, column);
        }
    }

    public class GameBoard
    {
        public List<Panel> Panels { get; set; }

        public GameBoard()
        {
            Panels = new List<Panel>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Panels.Add(new Panel(i, j));
                }
            }
        }

        public Panel GetPanel(int row, int col)
        {
            return GetPanel(new Coordinates(row, col));
        }

        public Panel GetPanel(Coordinates coords)
        {
            Panel pan = Panels.Where(p => (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column)).FirstOrDefault() ?? throw new Exception();
            return pan;
        }

        public List<Panel> GetOpponentPanels(Panel panelOpponent)
        {
            var opponentPanels = new List<Panel>();
            foreach (var pan in Panels)
            {
                if (pan.IsPiece && pan.Piece.IsOpponent(panelOpponent))
                {
                    List<Coordinates> pieceMoves = pan.Piece.GetAvailableMoves();
                    foreach (var coords in pieceMoves)
                    {
                        opponentPanels.Add(GetPanel(coords));
                    }
                }
            }
            return opponentPanels.Distinct().ToList();
        }
    }

    public abstract class Piece
    {
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
        //public List<Coordinates> AvailableMoves { get { return GetAvailableMoves()} }
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
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
                if (pan.Coordinates.Valid)
                {
                    if (!pan.IsPiece) { output.Add(pan.Coordinates); continue; }
                    else if (IsOpponent(pan)) { output.Add(pan.Coordinates); break; }
                    else if (!IsOpponent(pan)) { break; }
                }
                else break;
            }
            return output;
        }

        //todo: review!!
        public void Place(Coordinates coords)
        {
            var panel = GameBoard.GetPanel(coords);
            panel.Piece = this;
        }

        public abstract List<Coordinates> GetAvailableMoves();
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

        public override List<Coordinates> GetAvailableMoves()
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

        public override List<Coordinates> GetAvailableMoves()
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

        public override List<Coordinates> GetAvailableMoves()
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

        public override List<Coordinates> GetAvailableMoves()
        {
            var board = GameBoard;
            var r = Coordinates.Row;
            var c = Coordinates.Column;
            var output = new List<Coordinates>();
            var possible = new List<Coordinates>()
        {
            board.GetPanel(r - 2, c + 1).Coordinates,
            board.GetPanel(r - 1, c + 2).Coordinates,
            board.GetPanel(r + 1, c + 2).Coordinates,
            board.GetPanel(r + 2, c + 1).Coordinates,
            board.GetPanel(r + 2, c - 1).Coordinates,
            board.GetPanel(r + 1, c - 2).Coordinates,
            board.GetPanel(r - 1, c - 2).Coordinates,
            board.GetPanel(r - 2, c - 1).Coordinates
        };


            foreach (var coords in possible)
            {
                if (coords.Valid)
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

        public override List<Coordinates> GetAvailableMoves()
        {
            var output = new List<Coordinates>();
            if (Color == Color.White)
            {
                var up = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column);
                if (up.Coordinates.Valid && !up.IsPiece) { output.Add(up.Coordinates); }
                var upL = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column - 1);
                if (upL.Coordinates.Valid && IsOpponent(upL)) { output.Add(upL.Coordinates); }
                var upR = GameBoard.GetPanel(Coordinates.Row - 1, Coordinates.Column + 1);
                if (upR.Coordinates.Valid && IsOpponent(upR)) { output.Add(upR.Coordinates); }
            }
            else
            {
                var down = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column);
                if (down.Coordinates.Valid && !down.IsPiece) { output.Add(down.Coordinates); }
                var downL = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column - 1);
                if (downL.Coordinates.Valid && IsOpponent(downL)) { output.Add(downL.Coordinates); }
                var downR = GameBoard.GetPanel(Coordinates.Row + 1, Coordinates.Column + 1);
                if (downR.Coordinates.Valid && IsOpponent(downR)) { output.Add(downR.Coordinates); }
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

        public override List<Coordinates> GetAvailableMoves()
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
