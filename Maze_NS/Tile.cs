namespace Maze_NS;

/// <summary>
///     Represents a tile in a maze. It has multiple properties that dictate what that tile is within a maze, such as a
///     wall, item, player, or monster.
/// </summary>
public class Tile
{
    /// <summary>
    ///     Gets or sets a T/F value on whether the tile is a wall.
    /// </summary>
    public Boolean IsWall { get; set; }

    /// <summary>
    ///     Gets or sets a T/F value on whether a tile has been visited by the <see cref="Maze.GenerateMaze" /> method.
    /// </summary>
    public Boolean IsVisited { get; set; }

    /// <summary>
    ///     Gets or sets a T/F value on whether the tile has a item.
    /// </summary>
    public Boolean IsItem { get; set; }

    /// <summary>
    ///     Gets or sets a T/F value on whether the tile has a monster.
    /// </summary>
    public Boolean IsMonster { get; set; }

    /// <summary>
    ///     Gets or sets a T/F value on whether the tile is the exit.
    /// </summary>
    public Boolean IsExit { get; set; }

    /// <summary>
    ///     Gets or sets a T/F value depending on whether the <see cref="Player" /> is on that tile.
    /// </summary>
    public Boolean IsPlayer { get; set; }

    /// <summary>
    ///     Creates an object of the <see cref="Tile" /> class with default values.
    ///     The default value of a tile is that it's a wall, and nothing else.
    /// </summary>
    public Tile()
    {
        IsWall = true;
        IsItem = false;
        IsVisited = false;
        IsExit = false;
        IsMonster = false;
    }
}