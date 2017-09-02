// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameBoard
    {
        public GameBoard(Game game)
        {
            this.Game = game;
            this.Panels = new List<Panel>();
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    this.Panels.Add(new Panel(i, j));
                }
            }
        }

        public List<Panel> Panels { get; }

        public Game Game { get; }

        public Panel GetPanel(int row, int col)
        {
            return this.GetPanel(new Coordinates(row, col));
        }

        public Panel GetPanel(Coordinates coords)
        {
            if (!coords.Valid)
            {
                return null;
            }

            var pan = this.Panels.FirstOrDefault(p => (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column));
            return pan;
        }

        public string Output()
        {
            var output = new StringBuilder();
            const string Line = " +-+-+-+-+-+-+-+-+";
            output.AppendLine(Line);
            for (var r = 0; r < 8; r++)
            {
                output.Append(8 - r + "|");
                for (var c = 0; c < 8; c++)
                {
                    output.Append(this.GetPanel(r, c).ShowPanel());
                    output.Append("|");
                }

                output.AppendLine();
                output.AppendLine(Line);
            }

            output.Append("  A B C D E F G H");
            return output.ToString();
        }
    }
}
