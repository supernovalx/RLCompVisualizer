using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RLCompVisualizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();

        private Image[,] tiles = new Image[9, 21];
        private Label[,] labels = new Label[9, 21];
        private Label[,] players = new Label[9, 21];

        private static string path = @"C:\Users\hoang\source\repos\RLCompVisualizer\RLCompVisualizer\Assets";
        private BitmapImage forest = new BitmapImage(new Uri($"{path}/forest.png"));
        private BitmapImage gold = new BitmapImage(new Uri($"{path}/gold.png"));
        private BitmapImage grass = new BitmapImage(new Uri($"{path}/grass.png"));
        private BitmapImage player = new BitmapImage(new Uri($"{path}/player.png"));
        private BitmapImage swarm = new BitmapImage(new Uri($"{path}/swarm.png"));
        private BitmapImage trap = new BitmapImage(new Uri($"{path}/trap.png"));

        private const int OFFSET_TOP = 200;
        private const int OFFSET_LEFT = 20;


        private const int SIZE = 80;
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            this.WindowState = WindowState.Maximized;

            // init controls
            for (int i = 0; i < 9; i++)
            {
                for (int y = 0; y < 21; y++)
                {
                    tiles[i, y] = new Image();
                    tiles[i, y].Width = SIZE;
                    tiles[i, y].Height = SIZE;
                    Canvas.SetTop(tiles[i, y], OFFSET_TOP + i * SIZE);
                    Canvas.SetLeft(tiles[i, y], OFFSET_LEFT + y * SIZE);


                    this.grid.Children.Add(tiles[i, y]);

                    labels[i, y] = new Label();
                    Canvas.SetTop(labels[i, y], OFFSET_TOP + i * SIZE);
                    Canvas.SetLeft(labels[i, y], OFFSET_LEFT + y * SIZE);
                    labels[i, y].Content = "";
                    labels[i, y].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    labels[i, y].FontWeight = FontWeight.FromOpenTypeWeight(700);
                    labels[i, y].FontSize = 22;
                    this.grid.Children.Add(labels[i, y]);

                    players[i, y] = new Label();
                    Canvas.SetTop(players[i, y], OFFSET_TOP + i * SIZE + SIZE - 40);
                    Canvas.SetLeft(players[i, y], OFFSET_LEFT + y * SIZE);
                    players[i, y].Content = "";
                    players[i, y].Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    players[i, y].FontWeight = FontWeight.FromOpenTypeWeight(500);
                    players[i, y].FontSize = 22;
                    this.grid.Children.Add(players[i, y]);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            using (var fs = new FileStream("C:\\test.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                // Obj map
                for (int i = 0; i < 9; i++)
                {
                    for (int y = 0; y < 21; y++)
                    {
                        players[i, y].Content = "";
                        labels[i, y].Content = "";
                        int type = int.Parse(sr.ReadLine());
                        string value = sr.ReadLine();
                        if (type == 0)
                        {
                            tiles[i, y].Source = grass;
                        }
                        else if (type == -1)
                        {
                            tiles[i, y].Source = forest;
                        }
                        else if (type == -2)
                        {
                            tiles[i, y].Source = trap;
                        }
                        else if (type == -3)
                        {
                            tiles[i, y].Source = swarm;
                            labels[i, y].Content = value;
                        }
                        else if (type > 0)
                        {
                            tiles[i, y].Source = gold;
                            labels[i, y].Content = type.ToString();
                            //labels[i, y].();
                        }
                    }
                }

                // Player 1
                int x = int.Parse(sr.ReadLine());
                int yy = int.Parse(sr.ReadLine());
                players[yy, x].Content += "☺";
                lbP1E.Content = sr.ReadLine();
                lbP1S.Content = sr.ReadLine();
                lbP1A.Content = sr.ReadLine();

                // Player 2
                x = int.Parse(sr.ReadLine());
                yy = int.Parse(sr.ReadLine());
                players[yy, x].Content += "♣";
                lbP2E.Content = sr.ReadLine();
                lbP2S.Content = sr.ReadLine();
                lbP2A.Content = sr.ReadLine();

                // Player 3
                x = int.Parse(sr.ReadLine());
                yy = int.Parse(sr.ReadLine());
                players[yy, x].Content += "♥";
                lbP3E.Content = sr.ReadLine();
                lbP3S.Content = sr.ReadLine();
                lbP3A.Content = sr.ReadLine();

                // Player 4
                x = int.Parse(sr.ReadLine());
                yy = int.Parse(sr.ReadLine());
                players[yy, x].Content += "♠";
                lbP4E.Content = sr.ReadLine();
                lbP4S.Content = sr.ReadLine();
                lbP4A.Content = sr.ReadLine();
            }
        }



    }
}
