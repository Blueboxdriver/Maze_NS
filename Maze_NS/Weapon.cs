namespace Maze_NS;

public class Weapon : Item
{
    public Weapon(string desc, string pickupMessage, int damage) : base(desc, pickupMessage, damage)
    {
    }

    public override void ApplyEffect(Player player)
    {
        player.BoostDamage(ItemEffect);
    }
}