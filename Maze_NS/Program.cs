﻿namespace Maze_NS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Monster monster = new Monster("", 0, 0);
            Maze maze = null!;

            Console.WriteLine("Welcome to the Maze game, please select the maze's difficulty.");
            Console.WriteLine("Easy mode (15x15): [1] | Moderate mode (25x25): [2] | Hard mode (35x35) [3] | Grader Must Die (55x55): [4]");
            Console.WriteLine("Warning, Grader Must Die mode will likely not fit the console.");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    maze = new Maze(15, 15);
                    break;
                case 2:
                    maze = new Maze(25, 25);
                    break;
                case 3:
                    maze = new Maze(35, 35);
                    break;
                case 4:
                    maze = new Maze(55, 55);
                    break;
            }
            
            while (maze.GameInProgress)
            {
                Console.Clear();
                
                for (int y = 0; y < maze.height; y++)
                {
                    for (int x = 0; x < maze.width; x++) // test
                    {
                        var tile = maze.tiles[y, x];
                        string output;
                        var color = ConsoleColor.White;
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
                            default:
                                output = " _ ";
                                color = ConsoleColor.White;
                                break;
                        }

                        Console.ForegroundColor = color;
                        Console.Write(output);
                    }

                    Console.WriteLine();
                }
                Console.WriteLine($"Health: {maze.Player.Health}");
                if (maze.Player.currentWeapon != null)
                {
                    Console.WriteLine($"Equipped Weapon: {maze.Player.currentWeapon.ItemDesc}");
                }
                else
                {
                    Console.WriteLine("Equipped Weapon: None");
                }


                Console.WriteLine("Press [K] to access inventory");

                if (maze.AtItem())
                {
                    Item item = Item.GenerateItem();
                    item.OnPickUp();

                    if (item is Weapon weapon)
                    {
                        maze.Player.Inventory.Add(weapon);
                    }
                    else
                    {
                        item.ApplyEffect(maze.Player);
                    }
                    
                    maze.RemoveItem();
                }
                
                ConsoleKey action = Console.ReadKey(true).Key;

                if (action == ConsoleKey.K)
                {
                    Console.Clear();
                    Console.WriteLine("Inventory: ");
                    int i = 1;

                    List<Weapon> weaponList = new List<Weapon>();

                    foreach (var item in maze.Player.Inventory)
                    {
                        Console.WriteLine($"{i}: {item.ItemDesc}");

                        if (item is Weapon weapon)
                        {
                            weaponList.Add(weapon);
                        }

                        i++;
                    }

                    if (weaponList.Count > 1)
                    {
                        Console.WriteLine("\n Select a weapon to equip (enter the number): ");
                        if (int.TryParse(Console.ReadLine(), out int weaponChoice) && weaponChoice > 0 &&
                            weaponChoice <= weaponList.Count)
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
                    else if (weaponList.Count == 1)
                    {
                        Console.WriteLine("You only have one weapon equipped.");
                    }
                    else
                    {
                        Console.WriteLine("You have no weapons in your inventory.");
                    }
                    Console.ReadKey(true);
                }
                
                maze.MovePlayer(action);

                if (maze.AtMonster())
                {
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
                                    Console.WriteLine($"{monster.Name} retaliates for: 10 Damage!\n");
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
                        
                        if (monster.Health <= 0)
                        {
                            maze.RemoveMonster();
                            maze.GameInProgress = true;
                            maze.BattleInProgress = false;
                        }
                        else if (maze.Player.Health <= 0)
                        {
                            maze.GameInProgress = false;
                            maze.BattleInProgress = false;
                        }
                        else
                        {
                            maze.GameInProgress = false;
                            maze.BattleInProgress = true;
                        }
                    } while (maze.BattleInProgress);
                }
                
                if (maze.AtExit())
                {
                    Console.Clear();
                    Console.WriteLine("You've reached the exit! You win!");
                    Console.WriteLine($"Player Health: {maze.Player.GetHealth()}");
                    maze.GameInProgress = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You've died, game over.");
                    Console.WriteLine($"Killed by: {monster.Name}");
                }
                Console.ResetColor();
            }
        }
    }
}
