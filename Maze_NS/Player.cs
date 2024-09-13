namespace Maze_NS;

public class Player : ICharacter
{
    public int Health { get; set; } //test
    public int BaseDam { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public List<Item> Inventory = new List<Item>();
    public Weapon currentWeapon;
    
    public Player(int startX, int startY, int health = 100)
    {
        X = startX;
        Y = startY;
        Health = health;
        BaseDam = 10;
        Weapon defaultWeapon = new Weapon("Basic Baton", "A baton you found when you woke up.", 10);
        Inventory.Add(defaultWeapon);
        EquipWeapon(defaultWeapon);
    }

    public void BoostDamage(int amount)
    {
        BaseDam = amount;
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
    public string GetHealth()
    {
        return Health.ToString();
    }
    
    public void InflictDamage(Player player, Monster monster)
    {
        monster.Health -= Convert.ToInt32(BaseDam);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        BoostDamage(newWeapon.Damageboost);
    }
}