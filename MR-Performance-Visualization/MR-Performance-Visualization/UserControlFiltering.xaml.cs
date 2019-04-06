using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for UserControlFiltering.xaml
    /// </summary>
    public partial class UserControlFiltering : UserControl
    {
        TraceFileParserSingleton tfps = TraceFileParserSingleton.Instance;
        public List<Process> filterProcesses { get; set; }
        private string LastSearchString { get; set; } //the last search the user made. Used for results file

        public UserControlFiltering()
        {
            InitializeComponent();
            init();
        }

        public UserControlFiltering(String selectedProcessName)
        {
            InitializeComponent();
            init();
            int index = process_name_cb.Items.IndexOf(selectedProcessName);
            process_name_cb.SelectedIndex = index;
        }

        private void init() {
            filterProcesses = new List<Process>();

            List<string> metrics = new List<string>(new string[] { "CPU", "HC", "PRIV" });
            List<string> comparators = new List<string>(new string[] { ">", ">=", "=", "<=", "<" });
            // set up combo boxes
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

            //if all the necessary data is available
            if (tfps.ProcessNames != null && tfps.ProcessNames.Count > 0)
            {
                process_name_cb.Items.Insert(0, "Any Process");
                foreach (string name in tfps.ProcessNames)
                {
                    process_name_cb.Items.Add(name);
                }
                process_name_cb.SelectedIndex = 0;

                //Associated_Trace_File_label.Text = tfps.Filename;
            }
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            //get the values from the interface
            string processName = process_name_cb.SelectedItem != null ? process_name_cb.SelectedItem.ToString() : "";
            string metricName = metric_cb.SelectedItem != null ? metric_cb.SelectedItem.ToString() : "";
            string comparator = comparator_cb.SelectedItem != null ? comparator_cb.SelectedItem.ToString() : "";
            string searchValue = value_tb.Text;

            List<Process> filteredValues = new List<Process>();

            if (processName != "" && tfps.ProcessDictionary != null && searchValue != "")
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

                this.Cursor = Cursors.Arrow; //back to useable cursor
                filterProcesses = filteredValues;

                if (filterProcesses.Count > 0)
                {
                    noResults.Visibility = Visibility.Hidden;
                }
                else {
                    noResults.Visibility = Visibility.Visible;
                }

                process_dg.ItemsSource = filterProcesses; //set data for table
                //Associated_Trace_File_label.Text = tfps.Filename;
                //store last searched query
                LastSearchString = processName + "|" + metricName + "|" + comparator + "|" + searchValue;
            }
        }//Search_btn_clicked()

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            //read in data
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Result Files|*.txt";
            ofd.Title = "Select a Result File";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                string filepath = ofd.FileName;
                string[] lines = File.ReadAllLines(filepath);

                //populate the search items
                //Associated_Trace_File_label.Text = lines[0];

                string[] queryParts = lines[1].Split('|'); // name, metric, comparator, value

                var nameIndex = process_name_cb.Items.IndexOf(queryParts[0]);
                if (nameIndex >= 0)
                {
                    process_name_cb.SelectedIndex = nameIndex;
                }
                else
                {
                    int newIndex = process_name_cb.Items.Add(queryParts[0]);
                    process_name_cb.SelectedIndex = newIndex;
                }

                metric_cb.SelectedIndex = metric_cb.Items.IndexOf(queryParts[1]);
                comparator_cb.SelectedIndex = comparator_cb.Items.IndexOf(queryParts[2]);
                value_tb.Text = queryParts[3];

                //parse process data

                lines = lines.Where((item, index) => index > 1).ToArray(); //gets rid of first two lines, leaving process data;
                
                if (lines.Count() > 0)
                {
                    filterProcesses = new List<Process>();//empty list
                    foreach (string line in lines)
                    {
                        string[] data = line.Split('|'); //timestamp, name, cpu, hc, priv
                        Process p = new Process(
                            data[0],//timestamp
                            data[1],//name
                            double.Parse(data[3]),//hc
                            null,
                            double.Parse(data[4]),//priv
                            null,
                            double.Parse(data[2]),//cpu
                            null
                            );
                        filterProcesses.Add(p);
                    }

                    process_dg.ItemsSource = filterProcesses;

                }

            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Console.WriteLine("saving file to: " + docPath);

            if (tfps.Filename != null && tfps.Filename != "")
            {

                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, tfps.Filename + "_results.txt")))
                {
                    //associated trace file 
                    Console.WriteLine("associated trace file: '" + tfps.Filename + "'");
                    outputFile.WriteLine(tfps.Filename);

                    //write latest query
                    Console.WriteLine("the last searched query is: '" + LastSearchString + "'");
                    outputFile.WriteLine(LastSearchString);

                    //write the latest filtered data
                    foreach (Process p in filterProcesses)
                    {
                        String[] values = { p.Timestamp, p.Name, p.CPU.ToString(), p.HC.ToString(), p.PRIV.ToString() };
                        String joined = String.Join("|", values);
                        Console.WriteLine(joined);
                        outputFile.WriteLine(joined);
                    }

                    //provide the file path to the user
                    MessageBox.Show(
                        "Your result file was saved to: " + docPath,
                        "Result File Created!"
                    );//message box

                }

            }// if trace file name exists
        }
    }
}
