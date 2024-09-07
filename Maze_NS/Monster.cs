namespace Maze_NS;

public class Monster : ICharacter
{
    public int X { get; set; } //test
    public int Y { get; set; }
    
    public int Health { get; set; }

    public Monster(int x, int y, int health = 100)
    {
        Health = health;
        X = x;
        Y = y;
    }
    
    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
}