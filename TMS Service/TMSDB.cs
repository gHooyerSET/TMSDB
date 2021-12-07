/*
 * FILE             : TMSDB.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Provides an easy-to-use interface for the database.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace TMSProject
{
    /*
     * TITLE        : Creating a Connector/Net Connection String
     * AUTHOR       : MySQL Documentation
     * DATE         : N/A
     * VERSION      : N/A
     * AVAILABLE    : https://dev.mysql.com/doc/connector-net/en/connector-net-connections-string.html
     */
    /*
     * TITLE        : MySQL Commands in C#
     * AUTHOR       : Boy Karton, Edited by Aleksandar
     * DATE         : 07-05-2015
     * VERSION      : N/A
     * AVAILABLE    : https://stackoverflow.com/questions/16167924/c-sharp-with-mysql-insert-parameters
     */
    /*
     * TITLE        : Retrieving from a mysql database
     * AUTHOR       : Dave on C#
     * DATE         : 2021-11-18
     * VERSION      : N/A
     * AVAILABLE    : https://www.daveoncsharp.com/2009/11/retrieving-data-from-a-mysql-database/
     */
    /*
     * TITLE        : Sample DOxygen Commenting
     * AUTHOR       : SET
     * DATE         : 2021-11-18
     * VERSION      : N/A
     * AVAILABLE    : https://conestoga.desire2learn.com/d2l/common/dialogs/quickLink/quickLink.d2l?ou=62087&type=coursefile&fileId=Standards/Coding/SampleDOxygen.zip
     */

    
    /// <summary>
    /// The TMSDatabase class covers all data-retrieval and connection to the TMS MySQL database.
    /// </summary>
    public class TMSDB
    {
        //Static constants
        //Carrier Table constants
        private static readonly int Carrier_IDIndex = 0;
        private static readonly int Carrier_CityIndex = 1;
        private static readonly int Carrier_FTLAIndex = 2;
        private static readonly int Carrier_LTLAIndex = 3;
        private static readonly int Carrier_FTLRateIndex = 4;
        private static readonly int Carrier_LTLRateIndex = 5;
        private static readonly int Carrier_ReefChargeIndex = 6;
        //Invoice Table constants
        private static readonly int Invoice_IDIndex = 0;
        private static readonly int Invoice_CustomerIDIndex = 1;
        private static readonly int Invoice_OrderIDIndex = 2;
        private static readonly int Invoice_CostIndex = 3;
        //Order Table constants
        private static readonly int Order_IDIndex = 0;
        private static readonly int Order_CustomerIDIndex = 1;
        private static readonly int Order_StartCityIndex = 2;
        private static readonly int Order_EndCityIndex = 3;
        private static readonly int Order_StatusIndex = 4;
        private static readonly int Order_OrderDateIndex = 5;
        //Route table constants
        private static readonly int Route_IDIndex = 0;
        private static readonly int Route_OrderIDIndex = 1;
        private static readonly int Route_PlannerIDIndex = 2;
        private static readonly int Route_RouteStatusIndex = 3;
        private static readonly int Route_StartCityIndex = 4;
        private static readonly int Route_EndCityIndex = 5;
        private static readonly int Route_CostIndex = 6;
        //Trip Table constants
        private static readonly int Trip_IDIndex = 0;
        private static readonly int Trip_CarrierIDIndex = 1;
        private static readonly int Trip_RouteIDIndex = 2;
        private static readonly int Trip_StartCityIndex = 3;
        private static readonly int Trip_EndCityIndex = 4;
        private static readonly int Trip_TypeIndex = 5;
        private static readonly int Trip_RateIndex = 6;
        //User Table Constants
        private static readonly int User_IDIndex = 0;
        private static readonly int User_PasswordIndex = 1;
        private static readonly int User_RoleIndex = 2;


        //Global variables
        private MySqlConnection connection = new MySqlConnection("server=127.0.0.1;uid=" + ConfigurationManager.AppSettings.Get("dbUID") + ";pwd=" + ConfigurationManager.AppSettings.Get("dbPWD") + ";database=group15-tms");


        // ---------------- METHODS ---------------
        /// <summary>
        /// Grabs a users information and verifies that login information is valid.
        /// </summary>
        /// <param name="userName">This is the username of the account.</param>
        /// <param name="password">This is the password, as plain-text.</param>
        /// <param name="role"><b>OUT</b> This string is filled with the role of the user. Should be empty when passed.</param>
        /// <returns><b>bool</b> - <i>true</i> on successful login, <i>false</i> otherwise.</returns>        
        public bool Login(string userName, string password, out string role)
        {
            //Prepare the return value
            bool retValue = false;
            //Set 'role' to null initially
            role = null;

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;
            //These strings will be filled with data retrieved from the DB
            string dbUserName = null;
            string dbPassword = null;
            string dbRole = null;

            try
            {
                //Open the database connection
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                //Create the command with the correct text
                sqlCommand.CommandText = "SELECT * FROM user WHERE UserID=@UserName AND Password=@Password";
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                //Execute the command
                sqlReader = sqlCommand.ExecuteReader();

                //Read the information
                while (sqlReader.Read())
                {
                    dbUserName = (string)sqlReader.GetValue(User_IDIndex);
                    dbPassword = (string)sqlReader.GetValue(User_PasswordIndex);
                    dbRole = (string)sqlReader.GetValue(User_RoleIndex);
                }
                //If the information could be found (i.e. the strings aren't null), login was successful
                if (dbUserName != null && dbPassword != null && dbRole != null)
                {
                    retValue = true;
                    role = dbRole;
                }

            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                //If the connection is still open, make sure to close it!
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return retValue;
        }

        /// <summary>
        /// Returns a comma-seperated string of all of the invoices related to a particular customerID
        /// </summary>
        /// <param name="customerID">This is the username for a particular customer. Most often, this will be taken from the 
        /// 'UserID' field in the 'users' table.</param>
        /// <returns><b>string</b> - a comma-seperated <b>string</b> of all of the invoices related to a customer.
        /// Format: <i>InvoiceID , CustomerID , OrderID , Cost</i></returns>
        public string GetInvoices(string customerID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM invoice WHERE CustomerID=@CustomerID";
                sqlCommand.Parameters.AddWithValue("@CustomerID", customerID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the Invoice ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_IDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");                    
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Invoice_CustomerIDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the Order ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_OrderIDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the cost
                    stringBuilder.Append(sqlReader.GetFloat(Invoice_CostIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if(stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }                
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return stringBuilder.ToString();
        }


        /// <summary>
        /// Gets invoices that match the provided OrderID
        /// </summary>
        /// <param name="OrderID">This is the OrderID associated with the invoice</param>
        /// <returns><b>string</b> - a comma-seperated <b>string</b> of all of the invoices related to a customer.
        /// Format: <i>InvoiceID , CustomerID , OrderID , Cost</i></returns>
        public string GetInvoices(int OrderID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM invoice WHERE OrderID=@OrderID";
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the Invoice ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_IDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Invoice_CustomerIDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the Order ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_OrderIDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the cost
                    stringBuilder.Append(sqlReader.GetFloat(Invoice_CostIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns a comma-seperated string of data related to a particular invoice.
        /// </summary>
        /// <param name="InvoiceID">The ID of the specific invoice that is being looked up.</param>
        /// <returns><b>string</b> - comma-seperated values for all of the fields in the Invoice table related to the invoice that was looked up.</returns>
        public string GetInvoice(int InvoiceID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM invoice WHERE InvoiceID=@InvoiceID";
                sqlCommand.Parameters.AddWithValue("@InvoiceID", InvoiceID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the Invoice ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_IDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Invoice_CustomerIDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the Order ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Invoice_OrderIDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the cost
                    stringBuilder.Append(sqlReader.GetFloat(Invoice_CostIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return stringBuilder.ToString();
        }


        /// <summary>
        /// Returns a comma-seperated string of buyers.
        /// </summary>
        /// <returns><b>string</b> - comma-seperated values for all users that are buyers.</returns>
        public string GetBuyers()
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT UserID FROM user WHERE Role=@Role";
                sqlCommand.Parameters.AddWithValue("@Role", "Buyer");

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(0));
                    //Then append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets all orders associated with a customerID (i.e. username for an account)
        /// </summary>
        /// <param name="customerID">The username of the customer we wish to get the orders of.</param>
        /// <returns><b>string</b> of comma-seperated data representing the information regarding a set of orders.
        /// Format: <i> OrderID , CustomerID , StartCity , EndCity , Status , OrderDate </i></returns>
        public string GetOrders(string customerID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.order WHERE CustomerID=@CustomerID";
                sqlCommand.Parameters.AddWithValue("@CustomerID", customerID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the Order ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Order_IDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_CustomerIDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the StartCity to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_StartCityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the EndCity to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_EndCityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the Status to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_StatusIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the OrderDate to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetMySqlDateTime(Order_OrderDateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if(stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }                
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns a specific order when passed a valid orderID.
        /// </summary>
        /// <param name="orderID">The ID of the order that is being looked up.</param>
        /// <returns><b>string</b> of comma-seperated data representing the information regarding an order.
        /// Format: <i> OrderID , CustomerID , StartCity , EndCity , Status , OrderDate </i></returns>
        public string GetOrder(int orderID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.order WHERE OrderID=@OrderID";
                sqlCommand.Parameters.AddWithValue("@OrderID", orderID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the Order ID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetInt32(Order_IDIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the CustomerID to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_CustomerIDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the StartCity to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_StartCityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the EndCity to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_EndCityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the Status to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetString(Order_StatusIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the OrderDate to the StringBuilder string
                    stringBuilder.Append(sqlReader.GetMySqlDateTime(Order_OrderDateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder != null)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns a string of all of the carriers and their associated data in a comma-seperated format.
        /// </summary>
        /// <returns><b>string</b> - comma-seperated string of all of the values associated with the carrier.</returns>
        public string GetCarriers()
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.carrier";
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the CarrierID
                    stringBuilder.Append(sqlReader.GetString(Carrier_IDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the City
                    stringBuilder.Append(sqlReader.GetString(Carrier_CityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the FTLA
                    stringBuilder.Append(sqlReader.GetInt32(Carrier_FTLAIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the LTLA
                    stringBuilder.Append(sqlReader.GetInt32(Carrier_LTLAIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the FTLRate
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_FTLRateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the LTLRate
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_LTLRateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the ReefCharge
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_ReefChargeIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();

        }
       

        /// <summary>
        /// Returns a string of all of the carriers in a city and their associated data in a comma-seperated format.
        /// </summary>
        /// <param name="CityName">The city to search by.</param>
        /// <returns><b>string</b> - The carriers in a particular city. 
        /// Format: <i>CarrierID , City , FTLA , LTLA , FTLRate , LTLRate , ReefCharge</i></returns>
        public string GetCarriers(string CityName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.carrier WHERE City=@City";
                sqlCommand.Parameters.AddWithValue("@City", CityName);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    //Append the CarrierID
                    stringBuilder.Append(sqlReader.GetString(Carrier_IDIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the City
                    stringBuilder.Append(sqlReader.GetString(Carrier_CityIndex));
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the FTLA
                    stringBuilder.Append(sqlReader.GetInt32(Carrier_FTLAIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the LTLA
                    stringBuilder.Append(sqlReader.GetInt32(Carrier_LTLAIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the FTLRate
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_FTLRateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the LTLRate
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_LTLRateIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                    //Append the ReefCharge
                    stringBuilder.Append(sqlReader.GetFloat(Carrier_ReefChargeIndex).ToString());
                    //Append the comma
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();

        }

        /// <summary>
        /// Returns a comma-seperated string of all route data from the 'route' table.
        /// </summary>
        /// <returns><b>string</b> - a comma-seperated string of all route data from the 'route' table.</returns>
        public string GetRoutes()
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.route";
                sqlReader = sqlCommand.ExecuteReader();

                while(sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Route_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Route_OrderIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_PlannerIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_RouteStatusIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Route_CostIndex));
                    stringBuilder.Append(",");
                }
                if(stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns all routes and associated data with a given OrderID
        /// </summary>
        /// <param name="OrderID">The ID of the order that you wish to find Routes for.</param>
        /// <returns><b>string</b> - a comma-seperated string of all routes associated with the given ID and their data.
        /// Format: <i>RouteID , OrderID , PlannerID , RouteStatus , StartCity , EndCity , Cost</i>/></returns>
        public string GetRoutes(int OrderID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.route WHERE OrderID=@OrderID";
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Route_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Route_OrderIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_PlannerIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_RouteStatusIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Route_CostIndex));
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the information for a specific route.
        /// </summary>
        /// <param name="RouteID">The ID of the route that you wish to retrieve information about.</param>
        /// <returns><b>string</b> -  a comma-seperated string of all fields related to the route.</returns>
        public string GetRoute(int RouteID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.route WHERE RouteID=@RouteID";
                sqlCommand.Parameters.AddWithValue("@RouteID", RouteID);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Route_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Route_OrderIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_PlannerIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_RouteStatusIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Route_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Route_CostIndex));
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns information on all trips.
        /// </summary>
        /// <returns><b>string</b> - Format : <i>TripID , CarrierID , RouteID , StartCity , EndCity , Type , Rate</i></returns>
        public string GetTrips()
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.trip";
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Trip_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_CarrierIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Trip_RouteIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_TypeIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Trip_RateIndex));
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns information associated with a particular trip.
        /// </summary>
        /// <param name="TripID">The ID of the trip you wish to retrieve.</param>
        /// <returns><b>string</b> - Format : <i>TripID , CarrierID , RouteID , StartCity , EndCity , Type , Rate</i></returns>
        public string GetTrip(int TripID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.trip WHERE TripID=@TripID";
                sqlCommand.Parameters.AddWithValue("@TripID",TripID);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Trip_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_CarrierIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Trip_RouteIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_TypeIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Trip_RateIndex));
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns information on all trips associated with a particular route.
        /// </summary>
        /// <param name="RouteID">The route the trips are associated with.</param>
        /// <returns><b>string</b> - Format : <i>TripID , CarrierID , RouteID , StartCity , EndCity , Type , Rate</i></returns>
        public string GetTrips(int RouteID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.trip WHERE RouteID=@RouteID";
                sqlCommand.Parameters.AddWithValue("@RouteID",RouteID);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    stringBuilder.Append(sqlReader.GetInt32(Trip_IDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_CarrierIDIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetInt32(Trip_RouteIDIndex).ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_StartCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_EndCityIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetString(Trip_TypeIndex));
                    stringBuilder.Append(",");
                    stringBuilder.Append(sqlReader.GetFloat(Trip_RateIndex));
                    stringBuilder.Append(",");
                }
                if (stringBuilder.Length > 0)
                {
                    //Remove the last comma from the string
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Creates a new order entry and inserts it into the 'order' table in the database.
        /// </summary>
        /// <param name="CustomerID"><b>string</b> - The username of the customer.</param>
        /// <param name="StartCity"><b>string</b> - The starting city.</param>
        /// <param name="EndCity"><b>string</b> - The destination city.</param>
        /// <param name="OrderDate"><b>DateTime</b> - The date the order was placed.</param>
        /// <returns><b>bool</b> - <i>true</i> on successful insertion, <i>false</i> otherwise.</returns>
        public bool CreateOrder(string CustomerID, string StartCity, string EndCity, DateTime OrderDate)
        {
            bool retValue = false;
            MySqlCommand sqlCommand = connection.CreateCommand();

            try
            {
                //Open the connection to the database
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                //Set the command text
                sqlCommand.CommandText = "INSERT INTO `group15-tms`.order (CustomerID,StartCity,EndCity,Status,OrderDate) ";
                sqlCommand.CommandText += "VALUES (@CustomerID,@StartCity,@EndCity,@Status,@OrderDate)";
                //Set the parameters of the command
                sqlCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                sqlCommand.Parameters.AddWithValue("@StartCity", StartCity);
                sqlCommand.Parameters.AddWithValue("@EndCity", EndCity);
                sqlCommand.Parameters.AddWithValue("@Status", "Incomplete");
                sqlCommand.Parameters.AddWithValue("@OrderDate", OrderDate);
                //Run the command
                sqlCommand.ExecuteNonQuery();
                //Set the return value to 'true'
                retValue = true;

            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return retValue;
        }

        

        /// <summary>
        /// Creates a route and inserts it into the database.
        /// </summary>
        /// <param name="OrderID">The order the route will be associated with.</param>
        /// <param name="PlannerID">The username of the planner that added the route to the trip.</param>
        /// <returns><b>bool</b> - <i>true</i> if insertion was successful, <i>false</i> otherwise.</returns>
        public bool CreateRoute(int OrderID, string PlannerID)
        {
            bool retValue = false;

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader = null;

            string orderStartCity = null;
            string orderEndCity = null;

            try
            {
                //Open the connection to the database
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                //First we need to get some information from the order
                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.order WHERE OrderID=@OrderID";
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);

                sqlReader = sqlCommand.ExecuteReader();

                while(sqlReader.Read())
                {
                    orderStartCity = sqlReader.GetString(2);
                    orderEndCity = sqlReader.GetString(3);
                }
                //Close the reader
                sqlReader.Close();
                //Clear the parameters
                sqlCommand.Parameters.Clear();

                //Now we can insert the information into the database
                //Set the command text
                sqlCommand.CommandText = "INSERT INTO `group15-tms`.route (OrderID,PlannerID,RouteStatus,StartCity,EndCity,Cost) ";
                sqlCommand.CommandText += "VALUES (@OrderID,@PlannerID,@RouteStatus,@StartCity,@EndCity,@Cost)";
                //Set the parameters of the command               
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);
                sqlCommand.Parameters.AddWithValue("@PlannerID", PlannerID);
                sqlCommand.Parameters.AddWithValue("@RouteStatus", "Incomplete");
                sqlCommand.Parameters.AddWithValue("@StartCity", orderStartCity);
                sqlCommand.Parameters.AddWithValue("@EndCity", orderEndCity);
                sqlCommand.Parameters.AddWithValue("@Cost", 0.00);
                //Run the command
                sqlCommand.ExecuteNonQuery();
                //Set the return value to 'true'
                retValue = true;
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return retValue;
        }

        /// <summary>
        /// Creates a trip to be attached to a route.
        /// </summary>
        /// <param name="CarrierID">The name of the carrier company</param>
        /// <param name="RouteID">The ID of the route that the trip will be attached to.</param>
        /// <param name="Type">The shipping type. Should be either "FTL" or "LTL"</param>
        /// <param name="StartCity">The starting city of the trip.</param>
        /// <param name="EndCity">The ending city of the trip.</param>
        /// <returns></returns>
        public bool CreateTrip(string CarrierID, int RouteID, string Type, string StartCity, string EndCity)
        {
            bool retValue = false;

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader = null;
            float rate = 0;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                // we need to get some information from the carrier
                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.carrier WHERE CarrierID=@CarrierID and City=@StartCity;";
                sqlCommand.Parameters.AddWithValue("@CarrierID", CarrierID);
                sqlCommand.Parameters.AddWithValue("@StartCity", StartCity);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    if(Type.Equals("FTL"))//Based on the shipping type, we can get the correct rate
                    {
                        rate = sqlReader.GetFloat(Carrier_FTLRateIndex);
                    }
                    else if (Type.Equals("LTL"))
                    {
                        rate = sqlReader.GetFloat(Carrier_LTLRateIndex);
                    }
                    else
                    {
                        throw new Exception("CreateTrip Parameter Error - Invalid shipping type passed as parameter.");
                    }
                }
                //Close the reader
                sqlReader.Close();
                //Clear the parameters
                sqlCommand.Parameters.Clear();
                //Now we can insert the information into the database
                //Set the command text
                sqlCommand.CommandText = "INSERT INTO `group15-tms`.trip (CarrierID,RouteID,StartCity,EndCity,Type,Rate) ";
                sqlCommand.CommandText += "VALUES (@CarrierID,@RouteID,@StartCity,@EndCity,@Type,@Rate)";

                sqlCommand.Parameters.AddWithValue("@CarrierID", CarrierID);
                sqlCommand.Parameters.AddWithValue("@RouteID", RouteID);
                sqlCommand.Parameters.AddWithValue("@StartCity", StartCity);
                sqlCommand.Parameters.AddWithValue("@EndCity", EndCity);
                sqlCommand.Parameters.AddWithValue("@Type", Type);
                sqlCommand.Parameters.AddWithValue("@Rate", rate);
                //Run the command
                sqlCommand.ExecuteNonQuery();
                //Set the return value to 'true'
                retValue = true;

            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return retValue;
        }

        /// <summary>
        /// Creates a trip to be attached to a route.
        /// </summary>
        /// <param name="CarrierID">The name of the carrier company</param>
        /// <param name="RouteID">The ID of the route that the trip will be attached to.</param>
        /// <param name="Type">The shipping type. Should be either "FTL" or "LTL"</param>
        /// <param name="StartCity">The starting city of the trip.</param>
        /// <param name="EndCity">The ending city of the trip.</param>
        /// <returns></returns>
        public bool CreateInvoice(int OrderID, float Cost)
        {
            bool retValue = false;

            MySqlCommand sqlCommand = connection.CreateCommand();
            MySqlDataReader sqlReader = null;
            string CustomerID  = string.Empty;

            try
            {
                connection.Open();

                Logger.WriteLog("TMSDB SQL Connection Successful.");

                // we need to get some information from the order
                sqlCommand.CommandText = "SELECT * FROM `group15-tms`.order WHERE OrderID=@OrderID";
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);

                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    CustomerID = sqlReader.GetString(Order_CustomerIDIndex);
                }
                //Close the reader
                sqlReader.Close();

                //Clear sql Parameters
                sqlCommand.Parameters.Clear();

                sqlCommand.CommandText = "INSERT INTO `group15-tms`.invoice (CustomerID,OrderID,Cost) ";
                sqlCommand.CommandText += "VALUES(@CustomerID,@OrderID,@Cost)";

                sqlCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);
                sqlCommand.Parameters.AddWithValue("@Cost", Cost);

                sqlCommand.ExecuteNonQuery();

                retValue = true;

            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return retValue;
        }

        /// <summary>
        /// Runs a query to the database provided by the user.
        /// </summary>
        /// <param name="sqlCommandText">Text representing the desired SQL query.</param>
        public void RunQuery(string sqlCommandText)
        {
            MySqlCommand sqlCommand = connection.CreateCommand();
            try
            {
                connection.Open();

                if (!sqlCommandText.Contains("DROP"))
                {
                    sqlCommand.CommandText = sqlCommandText;
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    throw new AccessViolationException("RunQuery Error - CANNOT ATTEMP TO DROP TABLES");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

    }
}
