using My_Tetris.constants;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace My_Tetris.CONTROLLER.Game_controllers
{
    class Figure_creator
    {
        List<int[,]> figures_templates;
        public Figure_creator(List<int[,]> figures_templates)
        {
            this.figures_templates = figures_templates;
        }
       
        public Figure create_figure()
        {
            Random rn = new Random();
            int rn_figure_template = rn.Next(0, figures_templates.Count);

            Point start_point = new Point((int)Constants.frame_coord_x + 1 + rn.Next(0, 15), 
                                          (int)Constants.frame_coord_y + (int)Constants.world_y_size + 1);


            Figure f = new Figure(start_point, (int[,])figures_templates[rn_figure_template].Clone());

            for (int i = 0; i < rn.Next(0, 4); i++)
            {
                f.transpone();
            }

            return f;
        }

    }
}
