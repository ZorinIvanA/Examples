using Microsoft.Win32;
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

namespace SquareEquationLayers
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SourceBorderDrop(object sender, DragEventArgs e)
        {
            var model = GetModelFromDataContext();
            model.SourceFilePath = GetFileNameFromDrop(e.Data);
        }

        private void ResultsBorderDrop(object sender, DragEventArgs e)
        {
            var model = GetModelFromDataContext();
            model.ResultsFilePath = GetFileNameFromDrop(e.Data);
        }

        protected SquareEquationViewModel GetModelFromDataContext()
        {
            var op = (ObjectDataProvider)(this.DataContext);
            var model = (SquareEquationViewModel)op.Data;

            return model;
        }

        protected String GetFileNameFromDrop(IDataObject dropData)
        {
            String result = String.Empty;

            if (dropData.GetDataPresent(DataFormats.FileDrop))
            {
                String[] files = (string[])dropData.GetData(DataFormats.FileDrop);
                result = files[0];
            }

            return result;
        }

        private void LoadSourceClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileOk += (s, result) =>
            {
                var model = GetModelFromDataContext();
                model.SourceFilePath = dialog.FileName;
            };
            dialog.ShowDialog();
        }

        private void LoadResultsDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileOk += (s, result) =>
            {
                var model = GetModelFromDataContext();
                model.ResultsFilePath = dialog.FileName;
            };
            dialog.ShowDialog();
        }
    }
}
