using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_NS
{
    public class Tile
    {
        private bool isWall { get; set; }
        private bool isEmpty { get; set; }
        private bool isItem { get; set; }

        public Tile(bool isEmpty)
        {
            this.isEmpty = isEmpty;
        }

        public void SetWall() // Marks the tile as being a wall.
        {
            this.isWall = true;
            this.isEmpty = false;
        }

        public void SetItem() // Marks the tile as containing an item.
        {
            this.isItem = true;
            this.isWall = false;
            this.isEmpty = false;
        }

        public void PrintTile()
        {
            Console.Write(isWall ? "#" : " ");
        }

    }
}