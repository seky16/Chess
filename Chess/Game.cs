using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Game
    {
        public GameBoard GameBoard { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game(string player1Name, string player2Name)
        {
            GameBoard = new GameBoard(this);
            Player1 = new Player(player1Name, Color.White, this);
            Player2 = new Player(player2Name, Color.Black, this);
            Player1.PlacePieces();
            Player2.PlacePieces();
        }
    }
}
