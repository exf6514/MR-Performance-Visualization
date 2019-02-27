using LiveCharts;
using LiveCharts.Wpf;
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
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;

        public ProcessData()
        {
            InitializeComponent();

            process_names_cb.Items.Insert(0, "Please select a process");
            process_names_cb.SelectedIndex = 0;

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
                if (tfps.ProcessDictionary != null)
                {
                    List<Process> values = tfps.ProcessDictionary[processName];

                    ChartValues<double> cpuValues = new ChartValues<double>();
                    ChartValues<double> hcValues = new ChartValues<double>();
                    ChartValues<double> privValues = new ChartValues<double>();


                    //temp lists
                    var tempcpuValues = new List<double>();
                    var temphcValues = new List<double>();
                    var tempprivValues = new List<double>();
                    var tempLabels = new List<string>();

                    Console.Write("Found ");
                    Console.Write(values.Count);
                    Console.WriteLine(" values for this specific process");

                    foreach(Process p in values)
                    {
                        if (p.CPU > 0) tempcpuValues.Add(p.CPU);
                        if (p.HC > 0) temphcValues.Add(p.HC);
                        if (p.PRIV > 0) tempprivValues.Add(p.PRIV);
                        tempLabels.Add(p.Timestamp);
                    }

                    cpuValues.AddRange(tempcpuValues);
                    hcValues.AddRange(temphcValues);
                    privValues.AddRange(tempprivValues);
                    Labels = tempLabels.ToArray();

                    ProcessCpu_SC = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Cpu",
                            Values = cpuValues,
                            PointGeometry = null
                        }
                    };

                    ProcessHc_SC = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Hc",
                            Values = hcValues,
                            PointGeometry = null
                        }
                    };

                    ProcessPriv_SC = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Priv",
                            Values = privValues,
                            PointGeometry = null
                        }
                    };

                }// if dictionary exists
            }
            DataContext = this;
        }//search button clicked

        public SeriesCollection ProcessCpu_SC { get; set; }
        public SeriesCollection ProcessHc_SC { get; set; }
        public SeriesCollection ProcessPriv_SC { get; set; }
        public string[] Labels { get; set; }
    }
}
