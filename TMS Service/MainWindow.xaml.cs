/*
 * FILE             : MainWindow.xaml.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Allows the user to log in to the app.
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMSProject;

namespace TMS_Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Logs into the service and opens the correct window to match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Instantiate new instance of a TMSDB object
            TMSDB tmsdb = new TMSDB();
            //Instantiate our parameter variables
            string userName = tbUserName.Text;
            string pwd = tbPassword.Password;
            string role;
            //And our login sucess variable
            bool login;
            //Run the login in command with tmsdb
            login = tmsdb.Login(userName, pwd, out role);

            //If the login was succesful, we need to hide this window
            //and open the correct window based on user role
            if(login)
            {
                if (role == "Admin")
                {
                    AdminWindow adminWindow = new AdminWindow(new User(userName, role), this);
                    adminWindow.Show();
                }
                else if(role == "Buyer")
                {
                    //Create a buyer window and pass the user information via a User object
                    //As well as this window, so that it might be re-opened / unhidden
                    BuyerWindow buyerWindow = new BuyerWindow(new User(userName, role), this);
                    //Then show the widnow
                    buyerWindow.Show();
                }
                else if(role == "Planner")
                {
                    PlannerWindow plannerWindow = new PlannerWindow(new User(userName, role), this);
                    plannerWindow.Show();
                }
                else
                {
                    Logger.WriteLog("Invalid role for user : " + userName);
                }
                tbPassword.Password = "";
                this.Hide();
            }


        }
    }
}
