using System;
using System.Collections.Generic;
using System.Drawing;
using My_Tetris.constants;

namespace My_Tetris.controllers
{
    class Tetris
    {
        public int[,] world { get; private set; } 
        private int x_size, y_size;
        private List<int[,]> figures_templates = new List<int[,]>
        {
            Default_figures.A,
            Default_figures.B,
            //Default_figures.C,
            //Default_figures.D,
            //Default_figures.E,
            //Default_figures.F,
            //Default_figures.G
        };


        public Tetris()
        {
            x_size = (int)Constants.world_x_size;
            y_size = (int)Constants.world_y_size;
            world = new int[y_size, x_size];

            init_world();

        }


        public void drop_the_figures()
        {
            for (int y = 1; y < y_size - 2; y++)
            {
                if(check_if_the_row_made_of_same_blocks(y, Constants.CLEARED_BLOCKS))
                {
                    int next_row = y + 1;

                    if (next_row >= y_size - 2)
                        break;

                    while (check_if_the_row_made_of_same_blocks(next_row, Constants.CLEARED_BLOCKS))
                        next_row++;

                    for (int x = 1; x < x_size - 1; x++)
                    {
                        world[y, x] = world[next_row, x];
                    }

                    exchange_every_block_of_row(next_row, Constants.CLEARED_BLOCKS);
                }
            }

            for (int y = 0; y < y_size - 1; y++)
            {
                if (check_if_the_row_made_of_same_blocks(y, Constants.CLEARED_BLOCKS))
                    exchange_every_block_of_row(y, Constants.BLANK);
            }

        }

        private bool check_if_the_row_made_of_same_blocks(int row, Constants block_type)
        {
            for (int x = 1; x < x_size - 1; x++)
            {
                if (world[row, x] != (int)block_type)
                {
                    return false;
                }
            }

            return true;
        }

        public int search_for_finished_rows()
        {
            int number_of_rows = 0;

            for (int y = 1; y < y_size - 1; y++)
            {
                if (check_if_the_row_made_of_same_blocks(y, Constants.FIGURE_BLOCK_DROPED))
                {
                    exchange_every_block_of_row(y, Constants.CLEARED_BLOCKS);
                    number_of_rows++;
                }
            }

            return number_of_rows;
        }

        private void exchange_every_block_of_row(int y, Constants block)
        {
            for (int x = 1; x < x_size - 1; x++)
            {
                world[y, x] = (int)block;
            }
        }

        public bool step(Figure figure, int dx=0, int dy=-1)
        {
            if (something_above(figure))
            {
                Make_the_figure_droped(figure);
                return false;
            }

            Replace_world_coords_with_blocks(figure, Constants.BLOCK_IN_PAST);

            if (something_on_the_side(figure, dx))
                dx = 0;

            figure.change_coordinates(dx, dy);

            Replace_world_coords_with_blocks(figure, Constants.FIGURE_BLOCK);

            return true;

        }

        private bool something_on_the_side(Figure figure, int dx)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;

                if (ralative_y > y_size - 2)
                    return true;

                if (ralative_x > 0 && ralative_x < x_size - 1 &&
                   ralative_y < y_size - 1 && ralative_y > 0)
                {
                    var place_on_the_side = world[ralative_y, ralative_x + dx];

                    if (place_on_the_side == (int)Constants.WALL
                        || place_on_the_side == (int)Constants.FIGURE_BLOCK_DROPED)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        public void transpose(Figure figure, string direction="right")
        {
            if (Cant_transpose((Figure)figure.Clone(), direction))
                return;

            Replace_world_coords_with_blocks(figure, Constants.BLOCK_IN_PAST);

            if (direction == "right")
            {
                figure.transpone();
                figure.transpone();
                figure.transpone();
            }

            if (direction == "left")
            {
                figure.transpone();
            }

            Replace_world_coords_with_blocks(figure, Constants.FIGURE_BLOCK);

        }

        private bool Cant_transpose(Figure figure, string direction)
        {
            if (direction == "right")
            {
                figure.transpone();
                figure.transpone();
                figure.transpone();
            }
            if (direction == "left")
            {
                figure.transpone();
            }

            return something_on_the_side(figure, 0);                
        }

        private void Make_the_figure_droped(Figure figure)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;

                if (ralative_x > 0 && ralative_x < x_size - 1 &&
                  ralative_y < y_size - 1 && ralative_y > 0)
                {
                    world[ralative_y, ralative_x] = (int)Constants.FIGURE_BLOCK_DROPED;
                }
            }
        }

        private bool something_above(Figure figure)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;


                if (ralative_x > 0 && ralative_x < x_size - 1 &&
                   ralative_y < y_size - 1 && ralative_y > 0)
                {
                    if (world[ralative_y - 1, ralative_x] == (int)Constants.FIGURE_BLOCK_DROPED
                        || world[ralative_y - 1, ralative_x] == (int)Constants.WALL)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void init_world()
        {
            for(int y = 0; y < y_size; y++)
            {
                for (int x = 0; x < x_size; x++)
                {
                    if(x == 0 || x == x_size - 1 ||
                        y == 0 || y == y_size - 1)
                    {
                        world[y, x] = (int)Constants.WALL;
                    }
                    else
                    {
                        world[y, x] = (int)Constants.BLANK;
                    }
                }
            }
        }

        public Figure create_figure()
        {
            Random rn = new Random();
            int rn_figure_template = rn.Next(0, figures_templates.Count);
            Point start_point = new Point((int)Constants.frame_coord_x + 8, (int)Constants.frame_coord_y + y_size + 1);


            return new Figure(start_point, (int[,])figures_templates[rn_figure_template].Clone());
        }

        private void Replace_world_coords_with_blocks(Figure figure, Constants constants)
        {
            foreach (var absolute_positioin in figure.abdolute_coordinates)
            {
                int related_x = absolute_positioin.X - (int)Constants.frame_coord_x;
                int related_y = absolute_positioin.Y - (int)Constants.frame_coord_y;

                if (related_x > 0 && related_x < x_size - 1 &&
                    related_y < y_size - 1 && related_y > 0)
                {
                    world[related_y, related_x] = (int)constants;
                }
            }
        }
    }
}
