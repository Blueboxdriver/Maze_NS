using System;

namespace Maze_NS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playAgain = true;
            string response;
            while (playAgain)
            {
                Console.WriteLine("Welcome! How difficult do you want the maze?: ");
                Console.WriteLine("(1): Easy | (2): Medium | (3): Hard");

                int difficulty = Convert.ToInt32(Console.ReadLine());

                Maze genMaze = null; // Declare genMaze outside the switch statement

                switch (difficulty) // because i'm lazy the input is corresponds to the cases.
                {
                    case 1:
                        Console.WriteLine("Easy maze selected");
                        genMaze = new Maze(15, 15);
                        break;
                    case 2:
                        Console.WriteLine("Medium maze selected");
                        genMaze = new Maze(25, 25);
                        break;
                    case 3:
                        Console.WriteLine("Hard maze selected");
                        genMaze = new Maze(35, 35);
                        break;
                    default:
                        Console.WriteLine(
                            "How are you so stupid as to not type in the proper number? Try again."); // change this to be more friendly
                        return; // Exit the program if the choice is invalid
                }

                while (true)
                {
                    Console.Clear();
                    genMaze.PrintMaze();

                    ConsoleKey key = Console.ReadKey(true).Key;

                    genMaze.MovePlayer(key);
                    
                    if (genMaze.AtExit())
                    {
                        Console.Clear();
                        Console.WriteLine("Congrats! You escaped the maze!");
                        break;
                    }
                }

                Console.WriteLine("Would you like to play again? (y/n)");
                response = Console.ReadLine();
                if (response.Equals("y"))
                {
                    playAgain = true;
                }
                else
                {
                    playAgain = false;
                }

            }
        }
    }
}