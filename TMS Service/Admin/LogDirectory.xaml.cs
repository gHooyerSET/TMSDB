/*
* FILE : LogDirectory.xaml.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-12-07
* DESCRIPTION : Lets admin select directory for log file
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using TMSProject;

namespace TMS_Service.Admin
{
    /// <summary>
    /// Interaction logic for LogDirectory.xaml
    /// </summary>
    public partial class LogDirectory : Window
    {
        static private readonly string logName = "\\serverLog.log";
        public LogDirectory()
        {
            InitializeComponent();
        }

        
        /**
        * <summary>
        * Create the log file directory specified in the directoryPath textbox
        * </summary>
        * 
        * <param name="e">The state of the click object</param>
        * <param name="sender">The object that is being passed</param>
        * 
        * <details>
        * The admin can create the directory for the log files by specifying the path information in the
        * directoryPath textbox.
        * </details>
        * 
        * <returns>
        * No Return (void)
        * </returns>
        */ 
        private void create_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var logPath = directoryPath.Text;

            config.AppSettings.Settings["logPath"].Value = logPath;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Logger.WriteLog("Path changed to : " + logPath);
            MessageBox.Show("Path changed to : " + logPath, "Log", MessageBoxButton.OK);
        }

        /**
        * <summary>
        * Opens log file
        * </summary>
        * 
        * <param name="e">The state of the click object</param>
        * <param name="sender">The object that is being passed</param>
        * 
        * <returns>
        * No Return (void)
        * </returns>
        */
        private void open_Click(object sender, RoutedEventArgs e)
        {
            // Get Log path from config
            var logPath = ConfigurationManager.AppSettings.Get("logPath");
            if (Directory.Exists(logPath))
            {
                var log = logPath + logName;
                Process.Start(log);
            }
        }
    }
}
