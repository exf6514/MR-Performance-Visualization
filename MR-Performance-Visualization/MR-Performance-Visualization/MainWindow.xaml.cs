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

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //load trace file button has been clicked
            //open the file browser dialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".utr";
            ofd.RestoreDirectory = true; //opens the last opened directory

            if (ofd.ShowDialog() == true)
            {
                //Get the name of specified file
                string filename = ofd.FileName;
                Console.WriteLine("Selected filename: " + filename);

                //read contents of file into Stream
                var fileStream = ofd.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    //read contents
                    //string fileContent = reader.ReadToEnd();
                    //Console.WriteLine("File Contents: ");
                    //Console.WriteLine(fileContent);

                    string line;
                    int lineCount = 0;
                    while ((line = reader.ReadLine()) != null)
                    {

                        if (lineCount > 0) //skip first line, its the "Format" one
                        {
                            //Console.WriteLine(line);
                            string[] items = line.Split('|');
                            items.ToList().ForEach(Console.WriteLine);
                        }

                        lineCount++;
                    } // while line

                }//using stream reader
            }//dialog
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
        }

        private void MoveCursorMenu(int index)
        {
            MenuActiveItemIndicator.OnApplyTemplate();
            ActiveIndicator.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }
    }
}
