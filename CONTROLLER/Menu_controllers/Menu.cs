using My_Tetris.VIEW;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace My_Tetris.CONTROLLER.Menu_controllers
{

    class Menu
    {
        int X_window_size, Y_window_size;
        Word_creator word_Creator = new Word_creator();
        Frame_creator frame_Creator = new Frame_creator();

        List<string> Menu_variants = new List<string> { "GAME", "ADD  MODEL", "RECORD", "QUIT" };
        List<Point> Menu_variants_position = new List<Point> 
        {
            new Point(38, 6), 
            new Point(35, 8),
            new Point(37, 10), 
            new Point(38, 14),
        };
        

        public Menu()
        {
            X_window_size = 20;
            Y_window_size = 13;
        }
        public int Create_Menu()
        {
            int current_chose = 0;
            int previous_position = 0;
            Draw_The_Menu();

            while(true)
            {
                var key = Console.ReadKey(true);
                bool change_option = false;

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return current_chose;
                }

                if (key.Key == ConsoleKey.UpArrow)
                {
                    change_option = true;
                    previous_position = current_chose;
                    current_chose = current_chose == 0 ? 3 : current_chose - 1;
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    change_option = true;
                    previous_position = current_chose;
                    current_chose = current_chose == 3 ? 0 : current_chose + 1;
                }

                if(change_option)
                {
                    word_Creator.print_word(Menu_variants_position[current_chose].X,
                                            Menu_variants_position[current_chose].Y,
                                            Menu_variants[current_chose], ConsoleColor.Red);

                    word_Creator.print_word(Menu_variants_position[previous_position].X,
                                            Menu_variants_position[previous_position].Y,
                                            Menu_variants[previous_position]);
                }


            }     

        }
        protected void Draw_The_Menu()
        {
            frame_Creator.Draw_the_frame(30, 4, X_window_size, Y_window_size, "*");


            word_Creator.print_word(Menu_variants_position[0].X, Menu_variants_position[0].Y, Menu_variants[0], ConsoleColor.Red);
            word_Creator.print_word(Menu_variants_position[1].X, Menu_variants_position[1].Y, Menu_variants[1]);
            word_Creator.print_word(Menu_variants_position[2].X, Menu_variants_position[2].Y, Menu_variants[2]);
            word_Creator.print_word(Menu_variants_position[3].X, Menu_variants_position[3].Y, Menu_variants[3]);

        }
    }
}
