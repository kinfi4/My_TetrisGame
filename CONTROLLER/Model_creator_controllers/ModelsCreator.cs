using System;
using System.Collections.Generic;
using System.Text;
using My_Tetris.CONTROLLER.DATABASE;
using My_Tetris.CONTROLLER.Support_controllers;
using My_Tetris.VIEW;

namespace My_Tetris.CONTROLLER.Model_creator_controllers
{

    enum consta
    {
        x_for_add = 10,
        y_for_add = 3, 
        x_size_add = 70,
        y_size_add = 3,
    }
    class ModelsCreator
    {
        DatabaseController database = new DatabaseController();
        Frame_creator frame_Creator = new Frame_creator();
        Word_creator word_Creator = new Word_creator();
        FigureHasher figureHasher = new FigureHasher(); 

        public void Draw_add_model_window()
        {
            Create_add_model_window();
            Create_explanation_window();

            Create_model();

        }

        private void Create_model()
        {
            Console.CursorVisible = true;
            StringBuilder blocks = new StringBuilder();

            Console.SetCursorPosition((int)consta.x_for_add + 2 + 25 + blocks.Length, (int)consta.y_for_add + 1);
            while (true)
            {
                if(Console.KeyAvailable)
                {
                    Console.SetCursorPosition((int)consta.x_for_add + 2 + 25 + blocks.Length, (int)consta.y_for_add + 1);

                    var k = Console.ReadKey(false);

                    if (k.Key == ConsoleKey.Enter)
                    {
                        if (!check_if_invalid(blocks))
                            continue;

                        save_model_to_database(blocks);
                        blocks.Clear();

                        Clear_model_box();
                    }

                    if (k.Key == ConsoleKey.Q)
                    {
                        break;
                    }

                    if(k.Key != ConsoleKey.Backspace)
                        blocks.Append(k.KeyChar);

                    if (k.Key == ConsoleKey.Backspace)
                    {
                        if (blocks.Length == 0)
                            continue;

                        blocks = blocks.Remove(blocks.Length - 1, 1);
                        Console.SetCursorPosition((int)consta.x_for_add + 2 + 25, (int)consta.y_for_add + 1);

                        for (int i = 0; i < blocks.Length + 1; i++)
                        {
                            Console.Write(' ');
                        }

                        Console.SetCursorPosition((int)consta.x_for_add + 2 + 25, (int)consta.y_for_add + 1);
                        Console.Write(blocks.ToString());

                    }

                    if (check_if_invalid(blocks))
                    {
                        paint_the_figure(blocks);
                    }







                }

            }

            Console.CursorVisible = false;

        }

        private void save_model_to_database(StringBuilder blocks)
        {
            database.save_figure_to_database(blocks.ToString());
        }

        private void paint_the_figure(StringBuilder blocks)
        {
            int pos_x = (int)consta.x_for_add + (int)consta.x_size_add + 19, pos_y = 5;

            var lines = blocks.ToString().Split('/');
            Clear_model_box();

            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y + i);
                foreach (var letter in lines[i])
                {
                    if(letter == '@')
                        Console.Write("@");
                    if(letter == '_')
                        Console.Write(' ');
                }

            }
        }

        private bool check_if_invalid(StringBuilder blocks)
        {
            Console.CursorVisible = false;
            string line = blocks.ToString();
            clear_error_message();

            if (line.Length == 0)
                return true;
            

            foreach (char letter in line)
            {
                if (letter != '@' && letter != '/' && letter != '_')
                {
                    invalid_window("line consist invalid symbol");
                    return false;
                }
            }

            if (line[line.Length - 1] == '/')
                line = line.Substring(0, line.Length - 1);

            var lines = line.Split('/');

            for (int i = 0; i < lines.Length - 1; i++)
                if (lines[i].Length != lines[i + 1].Length)
                {
                    invalid_window("Each line must be the same length");
                    return false;
                }

            if (lines[0].Length > 5)
            {
                invalid_window("The model is too big");
                return false;
            }

            Console.CursorVisible = true;

            return true;
        }
        private void invalid_window(string error)
        {
            int pos_x = (int)consta.x_for_add + (int)consta.x_size_add + 19, pos_y = 2;
            Clear_model_box();

            Console.SetCursorPosition(pos_x, pos_y + 4);
            Console.WriteLine("ERROR");

            Console.SetCursorPosition(pos_x + 3 - (error.Length / 2), pos_y + 10);
            Console.WriteLine(error);

        }
        private void clear_error_message()
        {
            int pos_x = (int)consta.x_for_add + (int)consta.x_size_add + 19, pos_y = 2;
            Console.SetCursorPosition(pos_x + 3 - 25, pos_y + 10);

            for (int i = 0; i < 50; i++)
            {
                Console.Write(' ');
            }

        }
        private void Create_add_model_window()
        {
            frame_Creator.Draw_the_frame((int)consta.x_for_add,
                                         (int)consta.y_for_add,
                                         (int)consta.x_size_add,
                                         (int)consta.y_size_add,
                                         "#", ConsoleColor.Blue);


            frame_Creator.Draw_the_frame((int)consta.x_for_add + (int)consta.x_size_add + 17,
                                         (int)consta.y_size_add, 9, 9, "#", ConsoleColor.Yellow);

            word_Creator.print_word((int)consta.x_for_add + 2,
                                    (int)consta.y_for_add + 1,
                                    "Enter the model string: ");

            word_Creator.print_word((int)consta.x_for_add + (int)consta.x_size_add + 10,
                                    (int)consta.y_for_add - 1,
                                    "Real look of the model");

        }
        private void Create_explanation_window()
        {
            int x_pos = 10, y_pos = 8, x_size = 50, y_size = 10;

            frame_Creator.Draw_the_frame(x_pos, y_pos, x_size, y_size, "#", ConsoleColor.DarkYellow);

            word_Creator.print_word(x_pos + 2, y_pos + 2, "- '_' represents: blanck space");
            word_Creator.print_word(x_pos + 2, y_pos + 3, "- '/' represents: new line to be started");
            word_Creator.print_word(x_pos + 2, y_pos + 4, "- '@' represetns: a model block");
            word_Creator.print_word(x_pos + 2, y_pos + 5, "- No other symbols are available");
            word_Creator.print_word(x_pos + 2, y_pos + 6, "- The model length must be <= 5");
            word_Creator.print_word(x_pos + 2, y_pos + 7, "- Press Q in order to get back to the menu");

        } 
        private void Clear_model_box()
        {
            var x_p = (int)consta.x_for_add + (int)consta.x_size_add + 18;
            var y_p = (int)consta.y_size_add;

            for (int i = 1; i < 7; i++)
            {
                Console.SetCursorPosition(x_p, y_p + i);

                for (int k = 1; k < 7; k++)
                {
                    Console.Write(' ');
                }
            }
        }
    
    
    }
}
