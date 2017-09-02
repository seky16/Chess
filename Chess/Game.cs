// ReSharper disable StyleCop.SA1600

namespace Chess
{
    public class Game
    {
        public Game(string player1Name, string player2Name)
        {
            this.GameBoard = new GameBoard(this);
            this.Player1 = new Player(player1Name, Color.White, this);
            this.Player2 = new Player(player2Name, Color.Black, this);
            this.Player1.PlacePieces();
            this.Player2.PlacePieces();
        }

        public GameBoard GameBoard { get; }

        public Player Player1 { get; }

        public Player Player2 { get; }
    }
}
