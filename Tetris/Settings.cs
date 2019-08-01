using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum Direction
    {
        Left,
        Right,
        Down,
        Stop
    }

    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Size { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }
        public static int CountBlocks { get; set; }
        public static bool CollisionRight { get; set; }
        public static bool CollisionLeft { get; set; }

        public Settings()
        {
            Width = 10;
            Height = 20;
            Size = 20; //rozmiar jednego kwadratu
            Speed = 2;
            Score = 0;
            GameOver = false;
            direction = Direction.Down;
            CollisionRight = false;
            CollisionLeft = false;

            // 20pix * 10pix  X  20pix * 20pix = 200 X 400
        }
    }
}
