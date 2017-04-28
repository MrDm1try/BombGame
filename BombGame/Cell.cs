using System;

namespace BombGame
{
    class Cell
    {
        bool hasBomb;
        bool visibleBomb;
        int row;
        int column;
        Player player;

        public Cell()
        {
            hasBomb = false;
            visibleBomb = false;
            player = null;
        }

        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
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

        public bool BombVisible()
        {
            return visibleBomb;
        }

        public void ShowBomb()
        {
            visibleBomb = true;
        }

        public void HideBomb()
        {
            visibleBomb = false;
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
            if (player != null)
                return player.ToString();
            return hasBomb && visibleBomb ? "@@" : "<>";
        }
    }
}
