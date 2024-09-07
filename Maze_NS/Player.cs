namespace Maze_NS;

public class Player : ICharacter
{
    public int Health { get; set; } //test
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public Player(int startX, int startY, int health = 100)
    {
        X = startX;
        Y = startY;
        Health = health;
    }

    public void Move(int newX, int newY)
    {
        X = newX;
        Y = newY;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
    public string getHealth()
    {
        return Health.ToString();
    }
    
}