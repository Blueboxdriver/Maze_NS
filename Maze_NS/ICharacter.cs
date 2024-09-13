namespace Maze_NS;

public interface ICharacter
{
    public int Health { get; set; }
    public int BaseDam {get; set;}
    public void InflictDamage(Player player, Monster monster);
}