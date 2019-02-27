using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Performance_Visualization
{
    public class GlobalProcess
    {
        public string Timestamp { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public double Gcpu { get; set; }
        public double? GcpuPeak { get; set; }
        public int Ghc { get; set; }
        public int? GhcPeak { get; set; }

        public GlobalProcess(string timestamp, int pid, int tid, double gcpu, double? gcpuPeak, int ghc, int? ghcPeak)
        {
            Timestamp = timestamp;
            Pid = pid;
            Tid = tid;
            Gcpu = gcpu;
            GcpuPeak = gcpuPeak;
            Ghc = ghc;
            GhcPeak = ghcPeak;
        }
    }
}