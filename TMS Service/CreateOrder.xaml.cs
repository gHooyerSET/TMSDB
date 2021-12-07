/*
 * FILE             : CreateOrder.xaml.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Allows the buyer to create orders via a GUI.
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
using TMSProject;

namespace TMS_Service
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        User user;
        BuyerWindow buyer;
        TMSDB tmsdb;

        /// <summary>
        /// Instantiates a new instance of a CreateOrder window object.
        /// </summary>
        /// <param name="user">The buyer creating the order.</param>
        public CreateOrder(User user, BuyerWindow buyer)
        {
            InitializeComponent();
            this.user = user;
            this.buyer = buyer;
            tmsdb = new TMSDB();
            FillComboBoxes();
        }

        /// <summary>
        /// On click, an order is created if the selected values are valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            //Make sure the selected values are valid
            if (cbStartCity.Text != string.Empty && cbEndCity.Text != string.Empty && dpOrderDate.SelectedDate.Value >= DateTime.Now.Date)
            {
                //If they are, create the order
                tmsdb.CreateOrder(user.UserName, cbStartCity.Text, cbEndCity.Text, dpOrderDate.SelectedDate.Value);
                //Then update the order grid.
                buyer.FillOrderGrid();
                //Then update the status message
                buyer.sbiStatus.Content = "Order created successfully.";
                //Then close this window.
                this.Close();
            }
            else
            {
                buyer.sbiViewing.Content = "Order could not be created.";
            }
            
        }

        /// <summary>
        /// Fills the combo boxes with valid cities.
        /// </summary>
        private void FillComboBoxes()
        {
            List<string> cities = new List<string>();
            string[] citiesArray;
            citiesArray = tmsdb.GetCarriers().Split(',');

            for(int i = 0; i < citiesArray.Length;)
            {
                if(!cities.Contains(citiesArray[i+1]))
                {
                    cities.Add(citiesArray[i + 1]);
                }
                i += 7;
            }

            cbStartCity.ItemsSource = cities;
            cbEndCity.ItemsSource = cities;
        }

        /// <summary>
        /// On cancel, close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
