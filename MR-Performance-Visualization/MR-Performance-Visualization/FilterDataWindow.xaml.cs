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
    /// Interaction logic for FilterDataWindow.xaml
    /// </summary>
    public partial class FilterDataWindow : UserControl
    {
        public FilterDataWindow()
        {
            InitializeComponent();

            List<string> metrics = new List<string>(new string[] { "CPU", "HC", "PRIV" });
            List<string> comparators = new List<string>(new string[] { ">", ">=", "=", "<=", "<" });

            tfps = TraceFileParserSingleton.Instance;

            //if all the necessary data is available
            if(tfps.ProcessNames != null && tfps.ProcessNames.Count > 0)
            {
                process_name_cb.Items.Insert(0, "Any Process");
                foreach(string name in tfps.ProcessNames)
                {
                    process_name_cb.Items.Add(name);
                }
                process_name_cb.SelectedIndex = 0;

                foreach (string m in metrics)
                {
                    metric_cb.Items.Add(m);
                }
                metric_cb.SelectedIndex = 0;

                foreach (string c in comparators)
                {
                    comparator_cb.Items.Add(c);
                }
                comparator_cb.SelectedIndex = 0;
            }

        }
        private TraceFileParserSingleton tfps;

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
