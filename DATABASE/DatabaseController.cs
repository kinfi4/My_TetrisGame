using System.Collections.Generic;
using System.Linq;
using My_Tetris.controllers;

namespace My_Tetris.DATABASE
{
    class DatabaseController
    {
        FigureHasher hasher;
        public DatabaseController()
        {
            hasher = new FigureHasher();
        }
    
        public void save_figure_to_database(string line)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.CustomFigures.Add(new MODELS.Figure { hashed_figure = line });

                db.SaveChanges();
            }
        }
        public void save_figure_to_database(int[,] array)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.CustomFigures.Add(new MODELS.Figure { hashed_figure = hasher.Hash(array) });

                db.SaveChanges();

            }
        }

        public List<int[,]> get_all_the_figure_models()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return (from figure in db.CustomFigures
                       select hasher.DeHash(figure.hashed_figure)).ToList();
            }
        }


    }
}
