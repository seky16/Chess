// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System.Runtime.CompilerServices;

    public class Game
    {
        public Game(string player1Name, string player2Name)
        {
            this.GameBoard = new GameBoard(this);
            this.Player1 = new Player(player1Name, Color.White, this);
            this.Player2 = new Player(player2Name, Color.Black, this);
            this.Player1.PlacePieces();
            this.Player2.PlacePieces();
            this.WhoseMove = this.Player1;
            this.MoveCount = 1;
            this.FiftyMovesCount = 0;
        }

        public Game(GameBoard board)
        {
            this.GameBoard = board;
            this.Player1 = new Player("White", Color.White, this);
            this.Player2 = new Player("Black", Color.Black, this);

            this.Player1.Castled = true;
            this.Player1.KingMoved = true;
            this.Player1.LeftRookMoved = true;
            this.Player1.RightRookMoved = true;

            this.Player2.Castled = true;
            this.Player2.KingMoved = true;
            this.Player2.LeftRookMoved = true;
            this.Player2.RightRookMoved = true;
        }

        public GameBoard GameBoard { get; }

        public Player Player1 { get; }

        public Player Player2 { get; }

        public Player WhoseMove { get; set; }

        public Player WhitePlayer => this.Player1.Color == Color.White ? this.Player1 : this.Player2;

        public int MoveCount { get; set; }

        public int FiftyMovesCount { get; set; }
    }
}
