namespace Maze_NS
{
    public class Tile
    {
        public bool IsWall { get; set; }
        public bool IsVisited { get; set; }
        public bool IsItem { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsMonster { get; set; }
        public bool IsExit { get; set; }

        public Tile(bool isWall = true)
        {
            IsWall = isWall;
            IsItem = false;
            IsEmpty = false;
            IsVisited = false;
            IsExit = false;
            IsMonster = false;
        }
    }
}