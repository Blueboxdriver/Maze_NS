namespace Maze_NS;
/// <summary>
/// Represents a monster in the maze and implements the <see cref="ICharacter"/> interface.
/// </summary>
public class Monster : ICharacter
{
    /// <inheritdoc />
    public int BaseDam { get; set; }
    /// <inheritdoc />
    public int Health { get; set; }
    /// <summary>
    /// Represents whether or not a monster is stunned.
    /// </summary>
    public bool IsStunned { get; set; }
    /// <summary>
    /// Represents the name of a monster.
    /// </summary>
    public string Name { get; set; }
    private static Random Rand = new Random();

    /// <summary>
    /// Constructor for a Monster object. Takes a string and two integers representing the Monster's name, health and damage.
    /// </summary>
    /// <param name="name">Determines the name of Monster</param>
    /// <param name="health">Determines the max health of the Monster</param>
    /// <param name="damage">Determines the damage a monster can inflict</param>
    public Monster(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        BaseDam = damage;
    }
    /// <summary>
    /// Randomly selects a monster object from a list of monsters.
    /// </summary>
    /// <returns>A Monster for the player to fight.</returns>
    public static Monster GenerateMonster()
    {
        List<Monster> monsters = new List<Monster>()
        {
            new Monster("Peccatulum Irae", 75, 10),
            new Monster("Peccatulum Morositatis", 75, 10),
            new Monster("Edgar House Butler", 100, 15),
            new Monster("Josephine of the Wild Hunt", 125, 20),
            new Monster("Hindley of the Wild Hunt", 125, 20),
            new Monster("Linton of the Wild Hunt", 125, 20)
        };

        int index = Rand.Next(monsters.Count);
        return monsters[index];
    }
    
    /// <summary>
    /// Corresponds a string to a specific monster
    /// </summary>
    /// <returns>The string that corresponds with that monster</returns>
    public string GetTalk()
    {
        return Name switch
        {
            "Peccatulum Irae" => "The creature most resembles a praying mantis, yet each part of its body is elongated and sharpened. " +
                                 "It stares at you with a singular wrathful eye.",
            "Peccatulum Morositatis" => "Most resembling a jellyfish, this Peccatulum looks almost as if it's going to cry. " +
                                        "Inside its membrane, a singular eye gloomful eye stares at you.",
            "Edgar House Butler" => "A butler contracted with the Edgar family. Adorned in sterotypical suit and tie. " +
                                    " However, you notice that the equipment he carries is far more dangerous than it appears.",
            "Josephine of the Wild Hunt" => "Some version of Josephine, Chief Butler of Wulthering Heights. " +
                                            "It's hard to tell what she's thinking, given the nature of her arrival.",
            "Hindley of the Wild Hunt" => "Some version of Hindley, former master of Wulthering Heights. " +
                                          "Compared to the others, he looks ragged and on the brink of distortion.",
            "Linton of the Wild Hunt" => "Some version of Linton, head of the Edgar family. It seems he was separated from his staff. " +
                                         "His strength betrays his sickly nature. You can't tell what he's thinking."
        };
    }


    /// <inheritdoc />
    public void InflictDamage(Player player, Monster monster)
    {
        player.Health -= monster.BaseDam;
    }
}