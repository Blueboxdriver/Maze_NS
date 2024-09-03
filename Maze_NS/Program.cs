using System;

namespace Maze_NS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome! How difficult do you want the maze?: ");
            Console.WriteLine("(1): Easy | (2): Medium | (3): Hard");

            int difficulty = Convert.ToInt32(Console.ReadLine());

            Maze genMaze = null; // Declare genMaze outside the switch statement

            switch (difficulty)
            {
                case 1:
                    Console.WriteLine("Easy maze selected");
                    genMaze = new Maze(5, 5);
                    break;
                case 2:
                    Console.WriteLine("Medium maze selected");
                    genMaze = new Maze(10, 10);
                    break;
                case 3:
                    Console.WriteLine("Hard maze selected");
                    genMaze = new Maze(20, 20);
                    break;
                default:
                    Console.WriteLine("How are you so stupid as to not type in the proper number? Try again.");
                    return; // Exit the program if the choice is invalid
            }
             genMaze.GenerateMaze();
             genMaze.PrintMaze();
            
        }
    }
}