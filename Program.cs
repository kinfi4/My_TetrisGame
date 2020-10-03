using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;



using My_Tetris.constants;
using My_Tetris.CONTROLLER.Game_controllers;
using My_Tetris.VIEW.tetris_enviroment;
using My_Tetris.CONTROLLER.DATABASE;
using My_Tetris.MODEL;
using My_Tetris.CONTROLLER.Menu_controllers;
using My_Tetris.CONTROLLER.Record_window_controller;
using My_Tetris.CONTROLLER.Model_creator_controllers;
using My_Tetris.CONTROLLER.Support_controllers;

namespace My_Tetris
{
    class Program
    {
        static Menu menu = new Menu();
        static Record_window_controller record_Window = new Record_window_controller();

        static void Main()
        {
            Console.CursorVisible = false;

            //FigureHasher f = new FigureHasher();
            //var arr = f.DeHash("@@@_/@@@@");

            //for (int i = 0; i < Math.Sqrt(arr.Length); i++)
            //{
            //    for (int k = 0; k < Math.Sqrt(arr.Length); k++)
            //    {
            //        Console.Write(arr[i, k]);
            //    }
            //    Console.WriteLine();
            //}



            while (true)
            {
                Console.Clear();

                switch (menu.Create_Menu())
                {
                    case 0:
                        game_window();
                        break;

                    case 1:
                        add_model_window();
                        break;

                    case 2:
                        records_window();
                        break;

                    case 3:
                        return;

                }
            }



        }

        private static void records_window()
        {
            record_Window.Build_window();
        }

        private static void add_model_window()
        {
            ModelsCreator modelsCreator = new ModelsCreator();
            modelsCreator.Draw_add_model_window();
        }

        private static void game_window()
        {
            Tetris_env environment = new Tetris_env();
            environment.Game();
        }
    }
}
