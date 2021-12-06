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
    /// Interaction logic for FindCarrier.xaml
    /// </summary>
    public partial class FindCarrier : Window
    {
        public FindCarrier()
        {
            InitializeComponent();
        }




        /**
         * <summary>
         * Delete the Carrier entered in the carrierName Field
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The admin can delete the Carrier specified in the carrierName,
         * if the carrier does not exist an error message is displayed saying so.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void deleteCarrier_Click(object sender, RoutedEventArgs e)
        {

        }




        /**
         * <summary>
         * Access the Carrier data for the carrier entered in the carrierName field.
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The admin can update the Carrier specified in the carrierName,
         * if the carrier does not exist an error message is displayed saying so.
         * The admin is than brought to another screen with the data fields to edit.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void updateCarrier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
