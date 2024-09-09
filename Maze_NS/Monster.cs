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
            new Monster("Greed Peccatula", 75),
            new Monster("Pride Peccatula", 75),
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
            "Greed Peccatula" => "The creature doesn't respond to your attempts of communication.",
            "Pride Peccatula" => "The creature doesn't respond to your attempts of communication.",
            "Edgar House Butler" => "You're the cause of all of this!",
            "Josephine of the Wild Hunt" => "I knew one day you would return and ruin everything!",
            "Hindley of the Wild Hunt" => "This decaying manor... must crumble, as it should be.",
            "Linton of the Wild Hunt" => "figure this out later"
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