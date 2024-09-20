namespace Maze_NS;

/// <summary>
///     Represents an abstract item that can be found in the maze that can be picked up by a player and applied for various
///     effects.
/// </summary>
public abstract class Item
{
    /// <summary>
    ///     A string containing the description of an item.
    /// </summary>
    public string ItemDesc { get; set; }

    /// <summary>
    ///     A string containing the message that should be displayed when picking up the item.
    /// </summary>
    public string ItemPickUp { get; set; }

    /// <summary>
    ///     An integer that sets the effect value of an item.
    /// </summary>
    public int ItemEffect { get; set; }

    /// <summary>
    ///     A list of predefined items that can be found in the maze.
    /// </summary>
    private static readonly List<Item> _items =
    [
        new Potion("Bandage", "You found a bandage, at least it's better than nothing.", 25),
        new Potion("Half empty K-Corp Ampule", "You found a Half Empty K-Corp Ampule! This miracle drug originates from K-Corp's singularity. Some of the liquid leaked out of this Ampule. ", 50),
        new Potion("Full K-Corp Ampule", "You found a Full K-Corp Ampule! Incredibly valuable in the backstreets, this full K-Corp Ampule can cure anything.", 100),
        new Weapon("Stigma Workshop Longsword", "You found a Stigma Workshop weapon! It's warm to the touch.", 15),
        new Weapon("Atelier Logic Handgun", "You found a Atelier Logic Handgun! Fortunately, it came with bullets.", 20),
        new Weapon("Zelkova Workshop Mace", "You found a Zelkova Workshop Mace! Go bash something in the skull with it", 12),
        new Weapon("Rosespanner Workshop Spear", "You found a Rosespanner Workshop Spear! The chain on the end spurs to life, begging for a target.", 25)
    ];

    /// <summary>
    ///     Creates an object of the <see cref="Item" /> class containing a description, pickup message and value for the
    ///     effect.
    /// </summary>
    /// <param name="itemDesc">The description of an item.</param>
    /// <param name="itemPickup">The pickup message of an item.</param>
    /// <param name="itemEffect">The effect value of an item.</param>
    protected Item(string itemDesc, string itemPickup, int itemEffect)
    {
        ItemDesc = itemDesc;
        ItemPickUp = itemPickup;
        ItemEffect = itemEffect;
    }

    /// <summary>
    ///     Selects a random item from <see cref="_items" />.
    ///     If the item selected is a weapon, it removes the item from <see cref="_items" /> to prevent duplicates.
    /// </summary>
    /// <returns>The randomly selected item.</returns>
    public static Item GenerateItem()
    {
        Random rand = new();
        int i = rand.Next(_items.Count);
        Item selectedItem = _items[i];
        if (selectedItem is Weapon) // Removes the item from _items if it's of the weapons class
        {
            _items.RemoveAt(i);
        }

        return selectedItem;
    }

    /// <summary>
    ///     Abstract method that applies an item's effect to a player.
    /// </summary>
    /// <param name="player">The player an item's effect is being applied to</param>
    public abstract void ApplyEffect(Player player);
}