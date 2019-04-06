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
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;
        UserControlGlobalProcesses ucGlobalProcesses;
        UserControlProcesses ucProcesses;
        UserControlFiltering ucFiltering;

        public MainWindow()
        {
            InitializeComponent();
            ucGlobalProcesses = new UserControlGlobalProcesses();
            ucProcesses = new UserControlProcesses();
            ucFiltering = new UserControlFiltering();

            Associated_Trace_File_label.Text = tfps.Filename;

            MainContentGrid.Children.Clear();
            MainContentGrid.Children.Add(ucGlobalProcesses);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            ChangeMainContent(index);
        }

        public void MoveCursorMenu(int index)
        {
            MenuActiveItemIndicator.OnApplyTemplate();
            ActiveIndicator.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        public void ChangeMainContent(int index)
        {
            switch (index)
            {
                case 0:
                    //GLOBAL PROCESSES PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(ucGlobalProcesses);
                    break;
                case 1:
                    //PER PROCESS PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(ucProcesses);
                    break;
                case 2:
                    //FILTER PAGE
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(ucFiltering);
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
                
            }

        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
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
