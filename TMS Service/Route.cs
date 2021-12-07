/*
 * FILE             : Route.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : This class stores Route information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Service
{
    /// <summary>
    /// This class stores Route information.
    /// </summary>
    public class Route
    {
        private int routeID;
        private int orderID;
        private string plannerID;
        private string status;
        private string startCity;
        private string endCity;
        private float cost;

        public Route(int routeID, int orderID, string plannerID, string status, string startCity, string endCity, float cost)
        {
            this.routeID = routeID;
            this.orderID = orderID;
            this.plannerID = plannerID;
            this.status = status;
            this.startCity = startCity;
            this.endCity = endCity;
            this.cost = cost;
        }

        public int RouteID
        {
            get
            {
                return routeID;
            }
        }

        public int OrderID
        {
            get
            {
                return orderID;
            }
        }

        public string PlannerID
        {
            get
            {
                return plannerID;
            }
        }

        public string Status
        {
            get
            {
                return status;
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

        public float Cost
        {
            get
            {
                return cost;
            }
        }

    }
}
