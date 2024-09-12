namespace Maze_NS;

public class Weapon : Item
{
    public double Damageboost { get; private set; }

    public Weapon(string desc, string pickupMessage, double damage) : base(desc, pickupMessage)
    {
        Damageboost = damage;
    }

    public override void ApplyEffect(Player player)
    {
        player.BoostDamage(Damageboost);
    }
}