namespace Maze_NS;

public class Weapon : Item
{
    private double Damageboost { get; set; }

    public Weapon(string desc, string pickupMessage, double damageboost) : base(desc, pickupMessage)
    {
        Damageboost = damageboost;
    }

    public override void ApplyEffect(Player player)
    {
        player.boostDamage(Damageboost);
    }
}