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

namespace AdminScreen
{
    /// <summary>
    /// Interaction logic for AddCarrier.xaml
    /// </summary>
    public partial class AddCarrier : Window
    {
        public AddCarrier()
        {
            InitializeComponent();
        }



        /**
         * <summary>
         * Save the new Carrier information into the database
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The admin can save carrier into the database by inputing:
         * Carrier Name, error message displayed if blank
         * Selecting Distribution Cities and input coresponding data, If the fields are left empty after selecting city an error message is displayed.
         * This method a;so does the update
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void createCarrier_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Closes the window for adding new carrier
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * Closes the current window and returns the admin to the main AdminScreen
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
