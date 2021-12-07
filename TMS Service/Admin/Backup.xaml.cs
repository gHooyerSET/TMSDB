/*
* FILE : Backup.xaml.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-12-07
* DESCRIPTION : View allows admin to backup DB to entered directory
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
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;

namespace TMS_Service.Admin
{
    /// <summary>
    /// Interaction logic for Backup.xaml
    /// </summary>
    public partial class Backup : Window
    {
        public Backup()
        {
            InitializeComponent();
            directoryPath.Text = ConfigurationManager.AppSettings.Get("backupPath");
        }

        /// <summary>
        /// Creates a backup from selected directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBackup_Click(object sender, RoutedEventArgs e)
        {
            string db = "server=127.0.0.1;uid=root;pwd=password;database=group15-tms";
            string file = directoryPath.Text + "\\backup" + DateTime.Now.ToString("yyyyMMddHHmmss") +  ".sql";

            

            try
            {
                if (!Directory.Exists(directoryPath.Text))
                {
                    Directory.CreateDirectory(directoryPath.Text);
                }
                using (MySqlConnection conn = new MySqlConnection(db))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup backup = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            backup.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show("Backup success", "Backup", MessageBoxButton.OK);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Backup fail : " + ex, "Backup", MessageBoxButton.OK);
            }
            this.Close();
        }
    }
}
