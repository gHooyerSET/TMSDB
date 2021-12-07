using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject
{
    /// <summary>
    /// Class to hold Carrier information for Carrier Update System
    /// </summary>
    public class Carrier
    {
        // CSV file
        public const string csvFileIn = "../../Carriers.csv";
        public const string csvFileOut = "../../Carriers.csv";

        public string name { get; set; }
        public string city { get; set; }
        public int ftla { get; set; }
        public int ltla { get; set; }
        public double fRate { get; set; }
        public double lRate { get; set; }
        public double rRate { get; set; }
    }
}
