using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_Tetris.CONTROLLER.Support_controllers
{
    class FigureHasher
    {
        public string Hash(int[,] array_presentation_of_figure)
        {
            StringBuilder result = new StringBuilder();

            for (int y = 0; y < Math.Sqrt(array_presentation_of_figure.Length); y++)
            {
                for (int x = 0; x < Math.Sqrt(array_presentation_of_figure.Length); x++)
                {
                    if (array_presentation_of_figure[y, x] == 0)
                        result.Append('_');

                    if (array_presentation_of_figure[y, x] == 1)
                        result.Append('@');

                }

                result.Append('/');
            }

            return result.ToString();
        }

        public int[,] DeHash(string str_presentation_of_figure)
        {

            if (str_presentation_of_figure[str_presentation_of_figure.Length - 1] == '/')
                str_presentation_of_figure = str_presentation_of_figure
                    .Substring(0, str_presentation_of_figure.Length - 1);


            var lines = str_presentation_of_figure.Split('/').ToList();

            while (lines.Count < lines[0].Length)
            {
                var __add_s = "";
                for (int i = 0; i < lines[0].Length; i++)
                    __add_s += "_";

                lines.Add(__add_s);                    
            }


            int[,] result = new int[lines.Count, lines.Count];

            for (int line_index = 0; line_index < lines.Count; line_index++)
            {
                for (int x = 0; x < lines.Count; x++)
                {
                    result[line_index, x] = lines[line_index][x] == '@' ? 1 : 0;
                }
            }

            return result;
        }

    }
}
