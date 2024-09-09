namespace Maze_NS;

public interface ICharacter
{
    
    public int Health { get; set; }
    
    private void TakeDamage()
    {
    }

    public void InflictDamage(Player player, Monster monster)
    {
        
    }
}