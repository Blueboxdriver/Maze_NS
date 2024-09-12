namespace Maze_NS;

public class Monster : ICharacter
{
    public int X { get; set; } //test
    public int Y { get; set; }
    public int Health { get; set; }
    public bool IsStunned { get; set; }
    public string Type { get; set; }
    private static Random Random = new Random();

    public Monster(string type, int health)
    {
        Type = type;
        Health = health;
    }

    public static Monster GenerateMonster()
    {
        List<Monster> monsters = new List<Monster>()
        {
            new Monster("Peccatulum Irae", 75),
            new Monster("Peccatulum Morositatis", 75),
            new Monster("Edgar House Butler", 100),
            new Monster("Josephine of the Wild Hunt", 125),
            new Monster("Hindley of the Wild Hunt", 125),
            new Monster("Linton of the Wild Hunt", 125)
        };

        int index = Random.Next(monsters.Count);
        return monsters[index];
    }

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
    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
    
    public void InflictDamage(Player player)
    {
        player.Health -= 10;
    }
}