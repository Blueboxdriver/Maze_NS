namespace Maze_NS;

/// <summary>
///     Represents the maze itself, inheriting from <see cref="Tile" /> to represent what's inside the maze.
/// </summary>
public class Maze : Tile
{
    /// <summary>
    ///     A two dimensional array of <see cref="Tile" /> objects.
    /// </summary>
    public Tile[,] Tiles { get; set; }

    /// <summary>
    ///     Represents the width of the maze.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    ///     Represents the height of the maze.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    ///     Represents the Player used for every generated maze.
    /// </summary>
    public Player Player { get; set; }

    /// <summary>
    ///     Represents a T/F value depending on whether the game is in progress.
    /// </summary>
    public bool GameInProgress { get; set; }

    /// <summary>
    ///     Represents a T/F value depending on whether a battle is in progress.
    /// </summary>
    public bool BattleInProgress { get; set; }

    /// <summary>
    ///     Represents a random value.
    /// </summary>
    private readonly Random _random = new();

    /// <summary>
    ///     Constructs an object of the <see cref="Maze" /> class with specified dimensions for the maze.
    ///     When called, a maze is generated and them populated with <see cref="Item" />s, <see cref="Monster" />s, and a
    ///     <see cref="Player" />
    ///     after beginning the game.
    /// </summary>
    /// <param name="width">The width of the maze.</param>
    /// <param name="height">The height of the maze.</param>
    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new Tile[height, width];
        GameInProgress = true;
        BattleInProgress = false;

        // This is the hackiest thing I've done. It's awful.
        int randomWidth = _random.Next(1, width - 1);
        int randomHeight = _random.Next(1, height - 1);

        if (randomWidth % 2 == 0)
        {
            randomWidth++;
        }

        if (randomHeight % 2 == 0)
        {
            randomHeight++;
        }

        GenMazeOutline();
        GenerateMaze(randomWidth, randomHeight); // Will not generate correctly if given even integers.
        PlaceExit();
        PlaceMonsters(Convert.ToInt32(width * height / 35));
        PlaceItems(10);
        Player = new Player(1, 1);
    }

    /// <summary>
    ///     Generates a maze outline, where all tiles are walls.
    ///     Then, a player tile is generated on the top left.
    /// </summary>
    private void GenMazeOutline()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Tiles[y, x] = new Tile();
            }
        }

        Tiles[1, 1].IsPlayer = true;
    }

    /// <summary>
    ///     Generates the maze recursively utilizing a depth first search algorithm starting from the specified coordinates
    ///     Every tile visited by the algorithm is marked as visited and changes the status of <see cref="Tile.IsWall" /> to
    ///     false.
    /// </summary>
    /// <param name="startX">The starting X coordinate</param>
    /// <param name="startY">The starting Y coordinate</param>
    private void GenerateMaze(int startX, int startY)
        // inspired from: https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/
        // https://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
    {
        Tiles[startY, startX].IsWall = false;
        Tiles[startY, startX].IsVisited = true;

        foreach ((int, int) direction in GetRandomDirections())
        {
            int newX = startX + direction.Item1 * 2;
            int newY = startY + direction.Item2 * 2;

            if (IsInBounds(newX, newY) && !Tiles[newY, newX].IsVisited)
            {
                Tiles[startY + direction.Item2, startX + direction.Item1].IsWall = false;

                GenerateMaze(newX, newY);
            }
        }
    }

    /// <summary>
    ///     Gets a list of random directions for the <see cref="GenerateMaze" /> method.
    /// </summary>
    /// <returns>A shuffled list of tuples representing directions on the 2D maze array.</returns>
    private List<(int, int)> GetRandomDirections()
    {
        List<(int, int)> directions = new()
        {
            // This is the only time where the cordinates follow a (x, y) format.
            (0, -1), // north
            (0, 1), // south
            (-1, 0), // west
            (1, 0) // east
        };
        for (int i = directions.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (directions[i], directions[j]) = (directions[j], directions[i]);
        }

        return directions;
    }

    /// <summary>
    ///     Checks if the specified coordinates are within the bounds of the maze.
    /// </summary>
    /// <param name="x">the X coordinate that's to be checked.</param>
    /// <param name="y">the Y coordinate that's to be checked.</param>
    /// <returns>True or false depending on whether the coordinates are in bounds</returns>
    private bool IsInBounds(int x, int y)
    {
        return x > 0 && y > 0 && x < Width - 1 && y < Height - 1;
    }

    /// <summary>
    ///     Moves a player according to key input on a WASD format.
    ///     Prevents a player from moving into a <see cref="Tile" /> where <see cref="Tile.IsWall" /> is true.
    /// </summary>
    /// <param name="key">The key representing the direction.</param>
    public void MovePlayer(ConsoleKey key)
    {
        int oldY = Player.YCoord;
        int oldX = Player.XCoord;

        int newX = Player.XCoord;
        int newY = Player.YCoord;

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

        if (IsInBounds(newX, newY) && !Tiles[newY, newX].IsWall)
        {
            Tiles[oldY, oldX].IsPlayer = false;

            Player.Move(newX, newY);

            Tiles[newY, newX].IsPlayer = true;
        }
    }

    /// <summary>
    ///     Chooses the bottom right of any generated maze and switches that <see cref="Tile" />'s <see cref="Tile.IsExit" />
    ///     parameter to true.
    /// </summary>
    private void PlaceExit() // test
    {
        Tiles[Height - 2, Width - 2].IsExit = true;
    }

    /// <summary>
    ///     Checks if the player is at a <see cref="Tile" /> where <see cref="Tile.IsExit" /> is true.
    /// </summary>
    /// <returns>True if the <see cref="Player" /> is at the exit tile, false otherwise. </returns>
    public bool AtExit()
    {
        bool result = false;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Player.XCoord == x && Player.YCoord == y && Tiles[y, x].IsExit && Tiles[y, x].IsPlayer)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    /// <summary>
    ///     Checks to see if a <see cref="Player" /> is at a <see cref="Tile" /> where <see cref="Tile.IsMonster" /> is true.
    /// </summary>
    /// <returns>True if the <see cref="Player" /> is at a Monster tile, false otherwise</returns>
    public bool AtMonster()
    {
        bool result = false;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Player.XCoord == x && Player.YCoord == y && Tiles[y, x].IsMonster && Tiles[y, x].IsPlayer)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    /// <summary>
    ///     Checks to see if a <see cref="Player" /> is at a <see cref="Tile" /> where <see cref="Tile.IsItem" /> is true.
    /// </summary>
    /// <returns>True if the <see cref="Player" /> is at a Item tile, false otherwise</returns>
    public bool AtItem()
    {
        bool result = false;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Player.XCoord == x && Player.YCoord == y && Tiles[y, x].IsItem && Tiles[y, x].IsPlayer)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    /// <summary>
    ///     Sets the <see cref="Tile" /> the <see cref="Player" /> is currently on <see cref="Tile.IsItem" />'s status to
    ///     False.
    /// </summary>
    public void RemoveItem()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Player.XCoord == x && Player.YCoord == y && Tiles[y, x].IsItem && Tiles[y, x].IsPlayer)
                {
                    Tiles[y, x].IsItem = false;
                }
            }
        }
    }

    /// <summary>
    ///     Sets the <see cref="Tile" /> the <see cref="Player" /> is currently on <see cref="Tile.IsMonster" />'s status to
    ///     False.
    /// </summary>
    public void RemoveMonster()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Player.XCoord == x && Player.YCoord == y && Tiles[y, x].IsMonster && Tiles[y, x].IsPlayer)
                {
                    Tiles[y, x].IsMonster = false;
                }
            }
        }
    }

    /// <summary>
    ///     Places a specified amount of monsters into the maze.
    ///     If the method places a monster on a <see cref="Tile" /> where <see cref="Tile.IsWall" /> is already occupied, the
    ///     method tries again at a different <see cref="Tile" />.
    /// </summary>
    /// <param name="amount">The amount of Monsters to be placed</param>
    private void PlaceMonsters(int amount)
    {
        int placedMonsters = 0;

        while (placedMonsters <= amount)
        {
            int x = _random.Next(1, Width - 1);
            int y = _random.Next(1, Height - 1);

            if (!Tiles[y, x].IsWall && !Tiles[y, x].IsExit && !Tiles[y, x].IsPlayer && !Tiles[y, x].IsMonster &&
                !Tiles[y, x].IsItem)
            {
                Tiles[y, x].IsMonster = true;
                placedMonsters++;
            }
        }
    }

    /// <summary>
    ///     Places a specified amount of monsters into the maze.
    ///     If the method places a item on a <see cref="Tile" /> where <see cref="Tile.IsWall" /> is already occupied, the
    ///     method tries again at a different <see cref="Tile" />.
    /// </summary>
    /// <param name="amount">The amount of Monsters to be placed</param>
    private void PlaceItems(int amount)
    {
        int placedItems = 0;

        while (placedItems <= amount)
        {
            int x = _random.Next(1, Width - 1);
            int y = _random.Next(1, Height - 1);

            if (!Tiles[y, x].IsWall && !Tiles[y, x].IsExit && !Tiles[y, x].IsPlayer && !Tiles[y, x].IsMonster &&
                !Tiles[y, x].IsItem)
            {
                Tiles[y, x].IsItem = true;
                placedItems++;
            }
        }
    }
}