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
    public Maze(int width, int height) //Since this constructor is only called once, it constructs the entire maze by calling every relavent method.
    {
        Width = width;
        Height = height;
        Tiles = new Tile[height,
            width]; // Because the first value in a 2D array represents the rows, it becomes it the height. Vice versa for the Columns, and you get [yCoord,xCoord] instead of [xCoord,yCoord]
        GameInProgress = true;
        BattleInProgress = false;

        GenMazeOutline();
        GenerateMaze(_random.Next(0, width - 1), _random.Next(0, height - 1)); // Will not generate correctly if given even integers.
        PlaceExit();
        PlaceMonsters(Convert.ToInt32(width * height / 35)); // It's better to divide the area of the maze by 35 so the player isn't overwhelmed by monsters/items.
        PlaceItems(Convert.ToInt32(width * height / 35));    // For example, a 25x25 maze will spawn 18 Monsters and Items.
        Player = new Player(1, 1);
    }

    /// <summary>
    ///     Generates a maze outline, where all tiles are walls.
    ///     Then, a player tile is generated on the top left.
    /// </summary>
    private void GenMazeOutline()
    {
        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                Tiles[yCoord, xCoord] = new Tile();
            }
        }

        Tiles[1, 1].IsPlayer = true;
        Tiles[1, 1].IsEmpty = false;
    }

    /// <summary>
    ///     Generates the maze recursively utilizing a depth first search algorithm starting from the specified coordinates
    ///     Every tile visited by the algorithm is marked as visited and changes the status of <see cref="Tile.IsWall" /> to
    ///     false.
    /// </summary>
    /// <param name="startXCoord">The starting xCoord coordinate</param>
    /// <param name="startYCoord">The starting yCoord coordinate</param>
    private void GenerateMaze(int startXCoord, int startYCoord)
        // inspired from: https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/
        // https://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
    {
        if (startXCoord % 2 == 0)
        {
            startXCoord++;
        }

        //Admittedly, this is awful. However, for some God-awful reason the maze won't generate properly if you start from an even numbered position or if the dimensions are even.
        if (startYCoord % 2 == 0)
        {
            startYCoord++;
        }

        Tiles[startYCoord, startXCoord].IsWall = false;
        Tiles[startYCoord, startXCoord].IsEmpty = true; // Everytime we carve out a wall, we need to set the tile as visited, empty, and not a wall.
        Tiles[startYCoord, startXCoord].IsVisited = true;

        foreach ((int, int) direction in GetRandomDirections())
        {
            int newXCoord = startXCoord + direction.Item1 * 2;
            int newYCoord = startYCoord + direction.Item2 * 2;

            if (IsInBounds(newXCoord, newYCoord) && !Tiles[newYCoord, newXCoord].IsVisited)
            {
                Tiles[startYCoord + direction.Item2, startXCoord + direction.Item1].IsWall = false; // We do the same again, but without marking it as visited.
                Tiles[startYCoord + direction.Item2, startXCoord + direction.Item1].IsEmpty = true; // If we don't, all we do is carve out one spot every two spaces.

                GenerateMaze(newXCoord, newYCoord);
            }
        }
    }

    /// <summary>
    ///     Gets a list of random directions for the <see cref="GenerateMaze" /> method.
    /// </summary>
    /// <returns>A shuffled list of tuples representing directions on the 2D maze array.</returns>
    private List<(int, int)> GetRandomDirections()
    {
        List<(int, int)> directions =
        // It's easier to use a list with tuples instead of an enum. 
        [
            // This is the only time where the coordinates follow a (xCoord, yCoord) format.
            (0, -1), // north
            (0, 1), // south
            (-1, 0), // west
            (1, 0) // east
        ];
        for (int i = directions.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (directions[i], directions[j]) = (directions[j], directions[i]); // Swap whatever element corresponds to the count with a random element in that list.
        }

        return directions;
    }

    /// <summary>
    ///     Checks if the specified coordinates are within the bounds of the maze.
    /// </summary>
    /// <param name="xCoord">the xCoord coordinate that's to be checked.</param>
    /// <param name="yCoord">the yCoord coordinate that's to be checked.</param>
    /// <returns>True or false depending on whether the coordinates are in bounds</returns>
    private bool IsInBounds(int xCoord, int yCoord)
    {
        return xCoord > 0 && yCoord > 0 && xCoord < Width - 1 && yCoord < Height - 1;
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

        int newXCoord = Player.XCoord;
        int newYCoord = Player.YCoord;

        switch (key)
        {
            case ConsoleKey.W:
                newYCoord--;
                break;
            case ConsoleKey.S:
                newYCoord++;
                break;
            case ConsoleKey.A:
                newXCoord--;
                break;
            case ConsoleKey.D:
                newXCoord++;
                break;
        }

        if (IsInBounds(newXCoord, newYCoord) && !Tiles[newYCoord, newXCoord].IsWall) // Prevents the player from moving through walls or the boundary.
        {
            Tiles[oldY, oldX].IsPlayer = false;
            Tiles[oldY, oldX].IsEmpty = true;

            Player.Move(newXCoord, newYCoord);

            Tiles[newYCoord, newXCoord].IsPlayer = true;
            Tiles[newYCoord, newXCoord].IsEmpty = false;
        }
    }

    /// <summary>
    ///     Chooses the bottom right of any generated maze and switches that <see cref="Tile" />'s <see cref="Tile.IsExit" />
    ///     parameter to true.
    /// </summary>
    private void PlaceExit() // Just placing the exit in the same place, but I'm keeping this as a method in case I want to make it spawn randomly.
    {
        Tiles[Height - 2, Width - 2].IsExit = true;
        Tiles[Height - 2, Width - 2].IsEmpty = false;
    }

    /// <summary>
    ///     Checks if the player is at a <see cref="Tile" /> where <see cref="Tile.IsExit" /> is true.
    /// </summary>
    /// <returns>True if the <see cref="Player" /> is at the exit tile, false otherwise. </returns>
    public bool AtExit()
    {
        bool result = false;

        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                if (Tiles[yCoord, xCoord].IsExit && Tiles[yCoord, xCoord].IsPlayer)
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

        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                if (Tiles[yCoord, xCoord].IsMonster && Tiles[yCoord, xCoord].IsPlayer)
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

        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                if (Player.XCoord == xCoord && Player.YCoord == yCoord && Tiles[yCoord, xCoord].IsItem)
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
        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                if (Tiles[yCoord, xCoord].IsItem && Tiles[yCoord, xCoord].IsPlayer)
                {
                    Tiles[yCoord, xCoord].IsItem = false;
                    Tiles[yCoord, xCoord].IsEmpty = true;
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
        for (int yCoord = 0; yCoord < Height; yCoord++)
        {
            for (int xCoord = 0; xCoord < Width; xCoord++)
            {
                if (Tiles[yCoord, xCoord].IsMonster && Tiles[yCoord, xCoord].IsPlayer)
                {
                    Tiles[yCoord, xCoord].IsMonster = false;
                    Tiles[yCoord, xCoord].IsEmpty = true;
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
            int xCoord = _random.Next(1, Width - 1);
            int yCoord = _random.Next(1, Height - 1);

            if (Tiles[yCoord, xCoord].IsEmpty)
            {
                Tiles[yCoord, xCoord].IsMonster = true;
                Tiles[yCoord, xCoord].IsEmpty = false;
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
            int xCoord = _random.Next(1, Width - 1);
            int yCoord = _random.Next(1, Height - 1);

            if (Tiles[yCoord, xCoord].IsEmpty)
            {
                Tiles[yCoord, xCoord].IsItem = true;
                Tiles[yCoord, xCoord].IsEmpty = false;
                placedItems++;
            }
        }
    }
}