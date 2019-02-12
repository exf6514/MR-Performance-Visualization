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

            //Reading the file and getting all global processes
            TraceFileParser tfp = new TraceFileParser();
            List<GlobalProcess> list = new List<GlobalProcess>();

            //get the path to  the .utr here. If file provided, get processes
            if (filepath != "")
            {
                Console.WriteLine("File path provided: " + filepath);
                list = tfp.GetGlobalProcesses(filepath);
                Console.WriteLine("list.Count: " + list.Count);
                ChartValues<double> values = new ChartValues<double>();
                var tempValues = new List<Double>();
                foreach (GlobalProcess p in list)
                {
                    tempValues.Add(p.Gcpu);
                }
                values.AddRange(tempValues);
                //set series data to accessible attribute
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Gcpu",
                        Values = values
                    }
                };
            }
            DataContext = this;
        }
        //accessible data
        public SeriesCollection SeriesCollection { get; set; }
    }
}