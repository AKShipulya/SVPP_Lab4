using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
    internal class Car : INotifyPropertyChanged
    {
        int speed = 0;
        int position = 0;
        float x = 0;
        bool isFinish = false;

        public Car(int speed)
        {
            Speed = speed;
        }

        public int Speed
        {
            get => speed;
        
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            } 
        }
        public int Position
        { 
            get => position;
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public float X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public bool IsFinish
        { 
            get => isFinish;
            set
            {
                isFinish = value;
                OnPropertyChanged("IsFinish");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
