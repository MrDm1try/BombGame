using System;

namespace BombGame
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                GameGrid g = new GameGrid(21, 21);
                g.SetBombs(10);
                g.Print();
                Console.WriteLine();
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
