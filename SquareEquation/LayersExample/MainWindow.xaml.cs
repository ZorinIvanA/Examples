using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace LayersExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public LayersWindowViewModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Model = new LayersWindowViewModel();
        }
        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {
            Model.MoveToLeft(Model.SelectedInRight);
            Debug.WriteLine(Model.LeftList.Count);
            Debug.WriteLine(Model.RightList.Count);
        }

        private void RightButtonClick(object sender, RoutedEventArgs e)
        {
            Model.MoveToRight(Model.SelectedInLeft);
            Debug.WriteLine(Model.LeftList.Count);
            Debug.WriteLine(Model.RightList.Count);
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Model.NotifyPropertyChanged(e.PropertyName);
        }
    }
}
