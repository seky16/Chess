using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new GameBoard();
            var player1 = new Player("Player1", Color.White, board);
            var player2 = new Player("Player2", Color.Black, board);
            Console.WriteLine(board.Output());
            Console.ReadLine();
        }
    }
}
