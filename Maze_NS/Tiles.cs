namespace Maze_NS
{
    public class Tile
    {
        public bool IsWall { get; set; }
        public bool IsVisited { get; set; }
        public bool IsItem { get; set; }
        public bool IsMonster { get; set; }
        public bool IsExit { get; set; } //test
        public bool IsPlayer { get; set; }
        public Monster Monster { get; set; }

        public Tile(bool isWall = true)
        {
            IsWall = isWall;
            IsItem = false;
            IsVisited = false;
            IsExit = false;
            IsMonster = false;
            Monster = null;
        }
    }
}