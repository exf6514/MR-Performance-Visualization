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
using System.Windows.Shapes;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        Utils utils = new Utils();

        public WelcomeWindow()
        {
            InitializeComponent();
            ActionOne.Visibility = Visibility.Collapsed;
            ActionTwo.Visibility = Visibility.Collapsed;
            //ActionThree.Visibility = Visibility.Collapsed;
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if(ActionOne.Visibility != Visibility.Collapsed)
            {
                LoadButton_Click(sender, e);
                ActionOne.Visibility = Visibility.Collapsed;
                ActionThree.Visibility = Visibility.Collapsed;
                ActionTwo.Visibility = Visibility.Visible;
                this.IsEnabled = false;
            }

            if (ActionThree.Visibility != Visibility.Collapsed)
            {
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            utils.ApplyEffect(this);
            settingsWindow.ShowDialog();
            utils.ClearEffect(this);
        }
    }
}
