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

        private static string path = @"C:\Users\Admin\source\repos\RLCompVisualizer\RLCompVisualizer\Assets";
        private BitmapImage forest = new BitmapImage(new Uri($"{path}/forest.png"));
        private BitmapImage gold = new BitmapImage(new Uri($"{path}/gold.png"));
        private BitmapImage grass = new BitmapImage(new Uri($"{path}/grass.png"));
        private BitmapImage player = new BitmapImage(new Uri($"{path}/player.png"));
        private BitmapImage swarm = new BitmapImage(new Uri($"{path}/swarm.png"));
        private BitmapImage trap = new BitmapImage(new Uri($"{path}/trap.png"));

        private const int SIZE = 80;
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
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
                            tiles[i, y] = new Image();
                            tiles[i, y].Width = SIZE;
                            tiles[i, y].Height = SIZE;
                            Canvas.SetTop(tiles[i, y] , i * SIZE);
                            Canvas.SetLeft(tiles[i, y] , y * SIZE);

                            
                            this.grid.Children.Add(tiles[i, y]);

                            labels[i, y] = new Label();
                            Canvas.SetTop(labels[i, y], i * SIZE);
                            Canvas.SetLeft(labels[i, y], y * SIZE);
                            labels[i, y].Content = "";
                            labels[i, y].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            labels[i, y].FontWeight = FontWeight.FromOpenTypeWeight(700);
                            labels[i, y].FontSize = 22;
                            this.grid.Children.Add(labels[i, y]);
                        }

                        int type = int.Parse(sr.ReadLine());
                        if (type == 0)
                            tiles[i, y].Source = grass;
                        else if (type == -1)
                            tiles[i, y].Source = forest;
                        else if (type == -2)
                            tiles[i, y].Source = trap;
                        else if (type == -3)
                            tiles[i, y].Source = swarm;
                        else if (type > 0)
                        {
                            tiles[i, y].Source = gold;
                            labels[i, y].Content = "100";
                            //labels[i, y].();
                        }

                    }
                }

            }
        }

        

    }
}
