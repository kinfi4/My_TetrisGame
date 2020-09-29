using Microsoft.EntityFrameworkCore;

using My_Tetris.DATABASE.MODELS;

namespace My_Tetris.DATABASE
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

