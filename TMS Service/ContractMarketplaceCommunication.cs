/*
* FILE : ContractMarketplaceCommunication.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-11-25
* DESCRIPTION : Class to connect to the Contract Marketplace remote SQL Database
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace TMSProject
{
    /// <summary>
    /// Class to connect to the Contract Marketplace remote SQL Database, review and retrieve contracts.
    /// </summary>
    public class ContractMarketplaceCommunication
    {
        // Ref: https://zetcode.com/csharp/mysql/

        // Credential constants 
        private static readonly string _uid = "DevOSHT";
        private static readonly string _password = "Snodgr4ss!";
        private static readonly string _address = "159.89.117.198";
        private static readonly string _port = "3306";
        private static readonly string _db = "cmp";
        private static readonly string _connectionString = "SERVER=" + _address + "; PORT =" + _port + ";" + "DATABASE=" + _db + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";

        private MySqlConnection connectionCM = new MySqlConnection(_connectionString);

        /// <summary>
        /// Tests an attempt to connect to the Contract Marketplace with the provided credentials
        /// </summary>
        /// <param name="connectionString"> - <b>string</b> -The connection string for Contract Marketplace.</param>
        /// <returns><b>bool</b> : <b>true</b> if login success, <b>false</b> if failed.</returns>     
        public bool ConnectTestCM()
        {
            try
            {
                connectionCM.Open();
            }
            catch (MySqlException ex)
            {
                Logger.WriteLog("Contract Marketplace Communication connection exception: " + ex);
                return false;
            }
            finally
            {
                if(connectionCM.State == ConnectionState.Open)
                {
                    connectionCM.Close();
                }
                
            }
            return true;
        }

        /// <summary>
        /// Gets list of current available contracts
        /// </summary>
        /// <returns><b>string</b> : Several rows of contracts in format: CLIENT_NAME|JOB_TYPE|QUANTITY|ORIGIN|DESTINATION|VAN_TYPE.</returns>     
        public List<Contracts> GetContractList()
        {
            MySqlCommand cmd = connectionCM.CreateCommand();
            MySqlDataReader reader;
            StringBuilder contractGet = new StringBuilder();
            var contractList = new List<Contracts>();

            try
            {
                connectionCM.Open();

                cmd.CommandText = "SELECT * FROM Contract;";

                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    contractList.Add(new Contracts()
                    {
                        clientName = reader.GetString("CLIENT_NAME"),
                        jobType = reader.GetString("JOB_TYPE"),
                        quantity = reader.GetInt32("QUANTITY"),
                        origin = reader.GetString("ORIGIN"),
                        destination = reader.GetString("DESTINATION"),
                        vanType = reader.GetString("VAN_TYPE")
                    });
                }
            }
            catch (MySqlException ex)
            {
                Logger.WriteLog("Contract Marketplace Communication GetContractList exception: " + ex);
            }
            finally
            {
                connectionCM.Close();
            }
            return contractList;
        }
    }

}
