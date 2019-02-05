using Microsoft.Win32;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Load_button_Click(object sender, RoutedEventArgs e)
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

                using(StreamReader reader = new StreamReader(fileStream))
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
        }//load button clicked 
    }
}
