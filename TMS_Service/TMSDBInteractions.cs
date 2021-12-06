/*File            : Program.cs
 * Project        : TMS System
 * Programmmer    : Waleed Ahmed
 * First Version  : 2021-12-04
 * Description    : Receives a command from the UI and parses it to send
 *                  to the apporpriate TMSDatabase method
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSDatabase;

namespace TMSProject
{
    class Program
    {
        // create an instance of the TMSDB
        TMSDB db = new TMSDB();

        /// <summary>
        /// Parses the command to determine what actions needs to be performed and calls the appropriate method
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>   
        /// <returns><b>string</b> - information that was asked for</returns> 
        public string Command(string command)
        {
            string commandType = "";
            string info = "";
            // split the string 
            if (command.Contains(','))
            {
                // commandtype determines which method to call
                commandType = command.Substring(0, command.IndexOf(','));
                // all the parameters of the command
                info = command.Substring(command.IndexOf(',') + 1);
            }
            else
            {
                // store command in command type if no comma is found
                commandType = command;
            }
            
            // call the appropraite method depending on the request
            if (commandType == "login")
            {
                bool success = LoginCommand(info);
                
                if (success)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else if (commandType == "invoice")
            {
                string invoice = "";

                // call the invoice method
                invoice = InvoiceCommand(info);

                return invoice;
            }
            else if (commandType == "order")
            {
                string order = "";

                // call the order method
                order = OrderCommand(info);

                return order;
            }
            else if (commandType == "carrier")
            {
                string carrier = "";

                if (info == "")
                {
                    carrier = db.GetCarriers();
                }
                else
                {
                    carrier = db.GetCarriers(info);
                }

                return carrier;
            }

            else if (commandType == "trip")
            {
                string trip = "";
                // number to store the tripID 
                int tripID = 0;

                // convert tripID to int
                bool success = int.TryParse(info, out tripID);

                if (success)
                {
                    trip = db.GetTrip(tripID);
                }
                return trip;
                
            }

            else if (commandType == "trips")
            {
                string trips = "";

                if (info == "")
                {
                    trips = db.GetTrips();
                }
                else
                {
                    // number to store the routeID 
                    int routeID = 0;

                    // convert tripID to int
                    bool success = int.TryParse(info, out routeID);

                    if (success)
                    {
                        trips = db.GetTrips(routeID);
                    }
                }

                return trips;
            }
            else if (commandType == "createTrip")
            {
                bool success = CreateTripCommand(info);

                if (success)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else if (commandType == "createOrder")
            {
                bool success = CreateOrderCommand(info);

                if (success)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else if (commandType == "createRoute")
            {
                bool success = CreateRouteCommand(info);

                if (success)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else if (commandType == "createInvoice")
            {
                bool success = CreateInvoiceCommand(info);

                if (success)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else if (commandType == "query")
            {
                RunQueryCommand(info);
                return "TRUE"; 
            }
            else
            {
                return "Invalid command";
            }
        }


        /// <summary>
        /// Parses the login command received from the UI and sends it to the TMSDB class to verify
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>bool</b> - <i>true</i> if login was successful, <i>false</i> otherwise.</returns>        
        public bool LoginCommand(string command)
        {
            // split the string into string array
            string[] substring = command.Split(',');

            // call the login function and store the result in success
            bool success = db.Login(substring[0], substring[1], out substring[3]);

            if (success == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all invoices based on CustomerID or InvoiceID
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>string</b> - a comma-seperated <b>string</b> of all of the invoices related to a customer or invoiceID.       
        public string InvoiceCommand(string command)
        {
            // string to store the invoice
            string invoice = "";

            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 2
            if (substring.Length != 2)
            {
                return "FALSE";
            }

            // number to store the invoiceID 
            int invoiceID = 0;

            // determine if the second argument is a customerID or invoiceID
            bool success = int.TryParse(substring[1], out invoiceID);

            // if successful call the invoice function otherwise call customer function
            if (success)
            {
                invoice = db.GetInvoice(invoiceID);
            }
            else 
            {
                invoice = db.GetInvoices(substring[1]);
            }

            return invoice;
        }

        /// <summary>
        /// Retrieves all orders based on CustomerID or OrderID
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>string</b> - a comma-seperated <b>string</b> of all of the orders related to a customer or invoiceID.       
        public string OrderCommand(string command)
        {
            // string to store the invoice
            string order = "";

            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 2
            if (substring.Length != 2)
            {
                return "FALSE";
            }

            // number to store the OrderID 
            int orderID = 0;

            // determine if the second argument is a customerID or orderID
            bool success = int.TryParse(substring[1], out orderID);

            // if successful call the invoice function otherwise call customer function
            if (success)
            {
                order = db.GetOrder(orderID);
            }
            else
            {
                order = db.GetOrders(substring[1]);
            }

            return order;
        }

        /// <summary>
        /// Parses the command received and calls the CreateTrip method to create a trip
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>bool</b> - <i>true</i> if trip is created, <i>false</i> otherwise.</returns>
        public bool CreateTripCommand(string command)
        {
            bool createTrip = false;

            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 5
            if (substring.Length != 5)
            {
                return createTrip;
            }

            // number to store the OrderID 
            int routeID = 0;

            // determine if the second argument is a customerID or orderID
            bool success = int.TryParse(substring[1], out routeID);

            // call the function to create the trip
            createTrip = db.CreateTrip(substring[0], routeID, substring[2], substring[3], substring[4]);

            // return whether the trip was created
            return createTrip;

        }

        /// <summary>
        /// Parses the command received and calls the CreateOrder method to create an Order
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>bool</b> - <i>true</i> if order is created, <i>false</i> otherwise.</returns>
        public bool CreateOrderCommand(string command)
        {
            bool createOrder = false;
            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 4
            if (substring.Length != 4)
            {
                return createOrder;
            }

            // call the function to create the order
            createOrder = db.CreateOrder(substring[0], substring[1], substring[2], DateTime.Parse(substring[3]));

            // return whether the order was created
            return createOrder;
        }

        /// <summary>
        /// Parses the command received and calls the CreateRoute method to create a Route
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>bool</b> - <i>true</i> if route is created, <i>false</i> otherwise.</returns>
        public bool CreateRouteCommand(string command)
        {
            bool createRoute = false;

            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 2
            if (substring.Length != 2)
            {
                return createRoute;
            }

            // number to store the OrderID 
            int plannerID = 0;

            // convert plannerID into an int
            bool success = int.TryParse(substring[0], out plannerID);

            if (success)
            {
                // call the function to create the order
                createRoute = db.CreateRoute(plannerID, substring[1]);
            }

            // return whether the order was created
            return createRoute;
        }

        /// <summary>
        /// Parses the command received and calls the CreateInvoice method to create an Invoice
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        /// <returns><b>bool</b> - <i>true</i> if route is created, <i>false</i> otherwise.</returns>
        public bool CreateInvoiceCommand(string command)
        {
            bool createInvoice = false;

            // split the string into string array
            string[] substring = command.Split(',');

            // return false if the number of arguments is not exactly 2
            if (substring.Length != 2)
            {
                return createInvoice;
            }

            // number to store the OrderID and cost
            int orderID = 0;
            float cost = 0;

            // convert orderID and cost to an int and float
            bool success = int.TryParse(substring[0], out orderID);
            bool fSuccess = float.TryParse(substring[1], out cost);

            if (success && fSuccess)
            {
                // call the function to create the order
                createInvoice = db.CreateInvoice(orderID, cost);
            }

            // return whether the order was created
            return createInvoice;
        }

        /// <summary>
        /// Parses the command received and calls the RunQuery to run the query
        /// </summary>
        /// <param name="command">Comma seperated string containing all required fields.</param>
        public void RunQueryCommand(string command)
        {
            // run the query
            db.RunQuery(command);
        }
    }
}
