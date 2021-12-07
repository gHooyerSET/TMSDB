/*
 * FILE             : Order.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : This class stores Order information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Service
{
    /// <summary>
    /// This class stores Order information.
    /// </summary>
    class Order
    {
        private int orderID;
        string customerID;
        string startCity;
        string endCity;
        string status;
        DateTime orderDate;

        public Order(int orderID, string customerID, string startCity, string endCity, string status, DateTime orderDate)
        {
            this.orderID = orderID;
            this.customerID = customerID;
            this.startCity = startCity;
            this.endCity = endCity;
            this.status = status;
            this.orderDate = orderDate;
        }

        public int OrderID
        {
            get
            {
                return orderID;
            }
        }

        public string CustomerID
        {
            get
            {
                return customerID;
            }
        }

        public string StartCity
        {
            get
            {
                return startCity;
            }
        }

        public string EndCity
        {
            get
            {
                return endCity;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
        }

        public DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
        }


    }
}
