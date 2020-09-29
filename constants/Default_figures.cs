using System;
using System.Collections.Generic;
using System.Text;

namespace My_Tetris.constants
{
    public static class Default_figures
    {
        public static int[,] A = new int[4, 4] { {0, 0, 0, 0 },
                                                 {1, 1, 1, 1 },
                                                 {0, 0, 0, 0 },
                                                 {0, 0, 0, 0 }};

        public static int[,] B = new int[2, 2] { {1, 1 },
                                                 {1, 1 }};

        public static int[,] C = new int[3, 3] { {0, 1, 0 },
                                                 {1, 1, 1 },
                                                 {0, 0, 0 }};

        public static int[,] D = new int[3, 3] { {0, 1, 1 },
                                                 {1, 1, 0 },
                                                 {0, 0, 0 }};

        public static int[,] E = new int[3, 3] { {1, 1, 0 },
                                                 {0, 1, 1 },
                                                 {0, 0, 0 }};

        public static int[,] F = new int[3, 3] { {1, 0, 0 },
                                                 {1, 1, 1 },
                                                 {0, 0, 0 }};

        public static int[,] G = new int[3, 3] { {0, 0, 1 },
                                                 {1, 1, 1 },
                                                 {0, 0, 0 }};
    }
}
