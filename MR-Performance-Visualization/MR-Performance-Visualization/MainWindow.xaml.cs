using System;
using System.Collections.Generic;
using System.IO;
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
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainContentGrid.Children.Clear();
            MainContentGrid.Children.Add(new UserControlMemoryUsage());

            SeriesCollection SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                },
                new ColumnSeries
                {
                    Values = new ChartValues<decimal> { 5, 6, 4, 7 }
                }
            };
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PowerGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            ChangeMainContent(index);
        }

        private void MoveCursorMenu(int index)
        {
            MenuActiveItemIndicator.OnApplyTemplate();
            ActiveIndicator.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void ChangeMainContent(int index)
        {
            switch (index)
            {
                case 0:
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new UserControlMemoryUsage());
                    break;
                case 1:
                    MainContentGrid.Children.Clear();
                    //MainContentGrid.Children.Add(new UserControlMemoryUsage());
                    break;
                case 2:
                    MainContentGrid.Children.Clear();
                    //MainContentGrid.Children.Add(new UserControlMemoryUsage());
                    break;
                default:
                    break;
            }
        }

        private void AddFiles_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
