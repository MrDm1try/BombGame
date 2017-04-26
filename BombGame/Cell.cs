using System;

namespace BombGame
{
    class Cell
    {
        bool hasBomb;
        Player player;

        public Cell()
        {
            hasBomb = false;
            player = null;
        }

        public void PutBomb()
        {
            hasBomb = true;
        }

        public void RemoveBomb()
        {
            hasBomb = false;
        }

        public bool HasBomb()
        {
            return hasBomb;
        }

        public void SetPlayer(Player p)
        {
            player = p;
        }

        public void RemovePlayer()
        {
            player = null;
        }

        public bool HasPlayer()
        {
            return player != null; 
        }

        public override String ToString()
        {
            return hasBomb ? "@@" : "<>";
        }
    }
}
