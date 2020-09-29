using System;
using System.Diagnostics;
using System.Drawing;


using My_Tetris.controllers;
using My_Tetris.constants;
using My_Tetris.menu;
using My_Tetris.tetris_enviroment;
using My_Tetris.DATABASE;
using System.Collections.Generic;

namespace My_Tetris
{
    class Program
    {

    
        static void Main()
        {
            //Console.CursorVisible = false;
            //Tetris_env.Game();

   

            var db_controller = new DatabaseController();

            var models = db_controller.get_all_the_figure_models();

            foreach (var model in models)
            {
                for (int i = 0; i < Math.Sqrt(model.Length); i++)
                {
                    for (int k = 0; k < Math.Sqrt(model.Length); k++)
                    {
                        Console.Write(model[i, k]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine();
            }


            Console.WriteLine("All passed well");

        }
    }
}
