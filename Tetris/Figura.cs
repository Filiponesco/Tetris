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
    public enum Direction
    {
        Left,
        Right,
        Down,
        Stop
    }
    class Figura
    {
        public static List<Prostokat> allBloki = new List<Prostokat>();
        #region properties
        public List<Prostokat> Bloki { get; set; }
        public Direction Direction { get; set; }
        public static List<Prostokat> AllBloki {
            get
            {
                return allBloki;
            }
            set
            {
                allBloki = value;
            }
        }
        #endregion
        private readonly Hashtable Figures = new Hashtable()
        {

            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {0, 3} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {-1, 1}, {1, 0} } },
            {"Z", new int[,]{ {0, 0}, {0, 1}, {-1, 0}, {1, 1} } },
            {"L", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {1, 2} } },
            {"J", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {-1, 2} } },
            {"T", new int[,]{ {0, 0}, {0, 1}, {1, 1}, {-1, 1} } },
        };
        private readonly string[] Keys = new string[] { "O", "I", "S", "Z", "L", "J", "T" };

        public Figura()
        {
            Bloki = new List<Prostokat>();
            Random r = new Random();
            int index = r.Next(0, 1);
            string key = Keys[index];
            int[,] temp = (int[,])Figures[key];
            for (int i = 0; i < 4; i++)
            {
                Prostokat p = new Prostokat { X = temp[i, 0] + Settings.Width / 2, Y = temp[i, 1] };
                Bloki.Add(p);
                AllBloki.Add(p);
            }
        }
        public void Move(int a, int b)
        {
            foreach (Prostokat p in Bloki)
            {
                p.X += a;
                p.Y += b;
            }

        }
        public bool CollisionWithFloor(int maxY)
        {
            foreach (Prostokat p in Bloki)
            {
                if (p.Y >= maxY)
                    return true;
            }
            return false;
        }
        public bool CollisionWithLeftWall()
        {
            foreach (Prostokat p in Bloki)
            {
                if (p.X <= 0)
                    return true;
            }
            return false;
        }
        public bool CollisionWithRightWall(int maxX)
        {
            foreach(Prostokat p in Bloki)
            {
                if (p.X >= maxX)
                    return true;
            }
            return false;
        }
        public static bool CollisionWithOtherFiguryVertically(List<Figura> Figury)
        {
            int indexActive = Figury.Count() - 1;
            for (int j = 0; j < Figury.Count; j++)
            {
                for(int i = 0; i < Figury[j].Bloki.Count(); i ++)
                {
                    for(int k = 0; k < Figury[indexActive].Bloki.Count(); k++)
                    {
                        if (Figury[indexActive].Bloki[k].X == Figury[j].Bloki[i].X &&
                            Figury[indexActive].Bloki[k].Y + 1 == Figury[j].Bloki[i].Y && indexActive != j)
                            return true;
                    }
                }
            }
            return false;
        }
        public static bool CollisionWithOtherFiguryHorrizontalLeft(List<Figura> Figury)
        {
            int indexActive = Figury.Count() - 1;
            for (int j = 0; j < Figury.Count; j++)
            {
                for (int i = 0; i < Figury[j].Bloki.Count(); i++)
                {
                    for (int k = 0; k < Figury[indexActive].Bloki.Count(); k++)
                    {
                        if (Figury[indexActive].Bloki[k].X + 1 == Figury[j].Bloki[i].X &&
                            Figury[indexActive].Bloki[k].Y == Figury[j].Bloki[i].Y && indexActive != j)
                            return true;
                    }
                }
            }
            return false;
        }
        public static bool CollisionWithOtherFiguryHorrizontalRight(List<Figura> Figury)
        {
            int indexActive = Figury.Count() - 1;
            for (int j = 0; j < Figury.Count; j++)
            {
                for (int i = 0; i < Figury[j].Bloki.Count(); i++)
                {
                    for (int k = 0; k < Figury[indexActive].Bloki.Count(); k++)
                    {
                        if (Figury[indexActive].Bloki[k].X -1 == Figury[j].Bloki[i].X &&
                            Figury[indexActive].Bloki[k].Y == Figury[j].Bloki[i].Y && indexActive != j)
                            return true;
                    }
                }
            }
            return false;
        }
        //dodac kolizje na ukos
        public static void FullRows(List<Figura> Figury, int w, int h) //uwaga trzeba pamietac ze klocek musi byc juz nieruchomy zeby zniknal wiersz!
        {
            for (int i = 0; i < h; i++)
            {
                int counter = 0;
                for (int j = 0; j < w; j++)
                {
                    foreach(Prostokat p in AllBloki) //przyspieszenie pętli
                    {
                         if (p.X == j && p.Y == i)
                         counter++;
                    }
                }

                if (counter == w)
                {
                    List<int> indexFiguryToDelete = new List<int>();
                    List<Prostokat> toDelete = new List<Prostokat>();

                    for(int k = 0; k < AllBloki.Count; k++)
                    {
                        if (AllBloki[k].Y == i)
                        {
                            toDelete.Add(AllBloki[k]);
                        }
                    }
                    for (int j = toDelete.Count - 1; j >= 0; j--)
                        for (int m = 0; m < Figury.Count; m++)
                        {
                            for (int p = 0; p < Figury[m].Bloki.Count; p++)
                                if (toDelete[j].Equals(Figury[m].Bloki[p]))
                                    Figury[m].Bloki.Remove(toDelete[j]);
                        }
                    for (int j = 0; j < Figury.Count; j++)
                        for(int k = 0; k < Figury[j].Bloki.Count;k++)
                        if (Figury[j].Bloki[k].Y < i)
                            Figury[j].Bloki[k].Y += 1;
                }
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
