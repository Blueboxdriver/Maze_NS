namespace Maze_NS;

public class Monster : ICharacter
{
    public int X { get; set; } //test
    public int Y { get; set; }
    public int Health { get; set; }
    public string Type { get; set; }

    public Monster(string type, int health)
    {
        Type = type;
        Health = health;
    }
    private void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool IsAlive()
    {
        if (Health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void InflictDamage(Player player)
    {
        player.Health -= 10;
    }
}