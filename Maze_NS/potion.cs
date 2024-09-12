namespace Maze_NS;

public class Potion : Item
{
    private int HealthRestored { get; set; }
    
    public Potion(string desc, string pickupMessage, int healthRestored) : base(desc, pickupMessage)
    {
        HealthRestored = healthRestored;
    }

    public override void ApplyEffect(Player player)
    {
        player.Health += HealthRestored;
        Console.WriteLine("Test");
    }
}