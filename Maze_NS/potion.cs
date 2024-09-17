namespace Maze_NS;

/// <summary>
///     Represents a potion object that can be found by the Player. Inherits from the <see cref="Item" /> class
/// </summary>
public class Potion : Item
{
    /// <summary>
    ///     Creates a new object of the <see cref="Weapon" /> class with a description, pickup message and damage value.
    /// </summary>
    /// <param name="desc">The description of a potion.</param>
    /// <param name="pickupMessage">The message displayed when the potion is picked up.</param>
    /// <param name="healthRestored">The amount of health restored</param>
    public Potion(string desc, string pickupMessage, int healthRestored) : base(desc, pickupMessage, healthRestored)
    {
    }

    /// <summary>
    ///     Adds the potion's <see cref="Item.ItemEffect" /> value to the player's <see cref="Player.Health" />
    /// </summary>
    /// <param name="player">The player who's damage value is being boosted.</param>
    public override void ApplyEffect(Player player)
    {
        player.Health += ItemEffect;
    }
}