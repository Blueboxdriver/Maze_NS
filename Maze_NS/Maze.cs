namespace Maze_NS
{
    public class Maze
    {
        
        private Tile[,] tiles;
        private int width;
        private int height;
        private Random random = new Random();
        private Player player;
        

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new Tile[height, width];

            GenMazeOutline();
            GenerateMaze(1, 1);
            PlaceExit();

            player = new Player(1, 1);
        }

        public void GenMazeOutline()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[y, x] = new Tile(true);
                }
            }
        }

        private void GenerateMaze(int startX, int startY)
        {
            tiles[startY, startX].IsWall = false;
            tiles[startY, startX].IsVisited = true;

            foreach (var direction in GetRandomDirections())
            {
                int newX = startX + direction.Item1 * 2;
                int newY = startY + direction.Item2 * 2;
                
                if (IsInBounds(newX, newY) && !tiles[newY, newX].IsVisited)
                {
                    tiles[startY + direction.Item2, startX + direction.Item1].IsWall = false;
                    
                    GenerateMaze(newX, newY);
                }
            }
        }

        private List<(int, int)> GetRandomDirections()
        {
            var directions = new List<(int, int)>
            {
                (0, -1), // north
                (0, 1), // south
                (-1, 0), // west
                (1, 0) // east
            };

            for (int i = directions.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (directions[i], directions[j]) = (directions[j], directions[i]);
            }

            return directions;
        }

        private bool IsInBounds(int x, int y)
        {
            return x > 0 && y > 0 && x < width - 1 && y < height - 1;
        }

        public void MovePlayer(ConsoleKey key)
        {
            int newX = player.X;
            int newY = player.Y;

            switch (key)
            {
                case ConsoleKey.W:
                    newY--;
                    break;
                case ConsoleKey.S:
                    newY++;
                    break;
                case ConsoleKey.A:
                    newX--;
                    break;
                case ConsoleKey.D:
                    newX++;
                    break;
            }

            if (IsInBounds(newX, newY) && !tiles[newY, newX].IsWall)
            {
                player.Move(newX, newY);
            }
        }

        private void PlaceExit()
        {
            var ranX = random.Next(width);
            var ranY = random.Next(height);

            if (IsInBounds(ranX, ranY) && !tiles[ranX, ranY].IsWall)
            {
                tiles[ranX, ranY].IsExit = true;
            }
            else
            {
                PlaceExit();
            }
        }

        public bool AtExit()
        {
            bool result = false;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (player.X == x && player.Y == y && tiles[y, x].IsExit)
                    {
                        result = true;
                    } 
                }
            }

            return result;
        }

        public void PrintMaze()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (player.X == x && player.Y == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" P ");
                    }
                    else if (tiles[y, x].IsWall)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" # ");
                    }
                    else if (tiles[y, x].IsExit)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" E ");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(" _ "); 
                    }
                }
                Console.WriteLine();
            }
        }
    }
}