using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Size { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }
        public static int CountBlocks { get; set; }

        public Settings()
        {
            Width = 10;
            Height = 20;
            Size = 20; //rozmiar jednego kwadratu
            Speed = 12;
            Score = 0;
            GameOver = false;

            // 20pix * 10pix  X  20pix * 20pix = 200 X 400
        }
    }
}
