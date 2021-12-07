/*
 * FILE             : Invoice.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : This class stores invoice information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Service
{
    /// <summary>
    /// This class stores invoice information.
    /// </summary>
    class Invoice
    {
        private int invoiceID;
        private string customerID;
        private int orderID;
        private float cost;

        public Invoice(int invoiceID, string customerID, int orderID, float cost)
        {
            this.invoiceID = invoiceID;
            this.customerID = customerID;
            this.orderID = orderID;
            this.cost = cost;
        }

        public int InvoiceID
        {
            get
            {
                return invoiceID;
            }
        }

        public string CustomerID
        {
            get
            {
                return customerID;
            }
        }

        public int OrderID
        {
            get
            {
                return orderID;
            }
        }

        public float Cost
        {
            get
            {
                return cost;
            }
        }

    }
}
