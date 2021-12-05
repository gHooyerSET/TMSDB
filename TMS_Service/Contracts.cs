/*
* FILE : Contracts.cs
* PROJECT : TMS Project - Group 15
* PROGRAMMER : Nathan Domingo
* FIRST VERSION : 2021-11-25
* DESCRIPTION : Class to store Contracts retreived from Contract Marketplace
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject
{
    /// <summary>
    /// Class to store Contratcs from the Contract Marketplace remote SQL Database.
    /// </summary>
    public class Contracts
    {
            // Data retrieved from SQL database as: CLIENT_NAME|JOB_TYPE|QUANTITY|ORIGIN|DESTINATION|VAN_TYPE
            public string clientName { get; set; }
            public string jobType { get; set; }
            public int quantity { get; set; }
            public string origin { get; set; }
            public string destination { get; set; }
            public string vanType { get; set; }
    }
}
