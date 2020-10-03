using My_Tetris.CONTROLLER.DATABASE;
using My_Tetris.MODEL;
using My_Tetris.VIEW;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace My_Tetris.CONTROLLER.Record_window_controller
{
    class Record_window_controller
    {
        Frame_creator frame_Creator = new Frame_creator();
        Word_creator word_Creator = new Word_creator();
        DatabaseController database = new DatabaseController();

        List<string> Word_variants = new List<string> { "Order by date", "Order by score", "BACK" };
        List<Point> Word_positions = new List<Point>
        {
            new Point(25, 3),
            new Point(51, 3),
            new Point(72, 21),
        };
        List<GameScore> data;

        Sorting_type sorting_Type = Sorting_type.order_by_date_ascending;

        public void Build_window()
        {
            data = database.get_all_the_plays();
            Paint_the_window();
            word_Creator.print_word(Word_positions[0].X, Word_positions[0].Y, Word_variants[0], ConsoleColor.Red);


            print_data_in_a_column(data, sorting_Type);

            int current_chose = 0;
            int previous_position = 0;

            while (true)
            {
                var key = Console.ReadKey(true);
                bool change_option = false;

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

                    if (current_chose == 2)
                        return;

                    if (current_chose == 1)
                        sorting_Type = sorting_Type == Sorting_type.order_by_score_ascending ?
                            Sorting_type.order_by_score_descending :
                            Sorting_type.order_by_score_ascending;

                    if (current_chose == 0)
                        sorting_Type = sorting_Type == Sorting_type.order_by_date_ascending ?
                            Sorting_type.order_by_date_descending :
                            Sorting_type.order_by_date_ascending;


                    Paint_the_window();

                    word_Creator.print_word(Word_positions[current_chose].X,
                        Word_positions[current_chose].Y,
                        Word_variants[current_chose],
                        ConsoleColor.Red);

                    print_data_in_a_column(data, sorting_Type);

                }

                if (key.Key == ConsoleKey.UpArrow)
                {
                    change_option = true;
                    previous_position = current_chose;
                    current_chose = current_chose == 2 ? 1 : 2;
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    change_option = true;
                    previous_position = current_chose;
                    current_chose = current_chose == 1 || current_chose == 0 ? 2 : 1;
                }

                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                {
                    change_option = true;
                    previous_position = current_chose;
                    current_chose = current_chose == 1 ? 0 : 1;
                }


                if (change_option)
                {
                    word_Creator.print_word(Word_positions[current_chose].X,
                                            Word_positions[current_chose].Y,
                                            Word_variants[current_chose], ConsoleColor.Red);

                    word_Creator.print_word(Word_positions[previous_position].X,
                                            Word_positions[previous_position].Y,
                                            Word_variants[previous_position]);
                }


            }

        }

        private void Paint_the_window()
        {
            frame_Creator.Draw_the_frame(20, 2, 50, 20, "*");
            frame_Creator.Draw_the_frame(20, 2, 50, 3, "*");
            frame_Creator.Draw_the_frame(71, 20, 6, 3, "#");

            word_Creator.print_word(Word_positions[0].X, Word_positions[0].Y, Word_variants[0]);
            word_Creator.print_word(Word_positions[1].X, Word_positions[1].Y, Word_variants[1]);
            word_Creator.print_word(Word_positions[2].X, Word_positions[2].Y, Word_variants[2]);
        }

        private void print_data_in_a_column(List<GameScore> lists, Sorting_type sorting_Type)
        {
            switch (sorting_Type)
            {
                case Sorting_type.order_by_date_descending:
                    lists = lists.OrderByDescending(x => x.date_of_the_play).ToList();
                    break;

                case Sorting_type.order_by_date_ascending:
                    lists = lists.OrderBy(x => x.date_of_the_play).ToList();
                    break;

                case Sorting_type.order_by_score_descending:
                    lists = lists.OrderByDescending(x => x.Score).ToList();
                    break;

                case Sorting_type.order_by_score_ascending:
                    lists = lists.OrderBy(x => x.Score).ToList();
                    break;
            }
            
            int number_of_plays = lists.Count < 15 ? lists.Count : 15;
            for (int y = 0, index = 0; index < number_of_plays; y++, index++)
            {
                if(lists[index].Score == 0)
                {
                    number_of_plays = number_of_plays < lists.Count ? number_of_plays + 1 : number_of_plays;
                    y--;
                    continue;
                }
                Console.SetCursorPosition(24, 5 + y);
                var d_i = lists[index].date_of_the_play;

                Console.Write($"{d_i.Day}/{d_i.Month}/{d_i.Year}  {d_i.Hour}:{d_i.Minute}");

                Console.SetCursorPosition(55, 5 + y);
                Console.Write(lists[index].Score);


            }
        }
    }
}
