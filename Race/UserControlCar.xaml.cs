using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Race
{
    /// <summary>
    /// Логика взаимодействия для UserControlCar.xaml
    /// </summary>
    public partial class UserControlCar : UserControl
    {
        Car car;
        public UserControlCar(int speed)
        {
            InitializeComponent();
            car = new Car(speed);

            textBlockSpeed.DataContext = car;
            Binding bindingSpeed = new Binding("Speed");
            bindingSpeed.Converter = new SpeedToString();
            textBlockSpeed.SetBinding(TextBlock.TextProperty, bindingSpeed);

            textBlockPosition.DataContext = car;
            Binding bindingPosition = new Binding("Position");
            bindingPosition.Converter = new PositionToString();
            textBlockPosition.SetBinding(TextBlock.TextProperty, bindingPosition);

            this.DataContext = car;
            this.SetBinding(Canvas.LeftProperty, new Binding("X"));
        }

        public void UpdateSpeed(int speed)
        {
            car.Speed = speed;
        }
        public int GetSpeed()
        {
            return car.Speed;
        }

        public void UpdatePosition(int position)
        {
            car.Position = position;
        }
        public float XCar
        {
            get 
            { 
                return car.X; 
            }
            set
            {
                car.X = value;
            }
        }
        public bool isFinish
        {
            get 
            {
                return car.IsFinish;
            }
            set
            {
                car.IsFinish = value;
            }
        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(textBlockSpeed.Visibility == Visibility.Visible)
            {
                textBlockSpeed.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlockSpeed.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (textBlockPosition.Visibility == Visibility.Visible)
            {
                textBlockPosition.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlockPosition.Visibility = Visibility.Visible;
            }
        }
    }

    public class PositionToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Position {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public class SpeedToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value} km/h";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
