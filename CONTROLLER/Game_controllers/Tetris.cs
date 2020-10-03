using System;
using System.Collections.Generic;
using System.Drawing;

using My_Tetris.constants;
using My_Tetris.CONTROLLER.DATABASE;
using My_Tetris.MODEL;

namespace My_Tetris.CONTROLLER.Game_controllers
{
    class Tetris
    {
        public int[,] world { get; set; }

        Direction_controller direction_Controller;
        Figure_creator figure_Creator;
        DatabaseController databaseController;
        

        
        public Tetris()
        {
            world = new int[(int)Constants.world_y_size, (int)Constants.world_x_size];

            direction_Controller = new Direction_controller();
            databaseController = new DatabaseController();
            figure_Creator = new Figure_creator(databaseController.get_all_the_figure_models());

            init_world();
        }


        public bool Make_Move(Figure figure, int dx = 0, int dy = -1)
        {
            if (direction_Controller.something_above(world, figure))
            {
                Make_the_figure_droped(figure);
                return false;
            }

            Replace_world_coords_with_figure_blocks(figure, Constants.BLOCK_IN_PAST);

            if (direction_Controller.something_on_the_side(world, figure, dx))
                dx = 0;

            figure.change_coordinates(dx, dy);

            Replace_world_coords_with_figure_blocks(figure, Constants.FIGURE_BLOCK);

            return true;
        }

        internal Figure create_figure()
        {
            return figure_Creator.create_figure();
        }
        public void Replace_world_coords_with_figure_blocks(Figure figure, Constants constants)
        {
            foreach (var absolute_positioin in figure.abdolute_coordinates)
            {
                int related_x = absolute_positioin.X - (int)Constants.frame_coord_x;
                int related_y = absolute_positioin.Y - (int)Constants.frame_coord_y;

                if (related_x > 0 && related_x < (int)Constants.world_x_size - 1 &&
                    related_y < (int)Constants.world_y_size - 1 && related_y > 0)
                {
                    world[related_y, related_x] = (int)constants;
                }
            }

        }

        public bool the_game_has_ended(int score)
        {
            for (int x = 0; x < (int)Constants.world_x_size; x++)
            {
                if (world[(int)Constants.world_y_size - 2, x] == (int)Constants.FIGURE_BLOCK_DROPED)
                {
                    databaseController.save_current_game(score);
                    return true;
                }    
            }

            return false;
        }

        public void drop_the_figures()
        {
            for (int y = 1; y < (int)Constants.world_y_size - 2; y++)
            {
                if (check_if_the_row_made_of_same_blocks(y, Constants.CLEARED_BLOCKS))
                {
                    int next_row = y + 1;

                    while (check_if_the_row_made_of_same_blocks(next_row, Constants.CLEARED_BLOCKS))
                        next_row++;

                    if (next_row >= (int)Constants.world_y_size - 2)
                        break;

                    for (int x = 1; x < (int)Constants.world_x_size - 1; x++)
                    {
                        world[y, x] = world[next_row, x];
                    }

                    exchange_every_block_of_row(next_row, Constants.CLEARED_BLOCKS);
                }
            }

            for (int y = 1; y < (int)Constants.world_y_size - 1; y++)
            {
                if (check_if_the_row_made_of_same_blocks(y, Constants.CLEARED_BLOCKS))
                    exchange_every_block_of_row(y, Constants.BLANK);
            }

        }
        public void transpose(Figure figure, string direction="right")
        {
            if (direction_Controller.Cant_transpose(world, (Figure)figure.Clone(), direction))
                return;

            Replace_world_coords_with_figure_blocks(figure, Constants.BLOCK_IN_PAST);


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


            Replace_world_coords_with_figure_blocks(figure, Constants.FIGURE_BLOCK);

        }
        private void Make_the_figure_droped(Figure figure)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;

                if (ralative_x > 0 && ralative_x < (int)Constants.world_x_size - 1 &&
                  ralative_y < (int)Constants.world_y_size - 1 && ralative_y > 0)
                {
                    world[ralative_y, ralative_x] = (int)Constants.FIGURE_BLOCK_DROPED;
                }
            }
        }
        private void init_world()
        {
            for(int y = 0; y < (int)Constants.world_y_size; y++)
            {
                for (int x = 0; x < (int)Constants.world_x_size; x++)
                {
                    if(x == 0 || x == (int)Constants.world_x_size - 1 ||
                        y == 0 || y == (int)Constants.world_y_size - 1)
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
        
        

        private void exchange_every_block_of_row(int y, Constants block)
        {
            for (int x = 1; x < (int)Constants.world_x_size - 1; x++)
            {
                world[y, x] = (int)block;
            }
        }
        public bool check_if_the_row_made_of_same_blocks(int row, Constants block_type)
        {
            for (int x = 1; x < (int)Constants.world_x_size - 1; x++)
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

            for (int y = 1; y < (int)Constants.world_y_size - 1; y++)
            {
                if (check_if_the_row_made_of_same_blocks(y, Constants.FIGURE_BLOCK_DROPED))
                {
                    exchange_every_block_of_row(y, Constants.CLEARED_BLOCKS);
                    number_of_rows++;
                }
            }

            return number_of_rows;
        }


    }
}

