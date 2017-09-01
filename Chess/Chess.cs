using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
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

        public string ShowPanel()
        {
            if (!IsPiece) { return " "; }
            else { return Piece.Show().ToString(); }
        }
    }

    public class Move
    {
        public Coordinates StartCoordinates { get; set; }
        public Coordinates EndCoordinates { get; set; }
        public GameBoard GameBoard { get; set; }
        public Player Player { get; set; }

        public Move(string start, string end, Player player)
        {
            Player = player;
            GameBoard = Player.GameBoard;
            StartCoordinates = ParseCoords(start);
            EndCoordinates = ParseCoords(end);
        }

        private Coordinates ParseCoords(string coords)
        {
            if (coords.Length != 2) { throw new InvalidMoveException(); }
            int row = -1;
            if (!Int32.TryParse(coords[1].ToString(), out row)) { throw new InvalidMoveException(); };
            row = 8 - row;
            if (!(row >= 0 && row < 8)) { throw new InvalidMoveException(); };
            int column;
            char columnChar = coords.ToUpper()[0];
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

        public string Display()
        {
            string output = String.Empty;
                 if (StartCoordinates.Column == 0) { output += "A"; }
            else if (StartCoordinates.Column == 1) { output += "B"; }
            else if (StartCoordinates.Column == 2) { output += "C"; }
            else if (StartCoordinates.Column == 3) { output += "D"; }
            else if (StartCoordinates.Column == 4) { output += "E"; }
            else if (StartCoordinates.Column == 5) { output += "F"; }
            else if (StartCoordinates.Column == 6) { output += "G"; }
            else if (StartCoordinates.Column == 7) { output += "H"; }
            output += (8 - StartCoordinates.Row).ToString() + " -> ";
                 if (EndCoordinates.Column == 0) { output += "A"; }
            else if (EndCoordinates.Column == 1) { output += "B"; }
            else if (EndCoordinates.Column == 2) { output += "C"; }
            else if (EndCoordinates.Column == 3) { output += "D"; }
            else if (EndCoordinates.Column == 4) { output += "E"; }
            else if (EndCoordinates.Column == 5) { output += "F"; }
            else if (EndCoordinates.Column == 6) { output += "G"; }
            else if (EndCoordinates.Column == 7) { output += "H"; }
            output += (8 - EndCoordinates.Row).ToString();
            return output;
        }

        public bool IsValid()
        {
            if (!GameBoard.GetPanel(StartCoordinates)?.IsPiece ?? false) { return false; }
            else
            {
                var startPiece = GameBoard.GetPanel(StartCoordinates).Piece;
                if (!Player.Pieces.Contains(startPiece)) { return false; }
                var moves = startPiece.GetAvailableMoves();
                /*bool endCoordsCheck = false;
                foreach (var coords in moves)
                {
                    if (coords.Row == EndCoordinates.Row && coords.Column == EndCoordinates.Column) { endCoordsCheck = true; break; }
                }*/
                bool endCoordsCheck = moves.Any(c => c.Row == EndCoordinates.Row && c.Column == EndCoordinates.Column);
                return StartCoordinates.Valid && EndCoordinates.Valid && endCoordsCheck;
            }
        }

        public void Make()
        {
            if (!IsValid()) { throw new InvalidMoveException(); }
            var startPiece = GameBoard.GetPanel(StartCoordinates).Piece;
            //if ((Player.King.IsInCheck && startPiece.Name != "King") { throw new InvalidMoveException("King is in check! You have to defend him!"); }
            startPiece.MoveTo(EndCoordinates);

            // checks for castling
            if (startPiece.Name == "King" && StartCoordinates.Column == 4) { Player.KingMoved = true; }
            if (startPiece.Name == "Rook")
            {
                if (StartCoordinates.Column == 0 && StartCoordinates.Row == Player.BaseRow) { Player.LeftRookMoved = true; }
                else if (StartCoordinates.Column == 7 && StartCoordinates.Row == Player.BaseRow) { Player.RightRookMoved = true; }
            }

            // castling
            if (startPiece.Name == "King" && StartCoordinates.Column == 4 && EndCoordinates.Column == 2 && StartCoordinates.Row == Player.BaseRow && EndCoordinates.Row == Player.BaseRow)
            {
                var p = GameBoard.GetPanel(Player.BaseRow, 0);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(GameBoard.GetPanel(Player.BaseRow, 3).Coordinates);
            }

            if (startPiece.Name == "King" && StartCoordinates.Column == 4 && EndCoordinates.Column == 6 && StartCoordinates.Row == Player.BaseRow && EndCoordinates.Row == Player.BaseRow)
            {
                var p = GameBoard.GetPanel(Player.BaseRow, 7);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(GameBoard.GetPanel(Player.BaseRow, 5).Coordinates);
            }
        }
    }

    public class GameBoard
    {
        public List<Panel> Panels { get; set; }
        public Game Game { get; set; }

        public GameBoard(Game game)
        {
            Game = game;
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
            if (!coords.Valid) { return null; }
            Panel pan = Panels.FirstOrDefault(p => (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column));
            /*Panel pan = null;
            foreach (var p in Panels)
            {
                if (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column) { pan = p; break; }
            }*/
            return pan;
        }

        /*public List<Panel> GetOpponentPanels(Panel panelOpponent)
        {
            var opponentPanels = new List<Panel>();
            foreach (var pan in Panels)
            {
                if (pan.IsPiece && (pan.Piece?.IsOpponent(panelOpponent) ?? false))
                {
                    if (pan.Piece.Name == "King") { List<Coordinates> pieceMoves = pan.Piece.Cross(1).Concat(pan.Piece.Diagonal(1)).ToList(); }
                    else { List<Coordinates> pieceMoves = pan.Piece.GetAvailableMoves(); }
                    foreach (var coords in pieceMoves)
                    {
                        opponentPanels.Add(GetPanel(coords));
                    }
                }
            }
            return opponentPanels.Distinct().ToList();
        }*/

        public string Output()
        {
            var output = new StringBuilder();
            string line = " +-+-+-+-+-+-+-+-+";
            output.AppendLine(line);
            for (int r = 0; r<8; r++)
            {
                output.Append((8-r).ToString() + "|");
                for (int c = 0; c<8; c++)
                {
                    output.Append(GetPanel(r, c).ShowPanel());
                    output.Append("|");
                }
                output.AppendLine();
                output.AppendLine(line);
            }
            output.Append("  A B C D E F G H");
            return output.ToString();
        }
    }
}
