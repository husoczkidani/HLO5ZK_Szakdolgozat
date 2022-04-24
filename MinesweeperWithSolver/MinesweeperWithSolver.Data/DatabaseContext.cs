using Microsoft.EntityFrameworkCore;
using MinesweeperWithSolver.Data.Entities;
using System;

namespace MinesweeperWithSolver.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PlayedGame> PlayedGame { get; set; }
        public DbSet<Simulation> Simulation { get; set; }
        public DatabaseContext()
        {
               
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=MinesweeperDB; Trusted_Connection=true;");
        }
    }
}
