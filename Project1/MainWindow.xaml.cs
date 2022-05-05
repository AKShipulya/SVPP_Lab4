using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Project1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas[] canvases = new Canvas[3];
        Race.UserControlCar[] cars = new Race.UserControlCar[3];
        Race.UserControlFinish[] finishes= new Race.UserControlFinish[3];
        Race.UserControlPosition[] positions = new Race.UserControlPosition[3];
        DispatcherTimer timer;
        DispatcherTimer timerUpdateSpeed;
        Random random = new Random();
        bool flStart = false;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            for(int i = 0; i < canvases.Length; i++)
            {
                canvases[i] = new Canvas();
                Grid.SetRow(canvases[i], i);
                grid.Children.Add(canvases[i]);
            }
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(1000);
            
            timerUpdateSpeed = new DispatcherTimer();
            timerUpdateSpeed.Tick += new EventHandler(timerSpeedUpdateTick);
            timerUpdateSpeed.Interval = new TimeSpan(1000);
        }

        private void Start()
        {
            for(int i = 0; i < canvases.Length; i++)
            {
                canvases[i].Children.Clear();
                finishes[i] = new Race.UserControlFinish();
                canvases[i].Children.Add(finishes[i]);
                cars[i] = new Race.UserControlCar(random.Next(20, 100));
                canvases[i].Children.Add(cars[i]);
                Canvas.SetRight(finishes[i], 0);
                positions[i] = new Race.UserControlPosition();
                canvases[i].Children.Add(positions[i]);
                Canvas.SetLeft(positions[i], 50);
            }
            timer.Start();
            timerUpdateSpeed.Start();
        }

        private void timerSpeedUpdateTick(object sender, EventArgs e)
        {
            for(int i = 0; i < canvases.Length; i++)
            {
                if (!cars[i].isFinish)
                {
                    cars[i].UpdateSpeed(random.Next(30, 120));
                }
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            for(int i = 0; i < canvases.Length; i++)
            {
                if(cars[i].isFinish)
                {
                    continue;
                }
                int carPosition = 0;
                for(int j = 0; j < canvases.Length; j++)
                {
                    if(cars[j].isFinish || cars[i].XCar <= cars[j].XCar)
                    {
                        carPosition++;
                    }
                }
                if(cars[i].XCar < 1100)
                {
                    cars[i].UpdatePosition(carPosition);
                    cars[i].XCar += cars[i].GetSpeed() / 200f;
                }
                else
                {
                    cars[i].UpdatePosition(carPosition);
                    positions[i].SetPosition(carPosition);
                    cars[i].isFinish = true;
                }
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if(flStart == false)
            {
                flStart = true;
                Start();
            }
            if(timer.IsEnabled)
            {
                ((Button)sender).Content = "Start";
                timer.IsEnabled = false;
                timerUpdateSpeed.IsEnabled = false;
            }
            else
            {
                ((Button)sender).Content = "Pause";
                timer.IsEnabled = true;
                timerUpdateSpeed.IsEnabled = true;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
    }
}