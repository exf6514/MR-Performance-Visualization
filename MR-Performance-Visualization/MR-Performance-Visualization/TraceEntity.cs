using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Performance_Visualization
{
    public class TraceEntity
    {
        // Fields, properties, methods and events go here...
        public string timestamp = string.Empty;
        public string pid = string.Empty;
        public string tid = string.Empty;
        public string level = string.Empty;
        public string userText = string.Empty; //make this a class later for further ease of access?

    }
}
