using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        //private List<Prostokat> Blocks = new List<Prostokat>();
        private List<Figura> Blocks = new List<Figura>();
        private Figura f1;

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
            Blocks.Clear();
            GenerateBlock();


            lblScore.Text = Settings.Score.ToString();

        }

        private void GenerateBlock()
        {
            int maxXPos = pbEkran.Size.Width / Settings.Size - 1; //-1 bo anchor point to lewy gorny wierzchołek

            Random random = new Random();
            //Blocks.Add(new Prostokat { X = random.Next(0, maxXPos), Y = 0 });
            //Blocks.Add(new Figura(random.Next(0, maxXPos)));
            Figura f1 = new Figura(random.Next(0, maxXPos));
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
                if (Input.KeyPressed(Keys.Right))
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left))
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Down))
                    Settings.direction = Direction.Down;

                    MoveBlock();
            }
            pbEkran.Invalidate();

        }

        private void PbEkran_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    for (int j = 0; j < f1.Figury.Count; j++)
                    {
                        Brush blockColour;
                        blockColour = Brushes.Green;     //Draw block

                        //canvas.FillRectangle(blockColour,
                        //    new Rectangle(Blocks[i].X * Settings.Size,
                        //                  Blocks[i].Y * Settings.Size,
                        //                  Settings.Size, Settings.Size));
                        canvas.FillRectangle(blockColour,
                            new Rectangle(f1.Figury[i].X * Settings.Size,
                                          f1.Figury[i].Y * Settings.Size,
                                          Settings.Size, Settings.Size));

                        //canvas.FillRectangle(blockColour,
                        //        new Rectangle(Blocks[i].Figura.Figury[j] * Settings.Size,
                        //                      Figura.Figury[i].Y * Settings.Size,
                        //                      Settings.Size, Settings.Size));
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
            int index = Blocks.Count - 1;
            {

                switch (Settings.direction)
                {
                    case Direction.Right:
                        if(!Settings.CollisionRight) Blocks[index].X += 1;
                        Blocks[index].Y++;
                        break;
                    case Direction.Left:
                        if (!Settings.CollisionLeft) Blocks[index].X -= 1;
                        Blocks[index].Y++;
                        break;
                    case Direction.Down:
                        Blocks[index].Y+=1;
                        break;
                }

                //Get maximum X and Y Pos
                int maxXPos = pbEkran.Size.Width / Settings.Size - 1;
                int maxYPos = pbEkran.Size.Height / Settings.Size - 1;

                //Detect collission with floor  
                if (Blocks[index].Y >= maxYPos)
                {
                    GenerateBlock();

                }
                //Detect Collision with leftWall
                if (Blocks[index].X <= 0)
                {
                    Settings.CollisionLeft = true;
                }
                //Detect Collision with rightWall
                if (Blocks[index].X >= maxXPos)
                {
                    Settings.CollisionRight = true;
                }

                //Detect collission with other blocks
                for (int j = 0; j < Blocks.Count; j++)
                {
                    //if (Dist(Blocks[index].X, Blocks[index].Y, Blocks[j].X, Blocks[j].Y) <= 1 && index != j)
                    if (
                        Blocks[index].X == Blocks[j].X &&
                           Blocks[index].Y + 1 == Blocks[j].Y && index != j)
                    {
                        GenerateBlock();
                        if (Blocks[index].Y <= 1)
                            Settings.GameOver = true;
                        break;

                    }
                }
                // checks if any row is completed, if is, delete this line and moves rest one block down
                for (int i = 0; i < Settings.Height; i++)
                {
                    int counter = 0;
                    for (int j = 0; j < Settings.Width; j++)
                    {
                        foreach (var b in Blocks)
                            if (b.X == j && b.Y == i)
                                counter++;
                    }

                    if (counter == Settings.Width)
                    {
                        List<int> toDelete = new List<int>();

                        for (int j = 0; j < Blocks.Count; j++)
                            if (Blocks[j].Y == i)
                                toDelete.Add(j);

                        for (int j = toDelete.Count - 1; j >= 0; j--)
                            Blocks.RemoveAt(toDelete[j]);

                        for (int j = 0; j < Blocks.Count; j++)
                            if (Blocks[j].Y < i)
                                Blocks[j].Y += 1;
                        Settings.Score += 10;
                        lblScore.Text = Settings.Score.ToString();
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
            Settings.direction = Direction.Down;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
            Settings.direction = Direction.Down;
            Settings.CollisionRight = false;
            Settings.CollisionLeft = false;
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

        private void pbEkran_Click(object sender, EventArgs e)
        {

        }
    }
}
