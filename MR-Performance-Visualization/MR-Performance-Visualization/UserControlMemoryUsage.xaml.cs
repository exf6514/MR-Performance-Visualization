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
using LiveCharts;
using LiveCharts.Wpf;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for UserControlMemoryUsage.xaml
    /// </summary>
    public partial class UserControlMemoryUsage : UserControl
    {
        public UserControlMemoryUsage(string filepath = "")
        {
            InitializeComponent();

            int graphStep = 0;

            //get an instance of the trace file parser singleton
            TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;

            //Reading the file and getting all global processes
            List<GlobalProcess> list = new List<GlobalProcess>();

            //get the path to  the .utr here. If file provided, get processes
            if (filepath != "")
            {
                Console.WriteLine("File path provided: " + filepath);
                tfps.ParseTraceFile(filepath);
                ChartValues<double> gcpuValues = new ChartValues<double>();
                ChartValues<double> ghcValues = new ChartValues<double>();

                //temp lists
                var tempGcpuValues = new List<double>();
                var tempGhcValues = new List<double>();
                var tempLabels = new List<string>();

                //get list from singleton
                list = tfps.GlobalProcessList;
                Console.WriteLine("list.Count: " + list.Count);

                foreach (GlobalProcess p in list)
                {
                    if (p.Gcpu > 0) tempGcpuValues.Add(p.Gcpu);
                    if (p.Ghc > 0) tempGhcValues.Add((double)p.Ghc);
                    tempLabels.Add(p.Timestamp);
                }
                //add to values
                gcpuValues.AddRange(tempGcpuValues);
                ghcValues.AddRange(tempGhcValues);

                //handle labels
                Labels = tempLabels.ToArray();
                graphStep = tempLabels.Count / 10;
                Console.WriteLine("graph step is: " + graphStep);

                //set series data to accessible attribute
                CPUSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Gcpu",
                        Values = gcpuValues
                    }
                };
                // handle counts series
                HCSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Ghc",
                        Values = ghcValues
                    }
                };
            }
            DataContext = this;
        }
        //accessible data
        public SeriesCollection CPUSeriesCollection { get; set; }
        public SeriesCollection HCSeriesCollection { get; set; }
        public string[] Labels { get; set; }
    }
}