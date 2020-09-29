using My_Tetris.constants;
using My_Tetris.controllers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace My_Tetris.tetris_enviroment
{
    public static class Tetris_env
    {
        static bool done, new_figure_needed;
        static Tetris controller;
        static string block = "@"; //"■";
        static int speed;
        static Stopwatch timer;


        static Tetris_env()
        {
            done = false;
            new_figure_needed = true;
            speed = 250;
            timer = new Stopwatch();
            controller = new Tetris();
        }

        public static void Game()
        {

            // printing the world map
            Paint_rect_frame(new Point((int)Constants.frame_coord_x, (int)Constants.frame_coord_y), 
                                (int)Math.Sqrt(controller.world.Length),
                                (int)Math.Sqrt(controller.world.Length));

            // the frame for "the next figure"
            Paint_rect_frame(
                new Point(
                    (int)Constants.frame_coord_x + (int)Constants.world_x_size + 10,
                    (int)Constants.frame_coord_y),
                                                8,
                                                8);


            int drop = 0;
            int score = 0;

            new_figure_needed = false;
            var current_figure = controller.create_figure();
            var next_figure = controller.create_figure();

            Print_the_score(score);
            paint_the_next_figure(next_figure, block);

            timer.Start();
            while (!done)
            {
                if (new_figure_needed)
                {
                    paint_the_next_figure(next_figure, " ");

                    new_figure_needed = false;
                    current_figure = next_figure;
                    next_figure = controller.create_figure();

                    paint_the_next_figure(next_figure, block);
                }


                // Make a move
                if ((int)timer.ElapsedMilliseconds > speed - drop)
                {
                    timer.Restart();
                    new_figure_needed = !controller.step(current_figure);
                }


                drop = Translate_user_input_into_action(current_figure);

                int cleared_on_this_step = controller.search_for_finished_rows();


                Paint_updated_world();

                if (new_figure_needed)
                    update_droped_blocks();

                if (cleared_on_this_step > 0)
                {
                    controller.drop_the_figures();
                    update_droped_blocks();

                    score += cleared_on_this_step * 8 + (cleared_on_this_step - 1) * 8;
                    Print_the_score(score);
                }

            }
        }



        private static void paint_the_next_figure(Figure next_figure, string figure_block)
        {
            for (int y = 0; y < Math.Sqrt(next_figure.Array_presentation.Length); y++)
            {
                Console.SetCursorPosition((int)Constants.frame_coord_x + (int)Constants.world_x_size + 10 + 2,
                                      (int)Constants.frame_coord_y + 2 + y);

                for (int x = 0; x < Math.Sqrt(next_figure.Array_presentation.Length); x++)
                {
                    if(next_figure.Array_presentation[y, x] == 1)
                        Console.Write(figure_block);
                    else
                        Console.Write(' ');
                       
                }
            }

            Console.SetCursorPosition((int)Constants.frame_coord_x + (int)Constants.world_x_size + 10 + 2,
                                      (int)Constants.frame_coord_y + 2);


        }

        private static void Print_the_score(int score)
        {
            Console.SetCursorPosition(
                (int)Constants.frame_coord_x + (int)Constants.world_x_size + 10,
                (int)Constants.frame_coord_y + 9);


            Console.Write($"Score: {score}");
        }

        private static void update_droped_blocks()
        {
            for (int y = 1; y < Math.Sqrt(controller.world.Length) - 1; y++)
            {
                for (int x = 1; x < Math.Sqrt(controller.world.Length) - 1; x++)
                {
                    if (controller.world[y, x] == (int)Constants.FIGURE_BLOCK_DROPED)                       
                    {
                        Point absolutecoords = translate_coordinates(new Point(x, y));
                        Console.SetCursorPosition(absolutecoords.X, absolutecoords.Y);
                        Console.Write(block);
                    }
                }
            }
        }

        private static int Translate_user_input_into_action(Figure current_figure)
        {
            int drop = 0;
            var player_choice = Check_the_direction();

            if (player_choice[0] == -1)
            {
                drop = speed - speed / 4;

            }
            else if (player_choice[0] == 0)
            {
                controller.step(current_figure,
                    player_choice[1], player_choice[2]);

            }
            else if (player_choice[0] == 1)
            {
                if (player_choice[2] == 1)
                    controller.transpose(current_figure, "right");
                
                if (player_choice[2] == -1)
                    controller.transpose(current_figure, "left");
            }

            return drop;
        }

        private static int[] Check_the_direction()
        {

            if (Console.KeyAvailable)
            {
                var k = Console.ReadKey(true);

                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:
                        return new int[] { -1, 0, -1 };


                    case ConsoleKey.RightArrow:
                        return new int[] { 0, 1, 0 };


                    case ConsoleKey.LeftArrow:
                        return new int[] { 0, -1, 0 };


                    case ConsoleKey.A:
                        return new int[] { 1, 0, -1 };


                    case ConsoleKey.D:
                        return new int[] { 1, 0, 1 };

                    default:
                        return new int[] { 0, 0, -1 };

                }
            }

            return new int[] { 2, 0, -1 };
        }

        private static Point translate_coordinates(Point relative)
        {
            return new Point(relative.X + (int)Constants.frame_coord_x,
                            relative.Y + (int)Constants.frame_coord_y);
        }
        
        private static void Paint_updated_world()
        {
            for (int y = 0; y < Math.Sqrt(controller.world.Length); y++)
            {
                for (int x = 0; x < Math.Sqrt(controller.world.Length); x++)
                {
                    if(controller.world[y, x] == (int)Constants.BLOCK_IN_PAST ||
                       controller.world[y, x] == (int)Constants.CLEARED_BLOCKS)
                    {
                        Point absolutecoords = translate_coordinates(new Point(x, y));
                        Console.SetCursorPosition(absolutecoords.X, absolutecoords.Y);
                        Console.Write(" ");
                    }

                    if (controller.world[y, x] == (int)Constants.FIGURE_BLOCK)
                    {
                        Point absolutecoords = translate_coordinates(new Point(x, y));
                        Console.SetCursorPosition(absolutecoords.X, absolutecoords.Y);
                        Console.Write(block);
                    }

                }
            }
        }

        private static void Paint_rect_frame(Point point, int y_s, int x_s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(point.X, point.Y);
            for (int x = 0; x < x_s; x++)
            {
                Console.Write('#');
            }

            for (int y = 1; y < y_s - 1; y++)
            {
                Console.SetCursorPosition(point.X, point.Y + y);
                Console.Write('#');

                Console.SetCursorPosition(point.X + x_s - 1, point.Y + y);
                Console.Write('#');
            }

            Console.SetCursorPosition(point.X, point.Y + y_s - 1);     
            for (int x = 0; x < x_s; x++)
            {
                Console.Write('#');
            }

            Console.ForegroundColor = ConsoleColor.Gray;

        }



    }
}
