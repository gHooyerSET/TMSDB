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

namespace BuyerScreen
{
    /** 
     * <summary>
     * Interaction logic for MainWindow.xaml
     * </summary>
     * 
     * <details>
     * Buyer Main Window interaction logic for TMS
     * </details>
    */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        /**
         * <summary>
         * Logs the Buyer out of the system
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The buyer logs out of the system, and returns to the login window;
         * This is the same for selecting the close window option in the upper corner.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void logout_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Gets a new customer from the contract marketplace
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The buyer can pull a new customer from the contract marketplace and save into the database
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void newCustomer_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Displays a list of customers currently in the database
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The buyer can display a list of all current customers and their information
         * that are in the database.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void reviewCustomers_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Creates a new Order from the Marketplace Requests
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * Requests a new Order from the Marketplace to be fullfiled for the customer,
         * Opens a dialog window for the Buyer to select relevant Cities for the Order.
         * The relevant Cities will provide available Carriers to perform Order.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void createOrder_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Generates an Invoice for completed Orders
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * A dialog window pops up where the Buyer reviews the completed Orders 
         * and processess them for Invoice generation.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void completedOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
