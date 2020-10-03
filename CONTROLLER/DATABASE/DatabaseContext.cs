using Microsoft.EntityFrameworkCore;

using My_Tetris.MODEL;

namespace My_Tetris.CONTROLLER.DATABASE
{
    class DatabaseContext : DbContext
    {
        public DbSet<GameScore> Plays { get; set; }
        public DbSet<Figure> CustomFigures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-44JOMF3D\SQLEXPRESS; Database=TetrisDB; Trusted_Connection=True");
        }

    }
}

