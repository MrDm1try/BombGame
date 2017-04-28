using System;

namespace BombGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameGrid g = new GameGrid(9, 9);
            g.SetBombs(6);
            Player[] players = g.Start();
            g.Print();
            int mode;
            bool p1_status, p2_status;
            for (;;)
            {
                do
                {
                    Console.Write("Player 1, move: ");
                    mode = Convert.ToInt32(Console.ReadLine());
                    p1_status = g.Move(players[0], mode);
                }
                while (p1_status == false);
                g.Print();
                do
                {
                    Console.Write("Player 2, move: ");
                    mode = Convert.ToInt32(Console.ReadLine());
                    p2_status = g.Move(players[1], mode); 
                }
                while (p2_status == false);
                g.Print();
            }
            Console.ReadKey();
        }
    }
}
