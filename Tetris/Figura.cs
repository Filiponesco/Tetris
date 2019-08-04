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
        private string key;
        #region properties
        public List<Prostokat> Bloki { get; set; }
        public Direction Direction { get; set; }
        public int IleRazySpadal { get; set; }
        public int RoznicaPozycjaX { get; set; }
        public int KtoryObrot { get; set; }
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
        private readonly Hashtable Figures1Rotate = new Hashtable()
        {

            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {-1, 0}, {1, 0}, {2, 0} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {1, 1}, {1, 2} } },
            {"Z", new int[,]{ {0, 1}, {1, 0}, {0, 2}, {1, 1} } },
            {"L", new int[,]{ {0, 1}, {-1, 1}, {1, 0}, {1, 1} } },
            {"J", new int[,]{ {0, 0}, {-1, 0}, {1, 0}, {1, 1} } },
            {"T", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {-1, 1} } },
        };
        private readonly Hashtable Figures2Rotate = new Hashtable()
        {

            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {0, 1}, {0, 2}, {0, 3} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {-1, 1}, {1, 0} } },
            {"Z", new int[,]{ {0, 0}, {-1, 0}, {0, 1}, {1, 1} } },
            {"L", new int[,]{ {0, 0}, {1, 0}, {1, 1}, {1, 2} } },
            {"J", new int[,]{ {0, 0}, {1, 0}, {0, 1}, {0, 2} } },
            {"T", new int[,]{ {0, 0}, {-1, 0}, {1, 0}, {0, 1} } },
        };
        private readonly Hashtable Figures3Rotate = new Hashtable()
        {

            {"O", new int[,]{ {0, 0}, {0, 1}, {1, 0}, {1, 1} } },
            {"I", new int[,]{ {0, 0}, {-1, 0}, {1, 0}, {2, 0} } },
            {"S", new int[,]{ {0, 0}, {0, 1}, {1, 1}, {1, 2} } },
            {"Z", new int[,]{ {0, 1}, {1, 0}, {0, 2}, {1, 1} } },
            {"L", new int[,]{ {0, 0}, {-1, 0}, {1, 0}, {-1, 1} } },
            {"J", new int[,]{ {0, 1}, {-1, 1}, {-1, 0}, {1, 1} } },
            {"T", new int[,]{ {0, 0}, {1, 1}, {0, 1}, {0, 2} } },
        };
        private readonly string[] Keys = new string[] { "O", "I", "S", "Z", "L", "J", "T" };

        public Figura()
        {
            Bloki = new List<Prostokat>();
            IleRazySpadal = 0;
            RoznicaPozycjaX = Settings.Width / 2;
            KtoryObrot = 0;
            Random r = new Random();
            int index = r.Next(0, 7);
            //index = 1;
            key = Keys[index];
            int[,] temp = (int[,])Figures[key];
            for (int i = 0; i < 4; i++)
            {
                Prostokat p = new Prostokat { X = temp[i, 0] + Settings.Width / 2, Y = temp[i, 1] };
                Bloki.Add(p);
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
        public static List<Prostokat> GetAllBloki(List<Figura> Figury)
        {
            List<Prostokat> AllBloki = new List<Prostokat>();
            foreach(Figura f in Figury)
                foreach(Prostokat p in f.Bloki)
                    AllBloki.Add(p);
            return AllBloki;
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
        public static void DeleteFullRows(List<Figura> Figury, int w, int h)
        {
            for (int i = 0; i < h; i++)
            {
                int counter = 0;
                for (int j = 0; j < w; j++)
                {
                    foreach(Prostokat p in GetAllBloki(Figury)) //przyspieszenie pętli
                    {
                         if (p.X == j && p.Y == i)
                         counter++;
                    }
                }

                if (counter == w)
                {
                    List<int> indexFiguryToDelete = new List<int>();
                    List<Prostokat> toDelete = new List<Prostokat>();

                    for(int k = 0; k < GetAllBloki(Figury).Count; k++)
                    {
                        if (GetAllBloki(Figury)[k].Y == i)
                        {
                            toDelete.Add(GetAllBloki(Figury)[k]);
                        }
                    }
                    for (int j = toDelete.Count - 1; j >= 0; j--)
                        for (int m = 0; m < Figury.Count; m++)
                        {
                            for (int p = 0; p < Figury[m].Bloki.Count; p++)
                            {
                                if (toDelete[j].Equals(Figury[m].Bloki[p]))
                                    Figury[m].Bloki.Remove(toDelete[j]);
                            }
                        }
                    for (int j = 0; j < Figury.Count; j++)
                        for(int k = 0; k < Figury[j].Bloki.Count;k++)
                        if (Figury[j].Bloki[k].Y < i)
                            Figury[j].Bloki[k].Y += 1;
                }
            }
        }
        public void Turn(int obrot)
        {
            int pozycjaX;
            if(Bloki[0].X > Settings.Width / 2)
                pozycjaX = Bloki[0].X - 1;
            else
                pozycjaX = Bloki[0].X + 1;
            Bloki.Clear();
            int[,] temp = new int[4,2];
            switch(obrot%4)
            {
                case 1: temp = (int[,])Figures1Rotate[key];
                    break;
                case 2:
                    temp = (int[,])Figures2Rotate[key];
                    break;
                case 3:
                    temp = (int[,])Figures3Rotate[key];
                    break;
                case 0:
                    temp = (int[,])Figures[key];
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                Prostokat p = new Prostokat { X = temp[i, 0] + pozycjaX, Y = temp[i, 1] + IleRazySpadal };
                Bloki.Add(p);
            }

        }
    }
}
