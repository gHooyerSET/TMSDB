/**
 * <file>MainWindow.xaml.cs(Administrator)</file>
 * 
 * <summary>
 * Contains the interaction logic for the system Administrator.
 * </summary>
 * 
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


/**
 * <namespace>
 * AdminScreen
 * </namespace>
 * 
 * <summary>
 * Contains the methods available to the Admistrator
 * </summary>
 * 
 * <details>
 * Allows for the Admin to interact with the program by:
 *  1. Logging Out
 *  2. Changing Network Communications
 *  3. Alter Log File Directory and remove previous ones.
 *  4. Adjust Carrier Rates/Fees from database
 *  5. Display All Cariers from database
 *  6. Add a New Carrier to the database
 *  7. Update a Carrier in the database
 *  8. Delete a Carrier from the database
 * </details>
*/
namespace AdminScreen
{
    /**
     * <class>
     * MainWindow
     * </class>
     * 
     * <summary>
     * The MainWindow inherits the Window class from System.Windows.
     * It is the interaction logic for the Administrator contained in [MainWindow.xaml] (<ref>MainWindow.xaml.cs(Administrator)</ref>
     * </summary>
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        /**
         * <fn>
         * private void logout_Click(object sender, RoutedEventArg e)
         * </fn>
         * 
         * <summary>
         * Logs the user out of the program; ask for confirmation.
         * Note: Same as 'close' in upper corner.
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
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
         * Allows the administrator to change/delete the directory for the log files.
         * </summary>
         * 
         * <details>
         * On click another window displays with the current and former log file directories,
         * and allows for the admin to input a new directory and/or delete former directory.
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void logDirectory_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Allows the administrator to change the IP Address and Port Number to use for connection.
         * </summary>
         * 
         * <details>
         * On click another window displays with the fields to input the new IP Address and Port Number.
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void connection_Click(object sender, RoutedEventArgs e)
        {

        }



        /**
         * <summary>
         * Allows the administrator to adjust the rates/fees of a Carrier
         * </summary>
         * 
         * <details>
         * On click another window displays with all the Carrier rates/fees and allows
         * for the admin to adjust the values displayed. Confirmation of change is required.
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void rateFeeTable_Click(object sender, RoutedEventArgs e)
        {

        }



        /**
         * <summary>
         * Allows the administrator to display all Carrier information
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void allCarriers_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Allows the administrator to add a new Carrier to the database.
         * </summary>
         * 
         * <details>
         * On click another window displays with all the fields that are 
         * required to be filled out to add the new Carrier to the database.
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void addCarrier_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Allows the administrator to update the information of a Carrier in the database.
         * </summary>
         * 
         * <details>
         * On click another window displays asking for the name/ID of the Carrier to update.
         * If Carrier exists the Carrier information is displayed for editing.
         * Confirmation of the changes are required. 
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void updateCarrier_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Allows the administrator to delete a Carrier from the database.
         * </summary>
         * 
         * <details>
         * On click another window displays asking for the name/ID of the Carrier to delete.
         * If Carrier exists a confirmation of deletion is required. 
         * </details>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void deleteCarrier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
