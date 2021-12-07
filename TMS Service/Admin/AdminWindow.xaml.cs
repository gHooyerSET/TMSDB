/*
* FILE : AdminWindow.xaml.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-12-07
* DESCRIPTION :Admin user view window
*/
using MySql.Data.MySqlClient;
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
using TMS_Service.Admin;
using TMSProject;

namespace TMS_Service
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        User user;
        MainWindow main;
        TMSDB tmsdb;
        List<Carrier> carrierList = new List<Carrier>();

        public AdminWindow(User user, MainWindow main)
        {
            InitializeComponent();
            this.user = user;
            this.main = main;
            sbiUserName.Content += user.UserName;
            tmsdb = new TMSDB();
            FillCarrierGrid();
        }

        /// <summary>
        /// Logs the user out. Closes this window and unhides the login window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            //Show the login window
            main.Show();
            //And then close the buyer window
            this.Close();
        }

        /// <summary>
        /// Shows user the log window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logDirectory_Click(object sender, RoutedEventArgs e)
        {
            Admin.LogDirectory logDirectory = new Admin.LogDirectory();
            logDirectory.Show();
        }

        /// <summary>
        /// Shows admin the network settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connection_Click(object sender, RoutedEventArgs e)
        {
            Admin.NetworkSettings networkSettings = new Admin.NetworkSettings();
            networkSettings.Show();
        }


        /// <summary>
        /// Fills the datagrid with Carrier information
        /// </summary>
        private void FillCarrierGrid()
        {
            dgInfo.ItemsSource = null;

            string carrierString = tmsdb.GetCarriers();
            string[] carrierStringArray = carrierString.Split(',');

            for (int i = 0; i < carrierStringArray.Length;)
            {
                //Create a new order
                Carrier carrier = new Carrier();
                carrier.name = carrierStringArray[i];
                carrier.city = carrierStringArray[i+1];
                carrier.ftla = Int32.Parse(carrierStringArray[i+2]);
                carrier.ltla = Int32.Parse(carrierStringArray[i + 3]);
                carrier.fRate = Double.Parse(carrierStringArray[i + 4]);
                carrier.lRate = Double.Parse(carrierStringArray[i + 5]);
                carrier.rRate = Double.Parse(carrierStringArray[i + 6]);
                carrierList.Add(carrier);
                //Itterate forward with of table columns
                i += 7;
            }
            dgInfo.ItemsSource = carrierList;
        }

        /// <summary>
        /// Adds a row for a new carrier. Admin must enter the info and update the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string carrierString = tmsdb.GetCarriers();
            string[] carrierStringArray = carrierString.Split(',');

            Carrier carrier = new Carrier();
            carrier.name = "";
            carrier.city = "";
            carrier.ftla = 0;
            carrier.ltla = 0;
            carrier.fRate = 0;
            carrier.lRate = 0;
            carrier.rRate = 0;
            carrierList.Add(carrier);
            dgInfo.ItemsSource = carrierList;
            dgInfo.Items.Refresh();
        }

        /// <summary>
        /// Updates CSV file that contains carrier data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CarrierUpdateSystemCommunication.UpdateCSV(carrierList, Carrier.csvFileOut);
            MessageBox.Show("Updated : " + Carrier.csvFileOut, "My App", MessageBoxButton.OK);

            dgInfo.Items.Refresh();
        }

        /// <summary>
        /// Shows current routes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showRoutes_Click(object sender, RoutedEventArgs e)
        {
            RouteWindow routeWindow = new RouteWindow();
            routeWindow.Show();
        }

        /// <summary>
        /// Admin can select a directory and iniate a backup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backup_Click(object sender, RoutedEventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
        }
    }
}
