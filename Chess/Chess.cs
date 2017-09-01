using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Chess
{
    public enum Color { White, Black }

    public class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Valid => (Row >= 0 && Row < 8) && (Column >= 0 && Column < 8);

        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public class Panel
    {
        public Coordinates Coordinates { get; set; }
        public bool IsPiece => Piece != null;
        public Piece Piece { get; set; }

        public Panel(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
        }

        public string ShowPanel()
        {
            return !IsPiece ? " " : Piece.Show().ToString();
        }
    }

    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
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

        private static Coordinates ParseCoords(string coords)
        {
            if (coords.Length != 2) { throw new InvalidMoveException(); }
            if (!int.TryParse(coords[1].ToString(), out var row)) { throw new InvalidMoveException(); };
            row = 8 - row;
            if (!(row >= 0 && row < 8)) { throw new InvalidMoveException(); };
            int column;
            var columnChar = coords.ToUpper()[0];
                 switch (columnChar)
                 {
                     case 'A':
                         column = 0;
                         break;
                     case 'B':
                         column = 1;
                         break;
                     case 'C':
                         column = 2;
                         break;
                     case 'D':
                         column = 3;
                         break;
                     case 'E':
                         column = 4;
                         break;
                     case 'F':
                         column = 5;
                         break;
                     case 'G':
                         column = 6;
                         break;
                     case 'H':
                         column = 7;
                         break;
                     default:
                         throw new InvalidMoveException();
                 };
            return new Coordinates(row, column);
        }

        public string Display()
        {
            var output = string.Empty;
            switch (StartCoordinates.Column)
            {
                case 0:
                    output += "A";
                    break;
                case 1:
                    output += "B";
                    break;
                case 2:
                    output += "C";
                    break;
                case 3:
                    output += "D";
                    break;
                case 4:
                    output += "E";
                    break;
                case 5:
                    output += "F";
                    break;
                case 6:
                    output += "G";
                    break;
                case 7:
                    output += "H";
                    break;
                default:
                    throw new InvalidMoveException();

            }
            output += (8 - StartCoordinates.Row) + " -> ";
            switch (EndCoordinates.Column)
            {
                case 0:
                    output += "A";
                    break;
                case 1:
                    output += "B";
                    break;
                case 2:
                    output += "C";
                    break;
                case 3:
                    output += "D";
                    break;
                case 4:
                    output += "E";
                    break;
                case 5:
                    output += "F";
                    break;
                case 6:
                    output += "G";
                    break;
                case 7:
                    output += "H";
                    break;
                default:
                    throw new InvalidMoveException();
            }
            output += (8 - EndCoordinates.Row).ToString();
            return output;
        }

        public bool IsValid()
        {
            if (!GameBoard.GetPanel(StartCoordinates)?.IsPiece ?? false) { return false; }
            var startPiece = GameBoard.GetPanel(StartCoordinates).Piece;
            if (!Player.Pieces.Contains(startPiece)) { return false; }
            var moves = startPiece.GetAvailableMoves();
            var endCoordsCheck = moves.Any(c => c.Row == EndCoordinates.Row && c.Column == EndCoordinates.Column);
            return StartCoordinates.Valid && EndCoordinates.Valid && endCoordsCheck;
        }

        public void Make()
        {
            if (!IsValid()) { throw new InvalidMoveException(); }
            var startPiece = GameBoard.GetPanel(StartCoordinates).Piece;
            startPiece.MoveTo(EndCoordinates);

            // checks for castling
            switch (startPiece.Name)
            {
                case "King" when StartCoordinates.Column == 4:
                    Player.KingMoved = true;
                    break;
                case "Rook":
                    switch (StartCoordinates.Column)
                    {
                        case 0 when StartCoordinates.Row == Player.BaseRow:
                            Player.LeftRookMoved = true;
                            break;
                        case 7 when StartCoordinates.Row == Player.BaseRow:
                            Player.RightRookMoved = true;
                            break;
                    }
                    break;
            }

            // castling
            if (startPiece.Name == "King" && StartCoordinates.Column == 4 && EndCoordinates.Column == 2 &&
                StartCoordinates.Row == Player.BaseRow && EndCoordinates.Row == Player.BaseRow)
            {
                var p = GameBoard.GetPanel(Player.BaseRow, 0);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(GameBoard.GetPanel(Player.BaseRow, 3).Coordinates);
            }

            if (startPiece.Name != "King" || StartCoordinates.Column != 4 || EndCoordinates.Column != 6 ||
                StartCoordinates.Row != Player.BaseRow || EndCoordinates.Row != Player.BaseRow) return;
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
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
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
            var pan = Panels.FirstOrDefault(p => (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column));
            return pan;
        }

        public string Output()
        {
            var output = new StringBuilder();
            const string line = " +-+-+-+-+-+-+-+-+";
            output.AppendLine(line);
            for (var r = 0; r<8; r++)
            {
                output.Append((8-r) + "|");
                for (var c = 0; c<8; c++)
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
