using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

            filterProcesses = new List<Process>();

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
            //get the values from the interface
            string processName = process_name_cb.SelectedItem != null ? process_name_cb.SelectedItem.ToString() : "";
            string metricName = metric_cb.SelectedItem != null ? metric_cb.SelectedItem.ToString() : "";
            string comparator = comparator_cb.SelectedItem != null ? comparator_cb.SelectedItem.ToString() : "";
            string searchValue = value_tb.Text;

            List<Process> filteredValues = new List<Process>();

            if (processName != "" && tfps.ProcessDictionary != null)
            {
                List<Process> processesValues = new List<Process>();
                
                if (processName == "Any Process")
                {
                    foreach (List<Process> pl in tfps.ProcessDictionary.Values)
                    {
                        processesValues.AddRange(pl);
                    }
                } else
                {
                    processesValues = tfps.ProcessDictionary[processName];
                }

                this.Cursor = Cursors.Wait;
                for (int i = 0; i < processesValues.Count; i++) {

                    Process p = processesValues[i];

                    if (metricName == "CPU")
                    {
                        //compare p.CPU to searchValue based on comparator
                        switch (comparator)
                        {
                            case ">":
                                if (p.CPU > double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case ">=":
                                if (p.CPU >= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "=":
                                if (p.CPU == double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<=":
                                if (p.CPU <= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<":
                                if (p.CPU < double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            default:
                                break;
                        }
                    } else if (metricName == "HC")
                    { 
                        //compare p.HC to searchValue based on comparator
                        switch (comparator)
                        {
                            case ">":
                                if (p.HC > double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case ">=":
                                if (p.HC >= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "=":
                                if (p.HC == double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<=":
                                if (p.HC <= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<":
                                if (p.HC < double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            default:
                                break;
                        }

                    } else if (metricName == "PRIV")
                    {
                        //compare p.PRIV to searchValue based on comparator
                        switch (comparator)
                        {
                            case ">":
                                if (p.PRIV > double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case ">=":
                                if (p.PRIV >= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "=":
                                if (p.PRIV == double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<=":
                                if (p.PRIV <= double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            case "<":
                                if (p.PRIV < double.Parse(searchValue)) filteredValues.Add(p);
                                break;
                            default:
                                break;
                        }
                    }

                }//foreach value
                this.Cursor = Cursors.Arrow;
                Console.WriteLine(filteredValues.Count);
                filterProcesses = filteredValues;
                process_dg.ItemsSource = filteredValues;
            }
        }//Search_btn_clicked()
        public List<Process> filterProcesses { get; set; }
    }
}
