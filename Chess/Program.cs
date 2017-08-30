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
            string input1 = Console.ReadLine();
            string input2 = Console.ReadLine();
            var player1Move = new Move(input1, input2, player1);
            player1Move.Make();
            Console.WriteLine($"Player1: {player1Move.Display()}");
            string input3 = Console.ReadLine();
            string input4 = Console.ReadLine();
            var player2Move = new Move(input1, input2, player2);
            player2Move.Make();
            Console.WriteLine($"Player2: {player2Move.Display()}");
        }
    }
}
