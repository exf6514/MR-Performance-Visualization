using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Performance_Visualization
{
    public class Process
    {
        public string Timestamp { get; set; }
        public string Name { get; set; }
        public double HC { get; set; }
        public double? HCPeak { get; set; }
        public double PRIV { get; set; }
        public double? PRIVPeak { get; set; }
        public double CPU { get; set; }
        public double? CPUPeak { get; set; }

        public Process(string timestamp, string name, double hc, double? hcPeak, double priv, double? privPeak, double cpu, double? cpuPeak)
        {
            Timestamp = timestamp;
            Name = name;
            HC = hc;
            HCPeak = hcPeak;
            PRIV = priv;
            PRIVPeak = privPeak;
            CPU = cpu;
            CPUPeak = cpuPeak;
        }
    }
}
