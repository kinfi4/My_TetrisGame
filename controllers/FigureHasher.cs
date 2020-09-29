using System;
using System.Text;

namespace My_Tetris.controllers
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
            var lines = str_presentation_of_figure.Split('/');
            int[,] result = new int[lines.Length - 1, lines.Length - 1];

            for (int line_index = 0; line_index < lines.Length - 1; line_index++)
            {
                for (int x = 0; x < lines.Length - 1; x++)
                {
                    result[line_index, x] = lines[line_index][x] == '@' ? 1 : 0;
                }
            }

            return result;
        }

    }
}
