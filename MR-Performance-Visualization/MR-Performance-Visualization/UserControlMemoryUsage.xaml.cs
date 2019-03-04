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

        public UserControlMemoryUsage(string filepath = "", string filename = "")
        {
            InitializeComponent();

            IsLoading = false;
            //get an instance of the trace file parser singleton
            tfps = TraceFileParserSingleton.Instance;

            //get the path to  the .utr here. If file provided, get processes
            if (filepath != "")
            {
                Console.WriteLine("File path provided: " + filepath);
                IsLoading = true;
                tfps.ParseTraceFile(filepath, filename);
                IsLoading = false;
                GetGlobalData();
            }
            else
            {
                if (tfps.GlobalProcessList != null && tfps.GlobalProcessList.Count > 0)
                {
                    //get global data
                    GetGlobalData();
                }
            }

            DataContext = this;
        }

        private void GetGlobalData()
        {

            ChartValues<double> gcpuValues = new ChartValues<double>();
            ChartValues<double> ghcValues = new ChartValues<double>();

            //temp lists
            var tempGcpuValues = new List<double>();
            var tempGhcValues = new List<double>();
            var tempLabels = new List<string>();

            foreach (GlobalProcess p in tfps.GlobalProcessList)
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

            //set series data to accessible attribute
            CPUSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Gcpu",
                        Values = gcpuValues,
                        PointGeometry = null
                    }
                };
            // handle counts series
            HCSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Ghc",
                        Values = ghcValues,
                        PointGeometry = null
                    }
                };
        }
        //accessible data
        public SeriesCollection CPUSeriesCollection { get; set; }
        public SeriesCollection HCSeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public bool IsLoading { get; set; }
        private TraceFileParserSingleton tfps;
    }
}