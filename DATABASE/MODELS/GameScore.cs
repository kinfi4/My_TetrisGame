using System;

namespace My_Tetris.DATABASE.MODELS
{
    class GameScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime date_of_the_play { get; set; }
    }
}
