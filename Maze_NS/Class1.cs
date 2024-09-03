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

        public Tile(bool isWall)
        {
            this.isWall = isWall;
        }

        public void PrintTile()
        {
            Console.Write(isWall ? "# " : " ");
        }

    }
}