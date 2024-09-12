namespace Maze_NS;

public class Monster : ICharacter
{
    /// <summary>
    /// <param name="Health">Integer containing the max health of a monster.</param>
    /// <param name="IsStunned">Boolean that returns true if the monster is stunned.</param>
    /// <param name="Type">The type of monster, essentially it's name.</param>
    /// <param name="Rand">A Random object used to generate a random monster to spawn.</param>
    /// <param name="DamValue">Integer containing the attack value a monster has.</param>
    /// </summary>
    public int DamValue { get; set; }
    public int Health { get; set; }
    public bool IsStunned { get; set; }
    public string Type { get; set; }
    private static Random Rand = new Random();

    /// <summary>
    /// Constructor for a Monster object. Takes a string and an integer representing the Monster's type and max Health respectively.
    /// </summary>
    /// <param name="type">Determines the type of Monster</param>
    /// <param name="health">Determines the max health of the Monster</param>
    public Monster(string type, int health, int damage)
    {
        Type = type;
        Health = health;
        DamValue = damage;
    }
    /// <summary>
    /// Randomly selects a monster object from a list of monsters.
    /// </summary>
    /// <returns>A Monster Object</returns>
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
        return Type switch
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

    /// <summary>
    /// Takes a Player object as a target and subtracts 10 from their total health
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monster"></param>
    public void InflictDamage(Player player, Monster monster)
    {
        player.Health -= monster.DamValue;
    }
}