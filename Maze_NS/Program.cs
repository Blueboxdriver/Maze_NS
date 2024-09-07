using System;

namespace Maze_NS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool gameInProgress = true;
            string output = "";
            ConsoleColor color = ConsoleColor.White;

            Maze maze = new Maze(15, 15);

            while (gameInProgress)
            {
                Console.Clear();
                
                for (int y = 0; y < maze.height; y++)
                {
                    for (int x = 0; x < maze.width; x++)
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

                // Wait for the player to press a movement key
                var key = Console.ReadKey(true).Key;

                // Move the player
                maze.MovePlayer(key);

                // Check if player reached the exit
                if (maze.AtExit())
                {
                    Console.Clear();
                    Console.WriteLine("You've reached the exit! You win!");
                    gameInProgress = false; // End the loop
                }
                Console.ResetColor();
            }
        }
    }
}
