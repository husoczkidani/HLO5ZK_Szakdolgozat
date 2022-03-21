using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MinesweeperWithSolver.Data.Entities
{
    public class PlayedGame
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
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
