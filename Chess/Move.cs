// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System.Linq;

    public class Move
    {
        public Move(string start, string end, Player player)
        {
            this.Player = player;
            this.GameBoard = this.Player.GameBoard;
            this.StartCoordinates = ParseCoords(start);
            this.EndCoordinates = ParseCoords(end);
        }

        private Coordinates StartCoordinates { get; }

        private Coordinates EndCoordinates { get; }

        private GameBoard GameBoard { get; }

        private Player Player { get; }

        public string Display()
        {
            var output = string.Empty;
            switch (this.StartCoordinates.Column)
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

            output += (8 - this.StartCoordinates.Row) + " -> ";
            switch (this.EndCoordinates.Column)
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

            output += (8 - this.EndCoordinates.Row).ToString();
            return output;
        }

        public void Make()
        {
            if (!this.IsValid())
            {
                throw new InvalidMoveException();
            }

            var startPiece = this.GameBoard.GetPanel(this.StartCoordinates).Piece;
            startPiece.MoveTo(this.EndCoordinates);

            // checks for castling
            switch (startPiece.Name)
            {
                case "King":
                    if (this.StartCoordinates.Column == 4)
                    {
                        this.Player.KingMoved = true;
                        break;
                    }
                    else break;
                case "Rook":
                    switch (this.StartCoordinates.Column)
                    {
                        case 0:
                            if (this.StartCoordinates.Row == this.Player.BaseRow)
                            {
                                this.Player.LeftRookMoved = true;
                                break;
                            }
                            else break;
                        case 7:
                            if (this.StartCoordinates.Row == this.Player.BaseRow)
                            {
                                this.Player.RightRookMoved = true;
                                break;
                            }
                            else break;
                    }

                break;
            }

            // castling
            if (startPiece.Name == "King" && this.StartCoordinates.Column == 4 && this.EndCoordinates.Column == 2 && this.StartCoordinates.Row == this.Player.BaseRow && this.EndCoordinates.Row == this.Player.BaseRow)
            {
                var p = this.GameBoard.GetPanel(this.Player.BaseRow, 0);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(this.GameBoard.GetPanel(this.Player.BaseRow, 3).Coordinates);
            }

            if (startPiece.Name != "King" || this.StartCoordinates.Column != 4 || this.EndCoordinates.Column != 6 || this.StartCoordinates.Row != this.Player.BaseRow || this.EndCoordinates.Row != this.Player.BaseRow)
            {
                return;
            }

            {
                var p = this.GameBoard.GetPanel(this.Player.BaseRow, 7);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(this.GameBoard.GetPanel(this.Player.BaseRow, 5).Coordinates);
            }
        }

        private static Coordinates ParseCoords(string coords)
        {
            if (coords.Length != 2)
            {
                throw new InvalidMoveException();
            }

            if (!int.TryParse(coords[1].ToString(), out var row))
            {
                throw new InvalidMoveException();
            }

            row = 8 - row;
            if (!(row >= 0 && row < 8))
            {
                throw new InvalidMoveException();
            }

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
            }

            return new Coordinates(row, column);
        }

        private bool IsValid()
        {
            if (!this.GameBoard.GetPanel(this.StartCoordinates)?.IsPiece ?? false)
            {
                return false;
            }

            var startPiece = this.GameBoard.GetPanel(this.StartCoordinates).Piece;
            if (!this.Player.Pieces.Contains(startPiece))
            {
                return false;
            }

            var moves = startPiece.GetAvailableMoves();
            var endCoordsCheck = moves.Any(c => c.Row == this.EndCoordinates.Row && c.Column == this.EndCoordinates.Column);
            return this.StartCoordinates.Valid && this.EndCoordinates.Valid && endCoordsCheck;
        }
    }
}
