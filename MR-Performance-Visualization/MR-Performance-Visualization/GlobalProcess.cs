using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Create a namespace
namespace MR_Performance_Visualization
{
    /**
    * Create a class called Global Process
    */
    public class GlobalProcess
    {
        // Define public attributes
        public string Timestamp { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public double Gcpu { get; set; }
        public double? GcpuPeak { get; set; }
        public int Ghc { get; set; }
        public int? GhcPeak { get; set; }

        /**
        * Create a parameterized constructor that initializes attributes
        * @param timestamp -
        * @param pid -
        * @param tid - 
        * @param gcpu -
        * @param gcpuPeak -
        * @param ghc -
        * @parm ghcPeak -
        */
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
