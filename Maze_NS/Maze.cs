using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_NS
{
    public class Maze
    {
        private int[,] maze;
        private object player; // just filler, replace string with the player object when made

        public Maze(int rows, int cols, object player)
        {
            this.maze = new int[rows, cols];
            this.player = player;
        }

        public void GenerateMaze()
        {
            Random rand = new Random();

            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            // finding the top and bottom borders.
            for (int i = 0; i < cols; i++)
            {
                maze[0, i] = -1; // top border
                maze[rows - 1, i] = -1; // bottom border
            }

            // finding the left and right borders
            for (int i = 0; i < rows; i++)
            {
                maze[i, 0] = -1; // left border
                maze[i, cols - 1] = -1; // right border
            }
        }

        public void PrintMaze()
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    
                    if (maze[i, j] == -1)
                        Console.Write("# ");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
        }
    }
}
