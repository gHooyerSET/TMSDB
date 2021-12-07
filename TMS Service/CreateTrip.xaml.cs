/*
 * FILE             : CreateOrder.xaml.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Allows the planner to create trips via a GUI.
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
    /// Interaction logic for CreateTrip.xaml
    /// </summary>
    public partial class CreateTrip : Window
    {
        Route route;
        TMSDB tmsdb;
        PlannerWindow planner;
        Random rand;

        /// <summary>
        /// Instantiates a CreateTrip window object.
        /// </summary>
        /// <param name="route">The route associated with the trip.</param>
        /// <param name="planner">The instance of the plannerWindow that created this window.</param>
        public CreateTrip(Route route, PlannerWindow planner)
        {
            InitializeComponent();
            this.route = route;
            this.planner = planner;
            rand = new Random();
            tmsdb = new TMSDB();
            FillCarrierBox();
            FillCityBoxes();
        }

        /// <summary>
        /// Fills the Carrier combo box.
        /// </summary>
        public void FillCarrierBox()
        {
            //This list will store the carriers
            List<string> carriers = new List<string>();
            string[] carriersArray = tmsdb.GetCarriers().Split(',');
            //Iterate through the array to get the carrier info
            for(int i = 0; i < carriersArray.Length;)
            {
                //If the carrier name hasn't been added, add it
                if(!carriers.Contains(carriersArray[i]))
                {
                    //Add the carrier to the list
                    carriers.Add(carriersArray[i]);
                }

                //Increment past the width of the table
                i += 7;
            }

            cbCarrier.ItemsSource = carriers;
            cbCarrier.SelectedValue = carriers[0];
        }

        /// <summary>
        /// Fills the city combo boxes with the cities associated with a particular carrier.
        /// </summary>
        public void FillCityBoxes()
        {
            //This list will store the carriers
            List<string> cities = new List<string>();
            string[] carriersArray = tmsdb.GetCarriers().Split(',');

            cbStartCity.IsEnabled = true;
            //Iterate through the array to get the carrier info
            for (int i = 0; i < carriersArray.Length;)
            {
                //If the carrier name hasn't been added, add it
                //We also need to check that the city is available for the carrier
                if (!cities.Contains(carriersArray[i+1]) && carriersArray[i].Contains(cbCarrier.SelectedItem.ToString())
                    && cbCarrier.SelectedItem.ToString() != string.Empty)
                {
                    //Add the carrier to the list
                    cities.Add(carriersArray[i+1]);
                }

                //Increment past the width of the table
                i += 7;
            }

            cbStartCity.ItemsSource = cities;
            cbEndCity.ItemsSource = cities;
        }

        /// <summary>
        /// On change of selection for the carrier combo box, we need
        /// to update the city combo boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCarrier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCityBoxes();
        }

        /// <summary>
        /// Creates a trip based on the selections made in the dialog box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateTrip_Click(object sender, RoutedEventArgs e)
        {
            //Make sure every box has something selected
            if(cbCarrier.Text != string.Empty && cbStartCity.Text != string.Empty
                && cbEndCity.Text != string.Empty && cbType.Text != string.Empty)
            {
                tmsdb.CreateTrip(cbCarrier.Text, route.RouteID, cbType.Text, cbStartCity.Text, cbEndCity.Text);
                planner.FillTripsGrid(route.RouteID);
                planner.sbiStatus.Content = "Trip created successfully";
                cbStartCity.SelectedValue = cbEndCity.Text;
                cbStartCity.IsEnabled = false;
                cbEndCity.SelectedValue = "";
                UpdateRouteCost();
            }
        }

        /// <summary>
        /// Updates the cost of a route by taking information from
        /// the trips.
        /// </summary>
        private void UpdateRouteCost()
        {
            string[] tripArray = tmsdb.GetTrips(route.RouteID).Split(',');
            float cost = 0;

            for(int i = 0; i < tripArray.Length;)
            {
                //The cost will be the rate * a random number between 1 and 5
                //just as an example. A real cost would be calculated here.
                cost += float.Parse(tripArray[i + 6]) * rand.Next(1,5);

                cost = (float)Math.Round(cost, 2);
                //Iterate to the next row (the table width + 1)
                i += 7;
            }

            tmsdb.RunQuery("UPDATE `group15-tms`.route SET cost=" + cost + " WHERE RouteID=" + route.RouteID);

        }
    }
}
