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
    /// Interaction logic for LogDirectory.xaml
    /// </summary>
    public partial class LogDirectory : Window
    {
        public LogDirectory()
        {
            InitializeComponent();
        }




        /**
         * <summary>
         * Deletes the log file directory specified in the directoryPath textbox
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The admin can delete the directory for the log files by specifying the path information in the
         * directoryPath textbox.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }



        /**
         * <summary>
         * Create the log file directory specified in the directoryPath textbox
         * </summary>
         * 
         * <param name="e">The state of the click object</param>
         * <param name="sender">The object that is being passed</param>
         * 
         * <details>
         * The admin can create the directory for the log files by specifying the path information in the
         * directoryPath textbox.
         * </details>
         * 
         * <returns>
         * No Return (void)
         * </returns>
         */
        private void create_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
