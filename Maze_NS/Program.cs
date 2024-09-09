using System;

namespace Maze_NS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool gameInProgress = true;
            bool battleInProgress = false;
            string output = "";
            ConsoleColor color = ConsoleColor.White;
            ConsoleKey action;

            Maze maze = null;

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
            
            while (gameInProgress)
            {
                Console.Clear();
                
                for (int y = 0; y < maze.height; y++)
                {
                    for (int x = 0; x < maze.width; x++) // test
                    {
                        var tile = maze.tiles[y, x];
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
                
                action = Console.ReadKey(true).Key;
                
                maze.MovePlayer(action);

                if (maze.AtMonster())
                {
                    battleInProgress = true;
                    gameInProgress = false;
                    Monster monster = new Monster("Erlking Heathcliffe", 100);
                    Console.Clear();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"You have encountered: {monster.Type} | {monster.Health} HP\n\n");
                        Console.WriteLine($"You have {maze.Player.Health} HP");
                        Console.WriteLine("What do you want to do? [1] Attack | [2] Defend | [3] Talk");
                        
                        action = Console.ReadKey(true).Key;

                        switch (action)
                        {
                            case ConsoleKey.D1:
                                maze.Player.InflictDamage(monster);
                                Console.WriteLine($"You attacked for: 10 Damage!");
                                Console.WriteLine("Press SPACE to continue.");
                                action = Console.ReadKey().Key;
                                break;
                            case ConsoleKey.D2:
                                Console.Write("Not Built Yet.");
                                break;
                        }
                        
                        if (!monster.IsAlive())
                        {
                            maze.RemoveMonster();
                            gameInProgress = true;
                            battleInProgress = false;
                        }
                        else
                        {
                            gameInProgress = false;
                            battleInProgress = true;
                        }
                    } while (battleInProgress);
                }
                
                if (maze.AtExit())
                {
                    Console.Clear();
                    Console.WriteLine("You've reached the exit! You win!");
                    Console.WriteLine($"Player Health: {maze.Player.GetHealth()}");
                    gameInProgress = false;
                }
                Console.ResetColor();
            }
        }
    }
}
