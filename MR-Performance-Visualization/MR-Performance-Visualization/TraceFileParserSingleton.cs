using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Performance_Visualization
{
    public sealed class TraceFileParserSingleton
    {
        TraceFileParserSingleton()
        {
        }

        //this makes constructor thread safe
        private static readonly object padlock = new object();
        private static TraceFileParserSingleton instance = null;
        public static TraceFileParserSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TraceFileParserSingleton();
                    }
                    return instance;
                }
            }
        }

        //all other accessible values
        public List<GlobalProcess> GlobalProcessList { get; set; }
        public List<string> ProcessNames { get; set; }
        public Dictionary<string, List<Process>> ProcessDictionary { get; set; } // name of process -> list of Process 'points' that contains ts, CPU, PRIV, and HC

        public void ParseTraceFile(string path)
        {
            Console.WriteLine("Start reading");
            string[] lines = System.IO.File.ReadAllLines(path);
            Console.WriteLine("Read finished");

            List<GlobalProcess> tempGlobalProcessList = new List<GlobalProcess>();
            ProcessDictionary = new Dictionary<string, List<Process>>();
            HashSet<string> uniqueProcessNames = new HashSet<string>();

            var pCount = 0;
            foreach (string line in lines)
            {
                pCount++;
                string[] row = line.Split('|');

                if (row[4].StartsWith("Global"))
                {
                    string fullTimestamp = row[0];
                    string[] splitTS = fullTimestamp.Split('-');
                    string hmsm = splitTS[1];
                    string[] splitHMS = hmsm.Split('.');
                    string timestamp = splitHMS[0];

                    int pid = int.Parse(row[1]);
                    int tid = int.Parse(row[2]);
                    double gcpu = 0.0;
                    double? gcpuPeak = 0.0;
                    int ghc = 0;
                    int? ghcPeak = 0;

                    string[] usertext = row[4].Split(';');


                    // value of peaks can be n.a
                    // find a way to deal with this
                    foreach (string column in usertext)
                    {
                        if (column.Contains("GCPU(_Total"))
                        {
                            string value = column.Split(':')[2];
                            gcpu = double.Parse(value.Remove(value.Length - 1, 1));
                        }

                        if (column.Contains("GCPUPeak"))
                        {
                            string value = column.Split(':')[1];
                            if (value.Contains("n.a"))
                            {
                                gcpuPeak = null;
                            }
                            else
                            {
                                gcpuPeak = double.Parse(value.Remove(value.Length - 1, 1));
                            }

                        }

                        if (column.Contains("GHC(_Total)"))
                        {
                            ghc = int.Parse(column.Split(':')[1]);

                        }

                        if (column.Contains("GHCPeak"))
                        {
                            string value = column.Split(':')[1];

                            if (value.Contains("n.a"))
                            {
                                ghcPeak = null;
                            }
                            else
                            {
                                ghcPeak = int.Parse(value);
                            }

                        }

                    }//foreach column in usertext

                    tempGlobalProcessList.Add(new GlobalProcess(timestamp, pid, tid, gcpu, gcpuPeak, ghc, ghcPeak));

                }
                else if (row[4].StartsWith("Process"))
                {
                    string fullTimestamp = row[0];
                    string[] splitTS = fullTimestamp.Split('-');
                    string hmsm = splitTS[1];
                    string[] splitHMS = hmsm.Split('.');
                    string timestamp = splitHMS[0];

                    string processName = "";
                    double cpu = 0.0;
                    double hc = 0.0;
                    double priv = 0.0;

                    string[] usertext = row[4].Split(';');
                    if (usertext.Length > 1)
                    {
                        for(var i = 0; i < usertext.Count(); i++)
                        {
                            
                            if(i == 0) //need to treat index 0 differently because it's formatted weird
                            {
                                string[] firstChunk = usertext[i].Split(':');
                                string fullProcessName = firstChunk[1];
                                processName = fullProcessName.Split('(')[0].Trim();
                            } else
                            {
                                //look for CPU, PRIV, and HC
                                if (usertext[i].StartsWith("CPU(")) //dont want to confuse CPU with CPUPeak
                                {
                                    string value = usertext[i].Split(':')[1];
                                    cpu = double.Parse(value.Remove(value.Length - 1, 1)); //remove '%'
                                }

                                if (usertext[i].StartsWith("HC("))
                                {
                                    string value = usertext[i].Split(':')[1];
                                    hc = double.Parse(value);
                                }

                                if (usertext[i].StartsWith("PRIV("))
                                {
                                    string value = usertext[i].Split(':')[1];
                                    priv = double.Parse(value.Remove(value.Length - 2, 2)); //remove 'MB'
                                }

                            }// if not first column 

               

                        }// for each column in usertext

                        //create new process data object and add it to the dictionary
                        Process processData = new Process(timestamp, processName, hc, null, priv, null, cpu, null);
                        //check for keys and add to data
                        if (ProcessDictionary.ContainsKey(processName))
                        {
                            //if name exists, just add process to the array
                            List<Process> existingData = ProcessDictionary[processName];
                            existingData.Add(processData);
                            ProcessDictionary[processName] = existingData;
                        }
                        else
                        {
                            //add new entry to dictionary
                            List<Process> newList = new List<Process>();
                            newList.Add(processData);
                            ProcessDictionary.Add(processName, newList);
                        }
                    }

                }// for all processes

            }//foreach line

            Console.WriteLine("finished");
            Console.WriteLine("Globals amount: " + tempGlobalProcessList.Count);
            Console.WriteLine("Process amount: " + (pCount - tempGlobalProcessList.Count));
            Console.WriteLine("Unique process names: " + uniqueProcessNames.Count);
            this.GlobalProcessList = tempGlobalProcessList;
            this.ProcessNames = ProcessDictionary.Keys.ToList();
      
        }//ParseTraceFile()
    }//TraceFileParser
}
