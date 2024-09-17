namespace Maze_NS;

/// <summary>
///     Represents a player in the maze and implements the <see cref="ICharacter" /> interface.
/// </summary>
public class Player : ICharacter
{
    /// <inheritdoc />
    public int Health { get; set; }

    /// <inheritdoc />
    public int BaseDam { get; set; }

    /// <summary>
    ///     Represents the players X coordinate in the maze.
    /// </summary>
    public int XCoord { get; private set; }

    /// <summary>
    ///     Represents the player's Y coordinate in the maze.
    /// </summary>
    public int YCoord { get; private set; }

    /// <summary>
    ///     Represents the player's inventory, containing items they've picked up in the maze.
    /// </summary>
    public List<Item> Inventory = new();

    /// <summary>
    ///     Represents the player's current weapon.
    /// </summary>
    public Weapon CurrentWeapon;

    /// <summary>
    ///     Creates a new object of the <see cref="Player" /> class with specified starting coordinates and health.
    /// </summary>
    /// <param name="startXCoord">The starting X coordinate of the player within the maze.</param>
    /// <param name="startYCoord">The starting Y coordinate of the player within the maze.</param>
    /// <param name="health">The starting health of the player within the maze.</param>
    public Player(int startXCoord, int startYCoord, int health = 100)
    {
        XCoord = startXCoord;
        YCoord = startYCoord;
        Health = health;
        BaseDam = 10;
        Weapon defaultWeapon = new("Basic Baton", "A baton you found when you woke up.", 10);
        Inventory.Add(defaultWeapon);
        EquipWeapon(defaultWeapon);
    }

    /// <summary>
    ///     Takes the damage value of a new weapon and applies it to the player's base damage value.
    /// </summary>
    /// <param name="amount">The damage value of a weapon</param>
    public void BoostDamage(int amount)
    {
        BaseDam = amount;
    }

    /// <summary>
    ///     Moves the player into a new coordinate in the maze.
    /// </summary>
    /// <param name="newX">The new X coordinate the player is moving to</param>
    /// <param name="newY">The new Y coordinate the player is moving to</param>
    public void Move(int newXCoord, int newYCoord)
    {
        XCoord = newXCoord;
        YCoord = newYCoord;
    }

    /// <summary>
    ///     Returns the current health a player has.
    /// </summary>
    /// <returns>Whatever value is in <see cref="Health" /></returns>
    public string GetHealth()
    {
        return Health.ToString();
    }

    /// <inheritdoc />
    public void InflictDamage(Player player, Monster monster)
    {
        monster.Health -= Convert.ToInt32(BaseDam);
    }

    /// <summary>
    ///     Switches the players currently equipped weapon with a new one.
    /// </summary>
    /// <param name="newWeapon">Represents the weapon the player is equipping.</param>
    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        BoostDamage(newWeapon.ItemEffect);
    }
}