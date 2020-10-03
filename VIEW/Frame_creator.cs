using System;
using System.Collections.Generic;
using System.Text;

namespace My_Tetris.VIEW
{
    class Frame_creator
    {
        public void Draw_the_frame(int x_pos, int y_pos, int x_size, int y_size, string block, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;

            for (int y = 0; y < y_size; y++)
            {
                Console.SetCursorPosition(x_pos, y_pos + y);

                if (y == 0 || y == y_size - 1)
                {
                    for (int x = 0; x < x_size; x++)
                    {
                        Console.Write(block);
                    }
                }
                else
                {
                    Console.Write(block);

                    for (int x = 2; x < x_size; x++)
                    {
                        Console.Write(' ');
                    }

                    Console.Write(block);

                }

            }

            Console.ForegroundColor = ConsoleColor.Gray;

        }




    }
}
