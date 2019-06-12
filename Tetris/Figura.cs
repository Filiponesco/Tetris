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
        public List<Vector> Items { get; set; }
        Hashtable Figures = new Hashtable()
        {
            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {0, 3}, {0, 4} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {-1, 1}, {1, 0} } },
            {"Z", new int[,]{ {0, 0}, {0, 1}, {-1, 0}, {1, 1} } },
            {"L", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {1, 2} } },
            {"J", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {-1, 2} } },
            {"T", new int[,]{ {0, 0}, {0, 1}, {1, 1}, {-1, 1} } },
        };
        string[] Keys = new string[] { "O", "I", "S", "Z", "L", "J", "T" };
        public int Size { get; private set; }

        public Figura(int middle)
        {
            Items = new List<Vector>();
            Random r = new Random();
            int index = r.Next(0, Keys.Length);
            string key = Keys[index];
            int[,] temp = (int[,])Figures[key];
            Size = temp.GetLength(0);

            for (int i = 0; i < Size; i++)
            {
                Items.Add(new Vector(temp[i, 0] + middle, temp[i, 1]));
            }
        }

        public void Clear()
        {
            Size = 0;
            Items.Clear();
        }

        public void Turn(int a)
        {
            int mx = 0;
            int my = 0;
            double angle = a * Math.PI / 2;
            List<Vector> temp = new List<Vector>();
            bool ok = true;

            foreach (Vector v in Items)
            {
                mx += (int)v.X;
                my += (int)v.Y;
            }

            mx = (int)Math.Round((double)mx / Size);
            my = (int)Math.Round((double)my / Size);

            for (int i = 0; i < Size; i++)
            {
                int x = (int)Items[i].X;
                int y = (int)Items[i].Y;

                Vector v = new Vector(mx + (x - mx) * Math.Cos(angle) - (y - my) * Math.Sin(angle),
                    my + (x - mx) * Math.Sin(angle) + (y - my) * Math.Cos(angle));

                temp.Add(v);
            }

            foreach (Vector v in temp)
            {
                if (v.X < 0 || v.X >= Settings.Width)
                    ok = false;

                if (v.Y < 0 || v.Y >= Settings.Height)
                    ok = false;
            }

            if (ok)
                for (int i = 0; i < Size; i++)
                    Items[i] = temp[i];
        }
    }
}
