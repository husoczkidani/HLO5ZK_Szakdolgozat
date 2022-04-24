using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinesweeperWithSolver.Data.Entities
{
    public class PlayedGame : BaseTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public DateTime Time { get; set; }
    }
}
