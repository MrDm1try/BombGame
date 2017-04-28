using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombGame
{
    class Player
    {
        int number;
        int[] start;
        int[] place;

        public Player(int n, int r, int c)
        {
            number = n;
            start = new int[] { r, c };
            place = new int[] { r, c };
        }

        public void Move(int r, int c)
        {
            place[0] = r;
            place[1] = c;
        }

        public int GetRow()
        {
            return place[0];
        }

        public int GetColumn()
        {
            return place[1];
        }

        public int[] GetPlace()
        {
            return place;
        }

        public void ToStart()
        {
            place[0] = start[0];
            place[1] = start[1];
        }

        public override string ToString()
        {
            return "P" + number;
        }
    }
}
