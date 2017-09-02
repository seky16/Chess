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
    }
}
