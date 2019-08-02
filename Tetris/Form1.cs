using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private List<Figura> Figury = new List<Figura>();
        private Thread th;

        public Form1()
        {
            InitializeComponent();

            //Domyślne ustawienia
            new Settings();
            //Set game speed and start timer
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start New game
            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;

            //Set settings to default
            new Settings();

            //Set Ekran
            int h = Settings.Height;
            int w = Settings.Width;
            int s = Settings.Size;
            pbEkran.Size = new System.Drawing.Size(w * s, h * s);

            //Create new block
            Figury.Clear();
            GenerateBlock();

            lblScore.Text = Settings.Score.ToString();

        }

        private void GenerateBlock()
        {
            int maxXPos = pbEkran.Size.Width / Settings.Size - 1; //-1 bo anchor point to lewy gorny wierzchołek

            Random random = new Random();
            Figury.Add(new Figura());      
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for GameOver
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            { 
                MoveBlock();
            }
            pbEkran.Invalidate();

        }

        private void PbEkran_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                for (int i = 0; i < Figury.Count; i++)
                {
                    for (int j = 0; j < Figury[i].Bloki.Count; j++)
                    {
                        Brush blockColour;
                        blockColour = Brushes.Green;
                        Rectangle rectangle = new Rectangle(Figury[i].Bloki[j].X * Settings.Size,
                                          Figury[i].Bloki[j].Y * Settings.Size, Settings.Size, Settings.Size);
                        Pen border = new Pen(Color.Black);
                        canvas.FillRectangle(blockColour, rectangle);
                        canvas.DrawRectangle(border, rectangle);
                    }
                }
            }
            else
            {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }


        private void MoveBlock()
        {
            int indexActive = Figury.Count - 1; //aktywny blok
                Figury[indexActive].Move(0, 1);
                int maxXPos = GetMaxXPos();
                int maxYPos = GetMaxYPos();

                ////Detect collission with floor  
            if (Figury[indexActive].CollisionWithFloor(maxYPos))
            {
                Figury[indexActive].Direction = Direction.Stop;
                GenerateBlock();
            }
            if (Figura.CollisionWithOtherFiguryVertically(Figury))
            {
                Figury[indexActive].Direction = Direction.Stop;
                GenerateBlock();
            }

             //Detect collision with other Figury to prevent movement
             if(Figury[indexActive].Direction == Direction.Stop)
                Figura.FullRows(Figury, Settings.Width, Settings.Height);
        }
        private void HorrizontalMove()
        {
            int indexActive = Figury.Count - 1; //aktywny blok
            switch (Figury[indexActive].Direction)
            {
                case Direction.Right:
                    if (!Figury[indexActive].CollisionWithRightWall(GetMaxXPos()) && !Figura.CollisionWithOtherFiguryHorrizontalLeft(Figury))
                        Figury[indexActive].Move(1, 0);
                    break;
                case Direction.Left:
                    if (!Figury[indexActive].CollisionWithLeftWall() && !Figura.CollisionWithOtherFiguryHorrizontalRight(Figury))
                        Figury[indexActive].Move(-1, 0);
                    break;
            }
            pbEkran.Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
            ZczytanieZnaku();
            HorrizontalMove();
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
        private void Die()
        {
            Settings.GameOver = true;
        }
        private double Dist(int x1,int y1, int x2, int y2)
        {
            double wynik =  Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return wynik;
        }
        private void ZczytanieZnaku()
        {
            int indexActive = Figury.Count() - 1;
            if (Input.KeyPressed(Keys.Right))
                Figury[indexActive].Direction = Direction.Right;
            else if (Input.KeyPressed(Keys.Left))
                Figury[indexActive].Direction = Direction.Left;
            else
                Figury[indexActive].Direction = Direction.Down;
        }
        private int GetMaxXPos()
        {
            return pbEkran.Size.Width / Settings.Size - 1;
        }
        private int GetMaxYPos()
        {
            return pbEkran.Size.Height / Settings.Size - 1;
        }
    }
}
