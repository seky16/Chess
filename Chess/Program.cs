using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Player1", "Player2");
            var board = game.GameBoard;
            var player1 = game.Player1;
            var player2 = game.Player2;
            Console.WriteLine(board.Output());
            while (true)
            {
                Console.Write($"{player1.Name}: ");
                string input1 = Console.ReadLine();
                string input2 = Console.ReadLine();
                var player1Move = new Move(input1, input2, player1);
                player1Move.Make();
                Console.Clear();
                Console.WriteLine(board.Output());
                Console.WriteLine($"Player1: {player1Move.Display()}");

                Console.Write($"{player2.Name}: ");
                string input3 = Console.ReadLine();
                string input4 = Console.ReadLine();
                var player2Move = new Move(input3, input4, player2);
                player2Move.Make();
                Console.Clear();
                Console.WriteLine(board.Output());
                Console.WriteLine($"Player2: {player2Move.Display()}");
            }
        }
    }
}
