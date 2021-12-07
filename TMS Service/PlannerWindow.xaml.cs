/*
 * FILE             : PlannerWindow.xaml.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Provides the planner with a GUI.
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
using System.IO;
using System.Configuration;


/*
 * TITLE        : Populate Data Grid
 * AUTHOR       : Ramashankar
 * DATE         : 2021-12-07
 * VERSION      : N/A
 * AVAILABLE    : https://stackoverflow.com/questions/20350886/wpf-fill-data-on-data-grid
 */

namespace TMS_Service
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        MainWindow main;
        TMSDB tmsdb;
        private User user;
        public User buyer;

        /// <summary>
        /// Creates a PlannerWindow object.
        /// </summary>
        /// <param name="user">A user object representing the logged-in planner.</param>
        /// <param name="main">A reference to the Main window.</param>
        public PlannerWindow(User user, MainWindow main)
        {
            InitializeComponent();
            sbiUser.Content += user.UserName;
            tmsdb = new TMSDB();
            this.user = user;
            this.main = main;
        }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            this.Close();
        }

        /// <summary>
        /// This event fires when the PlannerWindow closes. It shows the MainWindow again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlannerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.Show();
        }

        /// <summary>
        /// Fills the Order's grid when the buyer textbox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewOrders_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillOrderGrid();
        }

        /// <summary>
        /// Fills the order grid with info from the buyer when the menu item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miViewOrders_Click(object sender, RoutedEventArgs e)
        {
            FillOrderGrid();
        }

        /// <summary>
        /// Allows the user to manually view routes associated with an order,
        /// using an order # number that they provide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewRoutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            int orderID;
            if(Int32.TryParse(tbViewRoutes.Text, out orderID))
            {
                FillRouteGrid(orderID);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewTrips_TextChanged(object sender, TextChangedEventArgs e)
        {
            int routeID;
            if(Int32.TryParse(tbViewTrips.Text, out routeID))
            {
                FillTripsGrid(routeID);
            }
            
        }
        /// <summary>
        /// Fills the data grid with routes related to the selected order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewRoutesSel_Click(object sender, RoutedEventArgs e)
        {
            if(dgInfo.SelectedItem == null)
            {
                sbiStatus.Content = "Please select an order or trip";
            }
            else if (dgInfo.SelectedItem is Order)
            {
                Order order = (Order)dgInfo.SelectedItem;

                FillRouteGrid(order.OrderID);
            }
            else if (dgInfo.Items.GetItemAt(0) is Trip)
            {
                //Get a trip from the list
                Trip trip = (Trip)dgInfo.Items.GetItemAt(0);
                int orderID;
                //Try to parse the order ID using the routeID from the trip
                if (Int32.TryParse(tmsdb.GetRoute(trip.RouteID).Split(',')[1], out orderID))
                {
                    FillRouteGrid(orderID);
                }
            }
        }
        /// <summary>
        /// Fills the data grid with routes related to the selected order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewTripsSel_Click(object sender, RoutedEventArgs e)
        {
            if (dgInfo.SelectedItem is Route)
            {
                Route route = (Route)dgInfo.SelectedItem;

                FillTripsGrid(route.RouteID);
            }
        }

        /// <summary>
        /// Allows the user to display orders or refresh them by hitting enter when typing
        /// in the buyer textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbViewOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                FillOrderGrid();
            }
        }


        /// <summary>
        /// Allows the user to view routes in the data grid.
        /// </summary>
        /// <param name="orderID">The OrderID associated with the route(s).</param>
        public void FillRouteGrid(int orderID)
        {
            
            //Make sure the data grid 'exists'
            if (dgInfo != null)
            {
                //Then we can clear the item source
                dgInfo.ItemsSource = null;

                //Instantiate some variables we'll need
                List<Route> routes = new List<Route>();
                string routesString = tmsdb.GetRoutes(orderID);
                string[] routesStringArray = routesString.Split(',');

                try
                {
                    //Only fill the thing if SOME sort of string was created
                    if (routesString != string.Empty)
                    {
                        //This for loop creates Order objects from the ordersStringArray
                        for (int i = 0; i < routesStringArray.Length;)
                        {
                            //Create a new order
                            Route route = new Route(Int32.Parse(routesStringArray[i]), orderID, routesStringArray[i + 2],
                                routesStringArray[i + 3], routesStringArray[i + 4], routesStringArray[i + 5], float.Parse(routesStringArray[i+6]));
                            //Then we add it to the list
                            routes.Add(route);
                            //Then we iterate forward 6 indexes (the width of the table in columns)
                            i += 7;
                        }
                    }
                    else
                    {
                        routes.Add(new Route(-1, orderID, null, null, null, null, -1));
                        
                    }
                    sbiCurrentView.Content = "Viewing: Routes";
                    dgInfo.ItemsSource = routes;
                }
                catch (Exception ex)
                {
                    //In case of exception, write a log message.
                    Logger.WriteLog(ex.Message);
                }


                
            }
        }

        /// <summary>
        /// Allows the user to view orders in the data grid.
        /// </summary>
        public void FillOrderGrid()
        {
            //Make sure the data grid 'exists'
            if(dgInfo != null)
            {
                //Then we can clear the item source
                dgInfo.ItemsSource = null;

                //Instantiate some variables we'll need
                List<Order> orders = new List<Order>();
                string ordersString = tmsdb.GetOrders(tbViewOrders.Text);
                string[] ordersStringArray = ordersString.Split(',');

                try
                {
                    //Only fill the thing if SOME sort of string was created
                    if (ordersString != string.Empty)
                    {
                        //This for loop creates Order objects from the ordersStringArray
                        for (int i = 0; i < ordersStringArray.Length;)
                        {
                            //Create a new order
                            Order order = new Order(Int32.Parse(ordersStringArray[i]), ordersStringArray[i + 1], ordersStringArray[i + 2],
                                ordersStringArray[i + 3], ordersStringArray[i + 4], DateTime.Parse(ordersStringArray[i + 5]));
                            //Then we add it to the list
                            orders.Add(order);
                            //Then we iterate forward 6 indexes (the width of the table in columns)
                            i += 6;
                        }
                    }
                    else
                    {
                        orders.Add(new Order(-1, null, null, null, null, DateTime.MinValue));
                    }

                    sbiCurrentView.Content = "Viewing: Orders";
                    dgInfo.ItemsSource = orders;
                }
                catch (Exception ex)
                {
                    //In case of exception, write a log message.
                    Logger.WriteLog(ex.Message);
                }


                
            }
        }

        /// <summary>
        /// Allows the user to view trips associated with the routeID in the 
        /// data grid.
        /// </summary>
        /// <param name="routeID"><b>int</b> - The RouteID to search for trips by.</param>
        public void FillTripsGrid(int routeID)
        {
            //Make sure the data grid 'exists'
            if (dgInfo != null)
            {
                //Then we can clear the item source
                dgInfo.ItemsSource = null;

                //Instantiate some variables we'll need
                List<Trip> trips = new List<Trip>();
                string tripsString = tmsdb.GetTrips(routeID);
                string[] tripsStringArray = tripsString.Split(',');

                try
                {
                    //Only fill the thing if SOME sort of string was created
                    if (tripsString != string.Empty)
                    {
                        //This for loop creates Order objects from the ordersStringArray
                        for (int i = 0; i < tripsStringArray.Length;)
                        {
                            //Create a new trip
                            Trip trip = new Trip(Int32.Parse(tripsStringArray[i]), tripsStringArray[i+1],routeID,tripsStringArray[i+3],
                                tripsStringArray[i+4], tripsStringArray[i+5], float.Parse(tripsStringArray[i+6]));
                            //Then we add it to the list
                            trips.Add(trip);
                            //Then we iterate forward 6 indexes (the width of the table in columns)
                            i += 7;
                        }
                    }
                    else
                    {
                        trips.Add(new Trip(-1,null,routeID, null,null,null,-1));
                    }

                    sbiCurrentView.Content = "Viewing: Trips";
                    dgInfo.ItemsSource = trips;
                }
                catch (Exception ex)
                {
                    //In case of exception, write a log message.
                    Logger.WriteLog(ex.Message);
                }

            }

            
        }

        /// <summary>
        /// Creates a new route conditionally, based on the currently selected Route or Order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCreateRoutes_Click(object sender, RoutedEventArgs e)
        {
            //This bool is used to make sure that the order was created
            bool success = false;
            //Check if an order is selected
            if(dgInfo.SelectedItem is Order)
            {
                Order order = (Order)dgInfo.SelectedItem;
                //Make sure that there is not a route already associated with the OrderID
                if(tmsdb.GetRoutes(order.OrderID) == string.Empty)
                {
                    //Create the route
                    tmsdb.CreateRoute(order.OrderID, user.UserName);
                    //Display the routes
                    FillRouteGrid(order.OrderID);
                    //Operation successful
                    success = true;
                }                
            }
            else if (dgInfo.Items.GetItemAt(0) is Route)
            {
                Route route = (Route)dgInfo.Items.GetItemAt(0);
                
                //Make sure that no route exists 
                //(will have a single route with routeID == -1)
                //And that an order exists for the orderID
                if(route.RouteID == -1 && tmsdb.GetOrder(route.OrderID) != string.Empty)
                {
                    //Create the route
                    tmsdb.CreateRoute(route.OrderID, user.UserName);
                    FillRouteGrid(route.OrderID);
                    success = true;
                }
            }
            //Display an appropriate message
            if(success)
            {
                sbiStatus.Content = "Route created successfully";
            }
            else
            {
                sbiStatus.Content = "No route created.";
            }

        }

        /// <summary>
        /// Opens the Create Trips window to allow the planner
        /// to create trips to attach to a route.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCreateTrips_Click(object sender, RoutedEventArgs e)
        {

            if(dgInfo.SelectedItem is Route)
            {
                //We can now safely cast the selected item to a route.
                Route route = (Route)dgInfo.SelectedItem;
                //We must also make sure that a route exists for the trip.
                if(tmsdb.GetRoute(route.RouteID) != string.Empty)
                {
                    CreateTrip createTrip = new CreateTrip(route, this);
                    createTrip.Show();
                }                
            }
            
        }

        /// <summary>
        /// Displays the invoice summary of the current buyer (using the buyerID textbox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInvoiceSummaryBuyer_Click(object sender, RoutedEventArgs e)
        {
            List<InvoiceSummary> summaries = new List<InvoiceSummary>();
            InvoiceSummary summary = new InvoiceSummary(tbViewOrders.Text);
            summaries.Add(summary);
            dgInfo.ItemsSource = summaries;
            sbiCurrentView.Content = "Viewing: Invoice Summaries";
        }

        /// <summary>
        /// Displays the invoice summaries for all buyers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInvoiceSummaryAll_Click(object sender, RoutedEventArgs e)
        {
            //Get the array of buyers
            string[] buyers = tmsdb.GetBuyers().Split(',');
            
            //Make sure that buyers were found
            if(buyers.Length > 0)
            {
                List<InvoiceSummary> summaries = new List<InvoiceSummary>();
                for (int i = 0; i < buyers.Length; i++)
                {
                    //Get the invoice summary for the buyer
                    InvoiceSummary summary = new InvoiceSummary(buyers[i]);
                    summaries.Add(summary);
                }
                dgInfo.ItemsSource = summaries;
                sbiCurrentView.Content = "Viewing: Invoice Summaries";
            }          

        }

        /// <summary>
        /// Exports an invoice summary .csv based on the currently displayed invoice summaries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportSummaries_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = ConfigurationManager.AppSettings.Get("invoiceSummaryPath") + "invoiceSummary" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                bool success = false;

                //Make sure we're looking at invoice summaries
                if (dgInfo.Items.GetItemAt(0) is InvoiceSummary)
                {
                    //Check if the directory exists
                    if (!Directory.Exists(ConfigurationManager.AppSettings.Get("invoiceSummaryPath")))
                    {
                        //Create it if it doesn't
                        Directory.CreateDirectory(ConfigurationManager.AppSettings.Get("invoiceSummaryPath"));
                    }
                    //Check if the file exists
                    if (!File.Exists(filePath))
                    {
                        //Create it then append the header
                        File.AppendAllText(filePath, "BuyerID,TotalInvoices,Cost\n");
                    }
                    for (int i = 0; i < dgInfo.Items.Count; i++)
                    {
                        //Get the invoice summary from the data grid
                        InvoiceSummary invoiceSummary = (InvoiceSummary)dgInfo.Items.GetItemAt(i);
                        //Create the line of text
                        string info = invoiceSummary.BuyerID + "," + invoiceSummary.TotalInvoices + "," + invoiceSummary.Cost + ",\n";
                        //Append it to the file
                        File.AppendAllText(filePath, info);
                        success = true;
                    }
                }
                //Display a status message based on success / failure of the operation
                if (success)
                {
                    sbiStatus.Content = "Invoice summaries exported to " + filePath;
                }
                else
                {
                    sbiStatus.Content = "Failed to export invoice summaries";
                }
            }
            catch (Exception ex)
            {
                //Log any exceptions
                Logger.WriteLog(ex.Message);
            }

            
        }
    }
}
