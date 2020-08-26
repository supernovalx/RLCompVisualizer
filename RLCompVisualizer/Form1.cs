using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLCompVisualizer
{
    public partial class Form1 : Form
    {
        private PictureBox[,] tiles = new PictureBox[9, 21];
        private Label[,] labels = new Label[9, 21];

        private Image forest = Image.FromFile("./Assets/forest.png");
        private Image gold = Image.FromFile("./Assets/gold.png");
        private Image grass = Image.FromFile("./Assets/grass.png");
        private Image player = Image.FromFile("./Assets/player.png");
        private Image swarm = Image.FromFile("./Assets/swarm.png");
        private Image trap = Image.FromFile("./Assets/trap.png");
        private const int SIZE = 80;

        public Form1()
        {
            InitializeComponent();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            using (var fs = new FileStream("D:\\test.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int y = 0; y < 21; y++)
                    {
                        if (tiles[i, y] == null)
                        {
                            tiles[i, y] = new PictureBox();
                            tiles[i, y].Size = new Size(SIZE, SIZE);
                            tiles[i, y].Location = new Point(SIZE * y, SIZE * i);
                            tiles[i, y].SizeMode = PictureBoxSizeMode.StretchImage;
                            this.Controls.Add(tiles[i, y]);

                            labels[i, y] = new Label();
                            //labels[i, y].Size = new Size(SIZE, 10);
                            labels[i, y].Location = new Point(SIZE * y, SIZE * i);
                            labels[i, y].Text = "100";
                            this.Controls.Add(labels[i, y]);
                        }

                        int type = int.Parse(sr.ReadLine());
                        if (type == 0)
                            tiles[i, y].Image = grass;
                        else if (type == -1)
                            tiles[i, y].Image = forest;
                        else if (type == -2)
                            tiles[i, y].Image = trap;
                        else if (type == -3)
                            tiles[i, y].Image = swarm;
                        else if (type > 0)
                        {
                            tiles[i, y].Image = gold;
                            labels[i, y].Text = "100";
                            labels[i, y].BringToFront();
                        }

                    }
                }
            }

        }
    }
}
