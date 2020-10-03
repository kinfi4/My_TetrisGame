using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace My_Tetris.VIEW
{
    public class Word_creator
    {
        public void print_word(int x_pos, int y_pos, string word, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;

            Console.SetCursorPosition(x_pos, y_pos);
            Console.Write(word);

            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
