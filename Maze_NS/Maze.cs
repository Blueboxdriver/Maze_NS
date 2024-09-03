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
        private Tile[,] maze;
        private int cols;
        private int rows;

        public Maze(int rows, int columns)
        {
            this.rows = rows;
            this.cols = columns;
            this.maze = new Tile[rows, columns];
            //We'll call the generatemaze method when creating the object to save time.
            GenerateMaze();
        }

        public void GenerateMaze()
        {
            Random rand = new Random();

            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            // Nested for loop goes through the 2D array and then uses an if-statement to determine if the current array element is on the edge of the 2D array.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == 0 || i == rows - 1 || j == 0 || j == cols - 1 ) // The selected element is on the edge of the 2D array if i(rows) or j(columns) is equal to 0 or is one less than the length of the 2d Array
                    {
                        maze[i, j] = new Tile(true); //The Tile object for that element is flagged as a wall
                    } 
                    else
                    {
                        maze[i, j] = new Tile(false); // The tile object for that element is flagged as not a wall
                    }

                }
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

                    maze[i, j].PrintTile();
                }
                Console.WriteLine();
            }
        }
    }
}
