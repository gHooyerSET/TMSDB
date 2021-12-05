/*
* FILE : CarrierUpdateCommunication.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-11-25
* DESCRIPTION : Class to provide information to the Carrier Update System
*/
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject
{
    /// <summary>
    /// Class to get information from the Carrier Update System
    /// </summary>
    public class CarrierUpdateSystemCommunication
    {
        // Credential constants 
        private static readonly string _uid = "?";
        private static readonly string _password = "?";
        private static readonly string _address = "?";
        private static readonly string _port = "?";
        private static readonly string _db = "?";
        private static readonly string _connectionString = "SERVER=" + _address + "; PORT =" + _port + ";" + "DATABASE=" + _db + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";
        private MySqlConnection connectionCUS = new MySqlConnection(_connectionString);

        /// <summary>
        /// Tests an attempt to connect to the Carrier Update Marketplace with the provided credentials
        /// </summary>
        /// <returns><b>bool</b> : <b>true</b> if login success, <b>false</b> if failed.</returns>   
        public bool ConnectTestCUS()
        {
            try
            {
                connectionCUS.Open();
            }
            catch (MySqlException ex)
            {
                //Log("Invalid credentials");
                return false;//******Placeholder
            }
            finally
            {
                //Close connection 
            }
            return true;//**********Placeholder
        }

        /// <summary>
        /// Gets status of an Order on a Carrier truck
        /// </summary>
        /// <param name="orderID"> - <b>int</b> -The id of the order to get an update on.</param>
        /// <returns><b>string</b> : Current tracking information</returns>     
        public string GetOrderUpdate(int orderID)
        {
            MySqlCommand cmd = connectionCUS.CreateCommand();
            MySqlDataReader read;
            StringBuilder orderUpdateGet = new StringBuilder();

            try
            {
                connectionCUS.Open();

                cmd.CommandText = "SELECT * FROM Orders;"; //******** Table and DB Details unkown at this time*******

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    orderUpdateGet.Append(read.GetString(0));
                }
            }
            catch (MySqlException ex)
            {
                //Log("Invalid credentials");
            }
            finally
            {
                //Close connection 
            }
            return orderUpdateGet.ToString();
        }

    }
}
