using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TMSProject
{
    /// <summary>
    /// Class to hold Carrier information for Carrier Update System
    /// </summary>
    public class Carrier
    {
        // CSV file
        static public readonly string csvFileIn = ConfigurationManager.AppSettings.Get("rootPath") + "/Carriers.csv";
        static public readonly string csvFileOut = ConfigurationManager.AppSettings.Get("rootPath") + "/Carriers.csv";

        public string name { get; set; }
        public string city { get; set; }
        public int ftla { get; set; }
        public int ltla { get; set; }
        public double fRate { get; set; }
        public double lRate { get; set; }
        public double rRate { get; set; }
    }
}
