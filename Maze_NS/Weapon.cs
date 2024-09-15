namespace Maze_NS;
/// <summary>
/// Represents a weapon object that can be found by the Player. Inherits from the <see cref="Item"/> class
/// </summary>
public class Weapon : Item
{
    /// <summary>
    /// Creates a new object of the <see cref="Weapon"/> class with a description, pickup message and damage value.
    /// </summary>
    /// <param name="desc">The description of a weapon.</param>
    /// <param name="pickupMessage">The message displayed when the weapon is picked up.</param>
    /// <param name="damage">The damage value of the weapon.</param>
    public Weapon(string desc, string pickupMessage, int damage) : base(desc, pickupMessage, damage)
    {
    }
    /// <summary>
    /// Applies the weapon's <see cref="Item.ItemEffect"/> value to the player's <see cref="Player.BaseDam"/> value.
    /// </summary>
    /// <param name="player">The player whose damage value is being boosted.</param>
    public override void ApplyEffect(Player player)
    {
        player.BoostDamage(ItemEffect);
    }
    
}