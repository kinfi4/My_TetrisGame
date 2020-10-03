using My_Tetris.constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace My_Tetris.CONTROLLER.Game_controllers
{
    class Direction_controller
    {
        public bool something_on_the_side(int[,] world, Figure figure, int dx)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;

                if (ralative_y > (int)Constants.world_y_size - 2)
                    return true;

                if (ralative_x > 0 && ralative_x < (int)Constants.world_x_size - 1 &&
                   ralative_y < (int)Constants.world_y_size - 1 && ralative_y > 0)
                {
                    var place_on_the_side = world[ralative_y, ralative_x + dx];

                    if (place_on_the_side == (int)Constants.WALL
                        || place_on_the_side == (int)Constants.FIGURE_BLOCK_DROPED)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }

            return false;
        }
        public bool something_above(int[,] world, Figure figure)
        {
            foreach (var point in figure.abdolute_coordinates)
            {
                int ralative_x = point.X - (int)Constants.frame_coord_x;
                int ralative_y = point.Y - (int)Constants.frame_coord_y;


                if (ralative_x > 0 && ralative_x < (int)Constants.world_x_size - 1 &&
                   ralative_y < (int)Constants.world_y_size - 1 && ralative_y > 0)
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
        public bool Cant_transpose(int[,] world,  Figure figure, string direction)
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

            return something_on_the_side(world, figure, 0);
        }

    }

}
