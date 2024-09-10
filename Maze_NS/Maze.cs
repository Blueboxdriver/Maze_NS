namespace Maze_NS
{
    public class Maze : Tile
    {
        
        public Tile[,] tiles { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        private Random random = new Random();
        public Player Player { get; set; }
        public bool GameInProgress { get; set; }
        public bool BattleInProgress { get; set; }
        
        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new Tile[height, width];
            GameInProgress = true;
            BattleInProgress = false;

            GenMazeOutline();
            GenerateMaze(1, 1);
            
            PlaceExit();
            PlaceMonsters(Convert.ToInt32((width * height) / 35));
            PlaceItems(Convert.ToInt32((width * height) / 35));
            Player = new Player(1, 1);
        }

        private void GenMazeOutline()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[y, x] = new Tile(true);
                }
            }

            tiles[1, 1].IsPlayer = true;
        }

        private void GenerateMaze(int startX, int startY)
        {
            tiles[startY, startX].IsWall = false;
            tiles[startY, startX].IsVisited = true;

            foreach ((int, int) direction in GetRandomDirections())
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
            List<(int, int)> directions = new List<(int, int)>
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
            int oldY = Player.Y;
            int oldX = Player.X;
            
            int newX = Player.X;
            int newY = Player.Y;

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
                tiles[oldY, oldX].IsPlayer = false;
                
                Player.Move(newX, newY);

                tiles[newY, newX].IsPlayer = true;
            }
        }

        private void PlaceExit() // test
        {
            tiles[height - 2, width - 2].IsExit = true;
        }

        public bool AtExit()
        {
            bool result = false;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Player.X == x && Player.Y == y && tiles[y, x].IsExit && tiles[y, x].IsPlayer) 
                    {
                        result = true;
                    } 
                }
            }
            return result;
        }

        public bool AtMonster()
        {
            bool result = false;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Player.X == x && Player.Y == y && tiles[y, x].IsMonster && tiles[y, x].IsPlayer)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public void RemoveMonster()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Player.X == x && Player.Y == y && tiles[y, x].IsMonster && tiles[y, x].IsPlayer)
                    {
                        tiles[y, x].IsMonster = false;
                    }
                }
            }
        }

        private void PlaceMonsters(int amount)
        {
            int placedMonsters = 0;

            while (placedMonsters <= amount)
            {
                int x = random.Next(1, width - 1);
                int y = random.Next(1, height - 1);

                if (!tiles[y, x].IsWall && !tiles[y, x].IsExit && !tiles[y, x].IsPlayer)
                {
                    tiles[y, x].IsMonster = true;
                    placedMonsters++;
                }
            }
        }
        
        private void PlaceItems(int amount)
        {
            int placedItems = 0;

            while (placedItems <= amount)
            {
                int x = random.Next(1, width - 1);
                int y = random.Next(1, height - 1);

                if (!tiles[y, x].IsWall && !tiles[y, x].IsExit)
                {
                    tiles[y, x].IsItem = true;
                    placedItems++;
                }
            }
        }
    }
}