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
    
    private static List<Item> items = new List<Item>()
    {
        new Potion("Half empty K-Corp Ampule", "You found a Half Empty K-Corp Ampule! This miracle drug originates from K-Corp's singularity. Some of the liquid leaked out of this Ampule. ", 25),
        new Potion("Full K-Corp Ampule", "You found a Full K-Corp Ampule! Incredibly valuable in the backstreets, this full K-Corp Ampule can cure anything.", 50),
        new Weapon("Stigma Workshop Longsword", "You found a Stigma Workshop weapon! It's warm to the touch.", 15),
        new Weapon("Atelier Logic Handgun", "You found a Atelier Logic Handgun! Fortunately, it came with bullets.", 20)
    };

    public static Item GenerateItem()
    {
        Random rand = new Random();
        int i = rand.Next(items.Count);
    
        Item selectedItem = items[i];

        // Remove the item from the list if it is a weapon
        if (selectedItem is Weapon)
        {
            items.RemoveAt(i);
        }

        return selectedItem;
    }

    public virtual void OnPickUp()
    {
        Console.WriteLine(ItemPickUp);
    }

    public abstract void ApplyEffect(Player player);
}