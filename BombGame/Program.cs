using System;

namespace BombGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameGrid g = new GameGrid(5, 5);
            g.SetBombs(2);
            Player[] players = g.Start();
            g.Print();
            int mode;
            for (;;)
            {
                Console.Write("Player 1, move: ");
                mode = Convert.ToInt32(Console.ReadLine());
                g.Move(players[0], mode); 
                g.Print();
                Console.Write("Player 2, move: ");
                mode = Convert.ToInt32(Console.ReadLine());
                g.Move(players[1], mode); 
                g.Print();
            }
            Console.ReadKey();
        }
    }
}
