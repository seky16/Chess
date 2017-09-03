// ReSharper disable StyleCop.SA1600

namespace Chess
{
    public class Panel
    {
        public Panel(int row, int column)
        {
            this.Coordinates = new Coordinates(row, column);
        }

        public Coordinates Coordinates { get; }

        public bool IsPiece => this.Piece != null;

        public Piece Piece { get; set; }

        public string ShowPanel()
        {
            return !this.IsPiece ? " " : this.Piece.Show().ToString();
        }
    }
}
