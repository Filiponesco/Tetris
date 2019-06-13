using BasicVector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris
{
    class Figura
    {
        public List<Prostokat> Figury { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        //int[,] figures = new int[7, 4]
        //    {
        //        {1,3,5,7}, // I
        //        {2,4,5,7}, // Z
        //        {3,5,4,6}, // S
        //        {3,5,4,7}, // T
        //        {2,3,5,7}, // L
        //        {3,5,7,6}, // J
        //        {2,3,4,5}, // O
        //    };

        Hashtable Figures = new Hashtable()
        {
            //{"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            //{"I", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {0, 3} } },
            //{"S", new int[,]{ {1, 1}, {1, 2}, {0, 1}, {1, 2} } },
            //{"Z", new int[,]{ {0, 1}, {0, 2}, {1, 2}, {1, 3} } },
            //{"L", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {1, 2} } },
            //{"J", new int[,]{ {1, 1}, {1, 2}, {1, 3}, {0, 3} } },
            //{"T", new int[,]{ {1, 1}, {1, 2}, {0, 2}, {1, 3} } },

            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {0, 3} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {-1, 1}, {1, 0} } },
            {"Z", new int[,]{ {0, 0}, {0, 1}, {-1, 0}, {1, 1} } },
            {"L", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {1, 2} } },
            {"J", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {-1, 2} } },
            {"T", new int[,]{ {0, 0}, {0, 1}, {1, 1}, {-1, 1} } },
        };
        string[] Keys = new string[] { "O", "I", "S", "Z", "L", "J", "T" };

        public Figura(int x)
        {
            Figury = new List<Prostokat>();
            Random r = new Random();
            int index = r.Next(0, 7);
            string key = Keys[index];
            int[,] temp = (int[,])Figures[key];
            for (int i = 0; i < 4; i++)
            {
               Figury.Add(new Prostokat {X = temp[i, 0], Y = temp[i, 1] });
            }
        }

        //public void Clear()
        //{
        //    Size = 0;
        //    Items.Clear();
        //}

        //public void Turn(int a)
        //{
        //    int mx = 0;
        //    int my = 0;
        //    double angle = a * Math.PI / 2;
        //    List<Vector> temp = new List<Vector>();
        //    bool ok = true;

        //    foreach (Vector v in Items)
        //    {
        //        mx += (int)v.X;
        //        my += (int)v.Y;
        //    }

        //    mx = (int)Math.Round((double)mx / Size);
        //    my = (int)Math.Round((double)my / Size);

        //    for (int i = 0; i < Size; i++)
        //    {
        //        int x = (int)Items[i].X;
        //        int y = (int)Items[i].Y;

        //        Vector v = new Vector(mx + (x - mx) * Math.Cos(angle) - (y - my) * Math.Sin(angle),
        //            my + (x - mx) * Math.Sin(angle) + (y - my) * Math.Cos(angle));

        //        temp.Add(v);
        //    }

        //    foreach (Vector v in temp)
        //    {
        //        if (v.X < 0 || v.X >= Settings.Width)
        //            ok = false;

        //        if (v.Y < 0 || v.Y >= Settings.Height)
        //            ok = false;
        //    }

        //    if (ok)
        //        for (int i = 0; i < Size; i++)
        //            Items[i] = temp[i];
        //}
    }
}
