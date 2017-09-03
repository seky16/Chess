// ReSharper disable StyleCop.SA1600

namespace Chess
{
    // todo: consider making this static, move parsing coordinates here from move.cs
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
