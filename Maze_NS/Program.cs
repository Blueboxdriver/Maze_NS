namespace Maze_NS;

public class Program
{
    public static void Main(string[] args)
    {
        Maze maze = null!;
        bool success = false;

        // Prompts the user to create a maze.
        Console.WriteLine("Welcome to the Maze game, please select the maze's difficulty.");
        Console.WriteLine("Easy mode (15x15): [1] | Moderate mode (25x25): [2] | Hard mode (35x35) [3] | Grader Must Die (55x55): [4]");
        Console.WriteLine("Warning, Grader Must Die mode will likely not fit the console.");
        while (!success)
        {
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    maze = new Maze(15, 15);
                    success = true;
                    break;
                case 2:
                    maze = new Maze(25, 25);
                    success = true;
                    break;
                case 3:
                    maze = new Maze(35, 35);
                    success = true;
                    break;
                case 4:
                    maze = new Maze(55, 55);
                    success = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    break;
            }
        }

        // A loop that repeats after 1: Movement, 2: Combat encounter, 3: Inventory management.
        while (maze.GameInProgress)
        {
            Console.Clear();
            // Handles going through the maze and printing each tile based off their attribute.
            for (int yCoord = 0; yCoord < maze.Height; yCoord++)
            {
                for (int xCoord = 0; xCoord < maze.Width; xCoord++)
                {
                    Tile tile = maze.Tiles[yCoord, xCoord];
                    string output;
                    ConsoleColor color = ConsoleColor.White;
                    switch (tile)
                    {
                        case { IsWall: true }:
                            output = " # ";
                            color = ConsoleColor.Red;
                            break;
                        case { IsExit: true }:
                            output = " E ";
                            color = ConsoleColor.Blue;
                            break;
                        case { IsPlayer: true }:
                            output = " P ";
                            color = ConsoleColor.Green;
                            break;
                        case { IsMonster: true }:
                            output = " M ";
                            color = ConsoleColor.Magenta;
                            break;
                        case { IsItem: true }:
                            output = " I ";
                            color = ConsoleColor.Yellow;
                            break;
                        case { IsEmpty: true }:
                            output = " _ ";
                            color = ConsoleColor.White;
                            break;
                        default:
                            output = " ? ";
                            break;
                    }

                    Console.ForegroundColor = color;
                    Console.Write(output);
                }

                Console.WriteLine();
            }

            // Prints out basic player information.
            Console.WriteLine($"Health: {maze.Player.Health}");
            if (maze.Player.CurrentWeapon != null)
            {
                Console.WriteLine($"Equipped Weapon: {maze.Player.CurrentWeapon.ItemDesc}");
            }
            else
            {
                Console.WriteLine("Equipped Weapon: None");
            }

            Console.WriteLine("Press [K] to access inventory");

            // Randomly generates and applies or adds an item to the Player's inventory.
            if (maze.AtItem())
            {
                Item item = Item.GenerateItem();
                Console.WriteLine($"{item.ItemPickUp}");

                if (item is Weapon weapon)
                {
                    maze.Player.Inventory.Add(weapon);
                    if (weapon.ItemEffect > maze.Player.CurrentWeapon.ItemEffect)
                    {
                        maze.Player.EquipWeapon(weapon);
                    }
                }
                else
                {
                    item.ApplyEffect(maze.Player);
                }

                maze.RemoveItem();
            }

            ConsoleKey action = Console.ReadKey(true).Key;

            // Inventory / Weapon selection system.
            if (action == ConsoleKey.K)
            {
                Console.Clear();
                Console.WriteLine("Inventory: ");
                int i = 1;

                // This is the list we'll be using to store our weapons.
                List<Weapon> weaponList = [];
                // This parses through every item in our Inventory list, even though earlier we've made it so only weapons are added. 
                foreach (Item item in maze.Player.Inventory)
                {
                    Console.WriteLine($"{i}: {item.ItemDesc}");
                    // Removing this if statement would cut down on the code, but I think it's better to show and ensure that ONLY weapons are being added to weaponList
                    if (item is Weapon weapon)
                    {
                        weaponList.Add(weapon);
                    }

                    i++;
                }

                // Allows the player to equip a weapon if the amount of weapons in their inventory is more than one.
                if (weaponList.Count > 1)
                {
                    Console.WriteLine("\n Select a weapon to equip (enter the number): ");
                    // Input validation, ensures input is within range of the current amount of items in inventory.
                    if (int.TryParse(Console.ReadLine(), out int weaponChoice) && weaponChoice > 0 && weaponChoice <= weaponList.Count)
                    {
                        Weapon selectedWeapon = weaponList[weaponChoice - 1];
                        maze.Player.EquipWeapon(selectedWeapon);
                        Console.WriteLine($"Equipped Weapon: {selectedWeapon.ItemDesc}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection");
                    }
                }

                Console.ReadKey(true);
            }

            maze.MovePlayer(action);

            // Displays a combat encounter with a random monster if the player is on a monster tile.
            if (maze.AtMonster())
            {
                Monster monster;
                maze.BattleInProgress = true;
                maze.GameInProgress = false;
                monster = Monster.GenerateMonster();

                Console.Clear();
                do
                {
                    Console.Clear();
                    Console.WriteLine($"You have encountered: {monster.Name} | {monster.Health} HP\n\n");
                    Console.WriteLine($"You have {maze.Player.Health} HP");
                    Console.WriteLine("What do you want to do? [1] Attack | [2] Stun | [3] Identify\n");

                    action = Console.ReadKey(true).Key;
                    // Implements the interaction of two ICharacter objects, in this case it's in the form of a fight.
                    switch (action)
                    {
                        case ConsoleKey.D1:
                            maze.Player.InflictDamage(maze.Player, monster);
                            Console.WriteLine($"You attacked for: {Convert.ToInt32(maze.Player.BaseDam)}!\n");

                            if (monster.IsStunned)
                            {
                                Console.WriteLine($"{monster.Name} is stunned, therefore {monster.Name} cannot retaliate!\n");
                                monster.IsStunned = false;
                            }
                            else
                            {
                                Console.WriteLine($"{monster.Name} retaliates for: {monster.BaseDam} Damage!\n");
                                monster.InflictDamage(maze.Player, monster);
                            }

                            Console.WriteLine("Press any key to continue.");
                            action = Console.ReadKey().Key;
                            break;

                        case ConsoleKey.D2:
                            Console.WriteLine($"You stunned {monster.Name}!\n");
                            monster.IsStunned = true;
                            Console.WriteLine("Press any key to continue.");
                            action = Console.ReadKey().Key;
                            break;

                        case ConsoleKey.D3:
                            Console.WriteLine($"You take a closer look at {monster.Name}\n");
                            Console.WriteLine($"{monster.Name}: " + monster.GetTalk() + "\n");
                            Console.WriteLine("Press any key to continue.");
                            action = Console.ReadKey().Key;
                            break;
                    }

                    // If the round ends in any of these two scenarios, they implement their expected outcomes, otherwise a new round of combat is started.
                    if (monster.Health <= 0)
                    {
                        maze.RemoveMonster();
                        maze.GameInProgress = true;
                        maze.BattleInProgress = false;
                    }
                    else if (maze.Player.Health <= 0)
                    {
                        Console.Clear();
                        maze.GameInProgress = false;
                        maze.BattleInProgress = false;
                        Console.Clear();
                        Console.WriteLine("You've died, game over.");
                        Console.WriteLine($"Killed by: {monster.Name}");
                    }
                    else
                    {
                        maze.GameInProgress = false;
                        maze.BattleInProgress = true;
                    }
                } while (maze.BattleInProgress);
            }

            // Displays a winning screen if the player reaches the exit. If the player dies, display a game over screen.
            if (maze.AtExit())
            {
                Console.Clear();
                Console.WriteLine("You've reached the exit! You win!");
                Console.WriteLine($"Player Health: {maze.Player.GetHealth()}");
                maze.GameInProgress = false;
            }

            Console.ResetColor();
        }
    }
}