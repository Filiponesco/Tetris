using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Prostokat
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Prostokat()
        {
            X = 0;
            Y = 0;
        }
        public Prostokat(double x, double y)
        {
            X = (int)x;
            Y = (int)y;
        }

    }
}
