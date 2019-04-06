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
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;
        string filename;
        string filepath;

        public WelcomeWindow()
        {
            InitializeComponent();

            ActionTwo.Visibility = Visibility.Collapsed;
        }

        private void Load_files(object sender, RoutedEventArgs e)
        {
            //get files here
            //send to user contorl memory usage
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Trace Files|*.utr";
            ofd.Title = "Select a Trace File";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                ActionTwo.Visibility = Visibility.Visible;
                ActionOne.Visibility = Visibility.Hidden;
                filepath = ofd.FileName;
                filename = ofd.SafeFileName;
                tfps.ParseTraceFile(filepath, filename);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MenuController mc = new MenuController();
            mc.Settings_Button_Click(this);
        }

        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            MenuController mc = new MenuController();
            mc.Help_Button_Click(this);
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuController.Power_Button_Click();
        }
    }
}
