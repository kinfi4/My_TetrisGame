using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace My_Tetris.CONTROLLER.Game_controllers
{
    class Figure : ICloneable
    {
        public int[,] Array_presentation { get; private set; }

        public Point left_top_corner;

        public List<Point> abdolute_coordinates;

        public Figure()
        {
            left_top_corner = new Point(0, 0);
        }
        public Figure(Point left_pos, int[,] type)
        {
            this.Array_presentation = type;
            this.left_top_corner = left_pos;

            abdolute_coordinates = new List<Point>();

            translate_relative_into_absolute();
        }

        private void translate_relative_into_absolute()
        {
            abdolute_coordinates = new List<Point>();

            for (int y = 0; y < Math.Sqrt(Array_presentation.Length); y++)
            {
                for (int x = 0; x < Math.Sqrt(Array_presentation.Length); x++)
                {
                    if(Array_presentation[y, x] == 1)
                    {
                        abdolute_coordinates.Add(new Point(
                            left_top_corner.X + x,
                            left_top_corner.Y + y));
                    }
                }
                
            }

        }
        public void change_coordinates(int dx, int dy)
        {
            left_top_corner.Offset(dx, dy);

            for (int point_index = 0; point_index < abdolute_coordinates.Count; point_index++)
            {
                // Тут я написал костылем ибо вообще не понимаю почему
                // не работет чтоб просто на каждой точке вызвать Offset
                
                Point copy_of_the_point = new Point(abdolute_coordinates[point_index].X + dx,
                    abdolute_coordinates[point_index].Y + dy);

                abdolute_coordinates[point_index] = copy_of_the_point;
            }
        }
        public void transpone()
        {
            var E = new int[Array_presentation.GetLength(1), Array_presentation.GetLength(0)];

            int newColumn, newRow = 0;
            for (int oldColumn = Array_presentation.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < Array_presentation.GetLength(0); oldRow++)
                {
                    E[newRow, newColumn] = Array_presentation[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }

            Array_presentation = E;
            translate_relative_into_absolute();

            
        }

        public object Clone()
        {
            return new Figure() 
            {
                left_top_corner = this.left_top_corner,
                abdolute_coordinates = this.abdolute_coordinates,
                Array_presentation = this.Array_presentation
            };
        }
    }
}
