using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace My_Tetris.menu
{

    class Menu
    {
        int X_window_size, Y_window_size;
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
                var key = Console.ReadKey();
                bool change_option = false;

                if (key.Key == ConsoleKey.Enter)
                    return current_chose;

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
                    print_word(Menu_variants_position[current_chose], Menu_variants[current_chose], true);
                    print_word(Menu_variants_position[previous_position], Menu_variants[previous_position]);
                }


            }

      

        }
        protected void Draw_The_Menu()
        {
            Draw_the_frame();


            print_word(Menu_variants_position[0], Menu_variants[0], true);
            print_word(Menu_variants_position[1], Menu_variants[1]);
            print_word(Menu_variants_position[2], Menu_variants[2]);
            print_word(Menu_variants_position[3], Menu_variants[3]);

        }
        private void Draw_the_frame()
        {
            for (int y = 0; y < Y_window_size; y++)
            {
                Console.SetCursorPosition(30, 4 + y);

                if (y == 0 || y == Y_window_size - 1)
                {
                    for (int x = 0; x < X_window_size; x++)
                    {
                        Console.Write('*');
                    }
                }
                else
                {
                    Console.Write('*');
                    for (int x = 2; x < X_window_size; x++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write('*');

                }

            }
        }
        protected void print_word(Point pos, string word, bool color_chosen=false)
        {
            if (color_chosen)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(word);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
