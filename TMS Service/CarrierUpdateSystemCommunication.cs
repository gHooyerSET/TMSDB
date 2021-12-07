/*
* FILE : CarrierUpdateCommunication.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-11-25
* DESCRIPTION : Class to provide information to the Carrier Update System
*/
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject
{
    /// <summary>
    /// Class to get information from the Carrier Update System
    /// </summary>
    public static class CarrierUpdateSystemCommunication
    {
        // Constants for csv index's
        const int NAME = 0;
        const int CITY = 1;
        const int FTLA = 2;
        const int LTLA = 3;

        /// <summary>
        /// Reads Carrier CSV file and outputs data to a list of Carriers
        /// </summary>
        /// <param name="csvFileIn"> - <b>string</b> -CSV filename to input from.</param>
        /// <returns><b>List<Carrier></b> : List of carriers from CSV file</returns>     
        public static List<Carrier> ReadCSV(string csvFileIn)
        {
            var carrierList = new List<Carrier>();
            string currentName = "";

            try
            {
                // Read all line of csv file
                string[] rows = File.ReadAllLines(csvFileIn); 

                // Skip first row as it contains collumn headers
                foreach (string row in rows.Skip(1))
                {
                    // Split each line into corresponding carrier variables and add to list
                    string[] data = row.Split(',');
                    if (data[NAME] != null)
                    {
                        currentName = data[NAME];
                    }
                    Carrier carrier = new Carrier() { name = currentName, city = data[CITY], ftla = Int32.Parse(data[FTLA]), ltla = Int32.Parse(data[LTLA]) };
                    carrierList.Add(carrier);
                }
            }
            catch (MySqlException ex)
            {
                Logger.WriteLog("Carrier Update System read exception: " + ex);
            }
            return carrierList;
        }

        /// <summary>
        /// Reads Carrier CSV file and outputs data to a list
        /// </summary>
        /// <param name="carrierList"> - <b>List<Carrier></b> -List of carriers to output.</param>
        /// <param name="csvFileOut"> - <b>string</b> -CSV filename to output to.</param>
        public static void UpdateCSV(List<Carrier> carrierList, string csvFileOut)
        {
            try
            {
                File.WriteAllText(csvFileOut,"");
                // Write header to CSV
                File.AppendAllText(csvFileOut, $"{"cName"},{"dCity"},{"FTLA"},{"LTLA"},{"fRate"},{"lRate"},{"rRate"}\n");
                // Write each carrier in list as line to SCV
                foreach (Carrier carrier in carrierList)
                {
                    File.AppendAllText(csvFileOut, $"{carrier.name},{carrier.city},{carrier.ftla},{carrier.ltla},{carrier.fRate},{carrier.lRate},{carrier.rRate}\n");
                }
            }
            catch (MySqlException ex)
            {
                Logger.WriteLog("Carrier Update System write exception: " + ex);
            }
        }
    }
}
