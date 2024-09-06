namespace Maze_NS;

public class Player
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }
    public void Move(int newX, int newY)
    {
        X = newX;
        Y = newY;
    }
}