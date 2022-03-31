using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinesweeperWithSolver.Data.Entities
{
    public class Simulation : BaseTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Solver { get; set; }
        public string Difficulty { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesSolved { get; set; }
        public int GamesFailed { get; set; }
        public double MinesFlagged { get; set; }
        public double TilesRevealed { get; set; }
        public DateTime Time { get; set; }
    }
}
