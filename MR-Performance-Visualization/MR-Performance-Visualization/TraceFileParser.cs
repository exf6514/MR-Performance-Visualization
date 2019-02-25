using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Performance_Visualization
{
    class TraceFileParser
    {
        public List<GlobalProcess> GetGlobalProcesses(string path)
        {
            Console.WriteLine("Start reading");
            string[] lines = System.IO.File.ReadAllLines(path);
            Console.WriteLine("Read finished");

            List<GlobalProcess> globalProcessList = new List<GlobalProcess>();
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

                    }

                    globalProcessList.Add(new GlobalProcess(timestamp, pid, tid, gcpu, gcpuPeak, ghc, ghcPeak));

                } else if (row[4].StartsWith("Process"))
                {
                    string[] usertext = row[4].Split(':');
                    if(usertext.Length > 1)
                    {
                        string fullProcessName = usertext[1];
                        string processName = fullProcessName.Split('(')[0];
                        uniqueProcessNames.Add(processName);
                    }
                    
                }

            }

            Console.WriteLine("finished");
            Console.WriteLine("Globals amount: " + globalProcessList.Count);
            Console.WriteLine("Process amount: " + (pCount - globalProcessList.Count));
            Console.WriteLine("Unique process names: " + uniqueProcessNames.Count);
            return globalProcessList;

        }

    }
}