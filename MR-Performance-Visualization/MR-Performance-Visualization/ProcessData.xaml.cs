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
    /// Interaction logic for ProcessData.xaml
    /// </summary>
    public partial class ProcessData : UserControl
    {
        public ProcessData()
        {
            InitializeComponent();

            process_names_cb.Items.Insert(0, "Please select a process");
            process_names_cb.SelectedIndex = 0;

            TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;
            List<string> names = tfps.ProcessNames;
            if (names != null) //could be null if no data loaded yet
            {
                names.Sort();
                foreach (string name in names)
                {
                    process_names_cb.Items.Add(name);
                }
            }
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = process_names_cb.SelectedIndex;
            if(index > 0)
            {
                string processName = process_names_cb.SelectedItem.ToString();
                Console.WriteLine("Looking for this process: " + processName);
            }
        }
    }
}
