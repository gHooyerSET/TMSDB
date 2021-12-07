/*
* FILE : RouteWindow.xaml.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-12-07
* DESCRIPTION : Displays current routes to admin
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

namespace TMS_Service.Admin
{
    /// <summary>
    /// Interaction logic for RouteWindow.xaml
    /// </summary>
    public partial class RouteWindow : Window
    {
        TMSDB tmsdb;
        List<Route> routeList = new List<Route>();

        public RouteWindow()
        {
            InitializeComponent();
            tmsdb = new TMSDB();
            FillRouteGrid();
        }

        /// <summary>
        /// Goes back to admin main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Fills the datagrid with Carrier information
        /// </summary>
        private void FillRouteGrid()
        {
            dgInfoRoute.ItemsSource = null;

            string routeString = tmsdb.GetRoutes();
            string[] routeStringArray = routeString.Split(',');

            for (int i = 0; i < routeStringArray.Length;)
            {
                Route route = new Route(Int32.Parse(routeStringArray[i]), Int32.Parse(routeStringArray[i+1]), routeStringArray[i+2], routeStringArray[i+3],
                    routeStringArray[i+4], routeStringArray[i+5], float.Parse(routeStringArray[i+6]));
                routeList.Add(route);
                i += 7;
            }
            dgInfoRoute.ItemsSource = routeList;
        }
    }
}
