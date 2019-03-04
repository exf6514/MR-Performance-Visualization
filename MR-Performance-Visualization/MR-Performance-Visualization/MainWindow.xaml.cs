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
        Utils utils = new Utils();

        public MainWindow()
        {
            InitializeComponent();

            MainContentGrid.Children.Clear();
            MainContentGrid.Children.Add(new UserControlMemoryUsage());

        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PowerGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
           //Creates a left click exception. Handler isn't necesarry.
           // DragMove();
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
                    //GLOBAL PROCESSES PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new UserControlMemoryUsage());
                    break;
                case 1:
                    //PER PROCESS PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new ProcessData());
                    break;
                case 2:
                    //FILTER PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new FilterDataWindow());
                    break;
                default:
                    break;
            }
        }

        private void AddFiles_Button_Click(object sender, RoutedEventArgs e)
        {
            //get files here
            //send to user contorl memory usage
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Trace Files|*.utr";
            ofd.Title = "Select a Trace File";
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == true)
            {
                string filepath = ofd.FileName;
                string filename = ofd.SafeFileName;
                MainContentGrid.Children.Clear();
                MainContentGrid.Children.Add(new UserControlMemoryUsage(filepath, filename));
            }

        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            utils.ApplyEffect(this);
            settingsWindow.ShowDialog();
            utils.ClearEffect(this);
        }
    }
}
