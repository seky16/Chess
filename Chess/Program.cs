// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System;

    internal static class Program
    {
        private static void Main()
        {
            /*var game = new Game("Player1", "Player2");
            var board = game.GameBoard;
            var player1 = game.Player1;
            var player2 = game.Player2;
            Console.WriteLine(board.Output());
            Console.WriteLine();
            Console.WriteLine(board.ToFEN());
            Console.WriteLine();
            while (!player1.CheckMate || !player2.CheckMate)
            {
                Console.Write($"{player1.Name}: ");
                var input1 = Console.ReadLine();
                var input2 = Console.ReadLine();
                var player1Move = new Move(input1, input2, player1);
                player1Move.Make();
                Console.Clear();
                Console.WriteLine(board.Output());
                Console.WriteLine();
                Console.WriteLine(board.ToFEN());
                Console.WriteLine();
                Console.WriteLine($"{player1.Name}: {player1Move.Display()}");

                if (player2.CheckMate)
                {
                    continue;
                }

                Console.Write($"{player2.Name}: ");
                var input3 = Console.ReadLine();
                var input4 = Console.ReadLine();
                var player2Move = new Move(input3, input4, player2);
                player2Move.Make();
                Console.Clear();
                Console.WriteLine(board.Output());
                Console.WriteLine();
                Console.WriteLine(board.ToFEN());
                Console.WriteLine();
                Console.WriteLine($"{player2.Name}: {player2Move.Display()}");
            }*/
            var fen = Console.ReadLine();
            var board = new GameBoard(fen);
            var input1 = Console.ReadLine();
            var input2 = Console.ReadLine();
            var player1Move = new Move(input1, input2, board.Game.Player1);
            Console.WriteLine(board.Output());
            player1Move.Make();
            Console.ReadLine();
            Console.WriteLine(board.Output());
            Console.ReadLine();
        }
    }
}
