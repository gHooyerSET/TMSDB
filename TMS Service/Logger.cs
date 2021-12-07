/*
 * FILE             : Logger.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Logs messages to a .log file.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Diagnostics;


/*
* TITLE         :    Logger
* AUTHOR        :    Norbert Mika
* DATE          :    2021-11-26
* VERSION       :    Not Specified
* AVAILABIILTY  :    https://stuconestogacon.sharepoint.com/sites/CC_course483719/Shared%20Documents/W09%20-%20Services/W09Service.zip
*/

namespace TMSProject
{
    /// <summary>
    /// Provides an easy to use way to write logs to a file.
    /// </summary>
    static class Logger
    {

        static private string logPath = ConfigurationManager.AppSettings.Get("logPath");
        static private readonly string logName = "\\TMSLog.log";

        /// <summary>
        /// Writes a message to the log file.
        /// </summary>
        /// <param name="message">a <b>string</b> that will be entered into the log.</param>
        public static void WriteLog(string message)
        {
            StreamWriter log = null;
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                //If the file doesn't exist, make it
                if (!File.Exists(logPath + logName))
                {
                    FileStream fs = File.Create(logPath + logName);
                    fs.Close();
                }
                //Then write the message to the log.
                log = File.AppendText(logPath + logName);
                log.WriteLine(DateTime.Now.ToString() + " : " + message);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (log != null)
                {
                    log.Close();
                }
            }

        }
    }
}
