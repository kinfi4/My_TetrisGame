using System;
using System.Collections.Generic;
using System.Linq;

using My_Tetris.CONTROLLER.Support_controllers;
using My_Tetris.MODEL;

namespace My_Tetris.CONTROLLER.DATABASE
{
    enum Sorting_type
    {
        order_by_date_descending,
        order_by_date_ascending,
        order_by_score_descending,
        order_by_score_ascending,
    }
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
                db.CustomFigures.Add(new Figure { hashed_figure = line });

                db.SaveChanges();
            }
        }
        public void save_figure_to_database(int[,] array)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.CustomFigures.Add(new Figure { hashed_figure = hasher.Hash(array) });

                db.SaveChanges();

            }
        }
        public List<int[,]> get_all_the_figure_models()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return (from figure in db.CustomFigures
                            //where figure.Id == 1 || figure.Id == 0
                            //where figure.Id <= 7
                        //where figure.Id == 3
                        select hasher.DeHash(figure.hashed_figure)).ToList();
            }
        }

        public void save_current_game(int score)
        {
            if (score == 0)
                return;

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Plays.Add(new GameScore { Score = score, date_of_the_play = DateTime.Now });

                db.SaveChanges();
            }
        }


        public List<GameScore> get_all_the_plays()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Plays.ToList();
            }
        }

    }
}
