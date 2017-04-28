using System;
using System.Collections.Generic;
using System.Linq;

namespace BombGame
{
    class GameGrid
    {
        Cell[,] grid;
        List<int[]> visited;
        Player player1;
        Player player2;

        public GameGrid(int rows, int columns)
        {
            if (rows % 2 == 0)
                throw new ArgumentException("Number of rows should be odd");
            grid = new Cell[rows, columns];
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    grid[i, j] = new Cell();
            visited = new List<int[]>();
        }

        public void SetBombs(int bombsPerRow)
        {
            Random r = new Random();
            do
            {
                ClearBombs();
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    foreach (int j in GenerateBombPositions(i, bombsPerRow, r))
                    {
                        grid[i, j].PutBomb();
                    }
                }
            } while (!CheckPath());
            Console.WriteLine();
        }
        
        public void ClearBombs()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    grid[i, j].RemoveBomb();

        }

        public List<int[]> GetMoves(int row, int column)
        {
            int[,] pattern =
                row % 2 == 0 ?
                new int[,] { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 0, -1 } } :
                new int[,] { { -1, -1 }, { -1, 0 }, { 0, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } ;
            List<int[]> toReturn = new List<int[]>();
            int r, c;
            for (int i = 0; i < pattern.GetLength(0); i++)
            {
                r = row + pattern[i, 0];
                c = column + pattern[i, 1];
                if ((r >= 0) && (c >= 0) && 
                    (r < grid.GetLength(0)) && (c < grid.GetLength(1)) && 
                    !(r % 2 == 0 && c == grid.GetLength(1)-1) &&
                    !(grid[r,c].HasBomb()) && !(grid[r,c].HasPlayer()))
                    toReturn.Add(new int[] { r, c });
            }
            return toReturn;

        }

        private bool CheckPath()
        {
            bool toReturn = CheckPath(grid.GetLength(0) - 1, 0);
            visited.Clear();
            return toReturn;
        }

        private bool CheckPath(int r, int c)
        {
            visited.Add(new int[] { r, c });
            if ((r == 0) && (c == grid.GetLength(1) - 2))
                return true;
            if (grid[r, c].HasBomb() || grid[r, c].HasPlayer())
                return false;
            List<int[]> moves = GetMoves(r, c);
            foreach (int[] move in moves)
            {
                if (!Visited(move) && CheckPath(move[0], move[1]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Visited(int[] cell)
        {
            foreach (int[] v in visited)
            {
                if (v[0] == cell[0] && v[1] == cell[1])
                {
                    return true;
                }
            }
            return false;
        }

        private int[] GenerateBombPositions(int row, int bombsPerRow, Random r)
        {
            int[] numbers;
            if (row == 0)
                numbers = Enumerable.Range(0, grid.GetLength(1) - 2).ToArray();
            else if (row == grid.GetLength(0) - 1)
                numbers = Enumerable.Range(1, grid.GetLength(1) - 1).ToArray();
            else
                numbers =
                row % 2 == 0 ?
                Enumerable.Range(0, grid.GetLength(1) - 1).ToArray() :
                Enumerable.Range(0, grid.GetLength(1)).ToArray();
            int[] bombsPositions = new int[bombsPerRow];
            int s = numbers.Length;
            int pos;
            for (int i = 0; i < bombsPerRow; i++)
            {
                pos = r.Next(0, s--);
                bombsPositions[i] = numbers[pos];
                numbers[pos] = numbers[s];
            }
            return bombsPositions;
        }

        public Player[]  Start()
        {
            player1 = new Player(1, grid.GetLength(0) - 1, 0);
            grid[grid.GetLength(0) - 1, 0].SetPlayer(player1);
            player2 = new Player(2, 0, grid.GetLength(0) - 2);
            grid[0, grid.GetLength(0) - 2].SetPlayer(player2);
            return new Player[] { player1, player2 };
        }

        public bool Move(Player player, int way)
        {
            int r = player.GetRow();
            int c = player.GetColumn();
            int[,] pattern =
                r % 2 == 0 ?
                new int[,] { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 0, -1 } } :
                new int[,] { { -1, -1 }, { -1, 0 }, { 0, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } ;
            int rn = r + pattern[way - 1, 0];
            int cn = c + pattern[way - 1, 1];

            if ((rn < 0) || (cn < 0) ||
                (rn >= grid.GetLength(0)) || (cn >= grid.GetLength(1)) ||
                (rn % 2 == 0 && cn == grid.GetLength(1) - 1) ||
                grid[rn, cn].HasPlayer() ||
                (grid[rn, cn].HasBomb() && grid[rn, cn].BombVisible()))
            {
                return false;
            }
            if (grid[rn, cn].HasBomb())
            {
                grid[rn, cn].ShowBomb();
                grid[r, c].RemovePlayer();
                player.ToStart();
                grid[player.GetRow(), player.GetColumn()].SetPlayer(player);
            }
            else
            {
                grid[r, c].RemovePlayer();
                grid[rn, cn].SetPlayer(player);
                player.Move(rn, cn);
            }
            return true;
        }

        public void Print()
        {
            Console.WriteLine();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (i % 2 == 0)
                    Console.Write(" ");
                for (int j = 0; j < (i % 2 == 0 ? grid.GetLength(1)-1 : grid.GetLength(1)); j++)
                    Console.Write(grid[i, j]);
                Console.WriteLine();
            }  

        }

    }
}
