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

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for Processes.xaml
    /// </summary>
    public partial class UserControlProcesses : UserControl
    {
        Utils utils = new Utils();
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;

        public UserControlProcesses()
        {
            InitializeComponent();

            List<Process> processes = new List<Process>();
            List<string> names = tfps.ProcessNames;

            if (names != null)
            {
                names.Sort();
                foreach (string name in names)
                {
                    processes.Add(new Process { Name = name });
                }

                processesTable.ItemsSource = processes;
            }
        }

        private void OpenProcessWindow(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow(getSeletedItem());
            Window parentWindow = Window.GetWindow(this);
            utils.ApplyEffect(parentWindow);
            processWindow.ShowDialog();
            utils.ClearEffect(parentWindow);
        }

        private void OpenFilteringWindow(object sender, RoutedEventArgs e) {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.MainContentGrid.Children.Add(new UserControlFiltering(getSeletedItem()));
            parentWindow.MoveCursorMenu(2);
        }

        private String getSeletedItem() {
            Process process = (Process)processesTable.SelectedItem;
            return process.Name;
        }

        private class Process
        {
            public string Name { get; set; }
        }
    }
}
