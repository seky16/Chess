// ReSharper disable StyleCop.SA1600

namespace Chess
{
    public class Coordinates
    {
        public Coordinates(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; }

        public int Column { get; }

        public bool Valid => (this.Row >= 0 && this.Row < 8) && (this.Column >= 0 && this.Column < 8);

        public static Coordinates FromString(string coords)
        {
            if (coords.Length != 2)
            {
                throw new InvalidCoordsException();
            }

            if (!int.TryParse(coords[1].ToString(), out var row))
            {
                throw new InvalidCoordsException();
            }

            row = 8 - row;
            if (!(row >= 0 && row < 8))
            {
                throw new InvalidCoordsException();
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
                    throw new InvalidCoordsException();
            }

            return new Coordinates(row, column);
        }

        // ReSharper disable once InconsistentNaming
        public string ToSAN()
        {
            var output = string.Empty;
            switch (this.Column)
            {
                case 0:
                    output += "a";
                    break;
                case 1:
                    output += "b";
                    break;
                case 2:
                    output += "c";
                    break;
                case 3:
                    output += "d";
                    break;
                case 4:
                    output += "e";
                    break;
                case 5:
                    output += "f";
                    break;
                case 6:
                    output += "g";
                    break;
                case 7:
                    output += "h";
                    break;
                default:
                    throw new InvalidMoveException();
            }

            output += (8 - this.Row).ToString();
            return output;
        }

        public override string ToString() => this.ToSAN();
    }
}
