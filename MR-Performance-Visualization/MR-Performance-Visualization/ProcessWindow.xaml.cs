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
using System.Windows.Shapes;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for ProcessWindow.xaml
    /// </summary>
    public partial class ProcessWindow : Window
    {
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;
        public ChartValues<double> cpuValues { get; set; }
        public ChartValues<double> hcValues { get; set; }
        public ChartValues<double> privValues { get; set; }
        public SeriesCollection ProcessCpu_SC { get; set; }
        public SeriesCollection ProcessHc_SC { get; set; }
        public SeriesCollection ProcessPriv_SC { get; set; }
        public string[] Labels { get; set; }

        public ProcessWindow(String pName)
        {
            InitializeComponent();
            Console.WriteLine("Looking for this process: " + pName);
            GetDataForProcess(pName);
            processName.Text = pName;
        }

        private void GetDataForProcess(string processName)
        {
            if (tfps.ProcessDictionary != null)
            {
                List<Process> values = tfps.ProcessDictionary[processName];

                cpuValues = new ChartValues<double>();
                hcValues = new ChartValues<double>();
                privValues = new ChartValues<double>();


                //temp lists
                var tempcpuValues = new List<double>();
                var temphcValues = new List<double>();
                var tempprivValues = new List<double>();
                var tempLabels = new List<string>();

                Console.Write("Found ");
                Console.Write(values.Count);
                Console.WriteLine(" values for this specific process");

                foreach (Process p in values)
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
            DataContext = this;
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MenuController.Close_Button_Click(this);
        }
    }
}
