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

namespace DragAndDropExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Нажатие мышкой на эллипс - заставляет работать D&D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop(ellipse, ellipse, DragDropEffects.Move);
        }

        /// <summary>
        /// Протаскивание через холст. Нельзя на холст дропнуть.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
            dragOverCanvas.Text = String.Format("Позиция относительно холста {0}, {1}",
                e.GetPosition(can).X, e.GetPosition(can).Y);
        }

        /// <summary>
        /// Протаскивание через квадрат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_DragOver(object sender, DragEventArgs e)
        {
            dragOverRectangle.Text = String.Format("Позиция относительно прямоугольника {0}, {1}",
                e.GetPosition(rect).X, e.GetPosition(rect).Y);
        }

        /// <summary>
        /// Роняем на квадрат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_Drop(object sender, DragEventArgs e)
        {
            Color newColor = (Color)ColorConverter.ConvertFromString("Red");

            rect.Fill = new SolidColorBrush() { Color = newColor };
        }

        private void Ellipse_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
