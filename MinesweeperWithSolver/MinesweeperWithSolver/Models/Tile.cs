using MinesweeperWithSolver.Enums;

namespace MinesweeperWithSolver.Models
{
    public class Tile
    {
        public int tileID { get; set; }
        public int X_pos { get; set; }
        public int Y_pos { get; set; }
        public int AdjacentMines { get; set; }
        public string Image { get; set; }
        public TileState State { get; set; }
        public bool IsFlagged { get; set; }

        public Tile(int id, int x, int y)
        {
            tileID = id;
            X_pos = x;
            Y_pos = y;
            State = TileState.Blank;
            Image = "/Resources/Images/covered.png";
            IsFlagged = false;
        }
    }
}
