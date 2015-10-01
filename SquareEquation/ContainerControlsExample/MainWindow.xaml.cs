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

namespace ContainerControlsExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TimeSpan TimeClicked { get; set; }
        public DispatcherTimer Tmr { get; set; }
        protected SecondTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            Tmr = new DispatcherTimer();
            _timer = new SecondTimer();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            TimeClicked = TimeSpan.Parse("00:00:00");
            _timer.BeginValue = DateTime.Now;

            //Так делать НЕЛЬЗЯ!
            timer.Text = TimeClicked.ToString();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            Tmr.Tick += (s, a) =>
            {
                TimeClicked = _timer.GetNextValue();

                //Так делать нельзя!
                timer.Text = TimeClicked.ToString();
            };
            Tmr.Start();
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            Tmr.Stop();
        }




    }
}
