/*
* FILE : NetworkSettings.xaml.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-12-07
* DESCRIPTION : Allows admin to select network setting, saved to App.config at runtime
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
using System.Configuration;

namespace TMS_Service.Admin
{
    /// <summary>
    /// Interaction logic for NetworkSettings.xaml
    /// </summary>
    public partial class NetworkSettings : Window
    {
        public NetworkSettings()
        {
            InitializeComponent();
        }

        /**
        * <summary>
        * Sets the network IP and Port to App.config file
        * </summary>
        * 
        * <param name="e">The state of the click object</param>
        * <param name="sender">The object that is being passed</param>
        * 
        * <returns>
        * No Return (void)
        * </returns>
        */
        private void btnSetNetwork_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var ip = setIPTextBx.Text;
            var port = setPortTextBx.Text;

            config.AppSettings.Settings["ipAddress"].Value = ip;
            config.AppSettings.Settings["port"].Value = port;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            this.Close();
        }

        /**
       * <summary>
       * Cancels network set and goes back to admin screen
       * </summary>
       * 
       * <param name="e">The state of the click object</param>
       * <param name="sender">The object that is being passed</param>
       * 
       * <returns>
       * No Return (void)
       * </returns>
       */
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
