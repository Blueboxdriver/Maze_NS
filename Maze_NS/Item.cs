namespace Maze_NS;

public abstract class Item
{
    public string ItemDesc { get; set; }
    public string ItemPickUp { get; set; }
    public double ItemEffect { get; set; }

    public Item(string itemdesc, string itempickup)
    {
        ItemDesc = itemdesc;
        ItemPickUp = itempickup;
    }

    public static Item GenerateItem()
    {
        List<Item> items = new List<Item>()
        {
            new Potion("Half empty K-Corp Ampule", "A miracle drug originating from K-Corp's singularity. Some of the liquid leaked out of this Ampule. ", 25),
            new Potion("Full K-Corp Ampule", "Incredibly valuable in the backstreets, this full K-Corp Ampule can cure anything.", 50),
            new Weapon("Stigma Workshop Longsword", "You found a Stigma Workshop weapon! It's warm to the touch.", .2),
            new Weapon("Atelier Logic Handgun", "You found a Atelier Logic Handgun! Fortunately, it came with bullets.", .3)
        };
        
        Random rand = new Random();
        return items[rand.Next(items.Count)];
    }

    public virtual void OnPickUp()
    {
        Console.WriteLine(ItemPickUp);
    }

    public abstract void ApplyEffect(Player player);
}