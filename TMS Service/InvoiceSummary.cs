using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject;

namespace TMS_Service
{
    class InvoiceSummary
    {
        string buyerID;
        int totalInvoices;
        float cost;
        

        public InvoiceSummary(string buyerID)
        {
            TMSDB tmsdb = new TMSDB();
            //Set the buyer ID
            this.buyerID = buyerID;
            //Set cost to initial value of 0
            cost = 0;
            //Get the invoices attached to the buyerID
            totalInvoices = 0;
            //Check that an invoice exists   
            if(tmsdb.GetInvoices(buyerID) != string.Empty)
            {
                //Now get the invoice data
                string[] invoicesArray = tmsdb.GetInvoices(buyerID).Split(',');
                //Now iterate through the invoices
                for (int i = 0; i < invoicesArray.Length;)
                {
                    //Add to the cost
                    cost += float.Parse(invoicesArray[i + 3]);
                    //Increment total invoices
                    totalInvoices++;
                    //Iterate to the next row
                    i += 4;
                }
            }  
        }

        public string BuyerID
        {
            get
            {
                return buyerID;
            }
        }

        public int TotalInvoices
        {
            get
            {
                return totalInvoices;
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
