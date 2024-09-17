namespace Maze_NS;

public interface ICharacter
{
    /// <summary>
    ///     Represent a character's health.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    ///     Represent's a character base damage.
    /// </summary>
    public int BaseDam { get; set; }

    /// <summary>
    ///     Provides the ability for a character's health to be subtracted by another's base damage.
    /// </summary>
    /// <param name="player">A player object that can either inflict or take damage.</param>
    /// <param name="monster">A monster object that can either inflict or take damage.</param>
    public void InflictDamage(Player player, Monster monster);
}