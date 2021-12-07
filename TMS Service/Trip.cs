/*
 * FILE             : Trip.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : This class stores Trip information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Service
{
    /// <summary>
    /// This class stores Trip information.
    /// </summary>
    class Trip
    {
        //DATA MEMBERS
        private int tripID;
        private string carrierID;
        private int routeID;
        private string startCity;
        private string endCity;
        private string type;
        private float rate;

        /// <summary>
        /// Creates an instance of a trip object
        /// </summary>
        /// <param name="tripID"><b>int</b></param>
        /// <param name="carrierID"><b>string</b></param>
        /// <param name="routeID"><b>int</b></param>
        /// <param name="startCity"><b>string</b></param>
        /// <param name="endCity"><b>string</b></param>
        /// <param name="type"><b>string</b></param>
        /// <param name="rate"><b>float</b></param>
        public Trip(int tripID, string carrierID, int routeID, string startCity, string endCity,
            string type, float rate)
        {
            this.tripID = tripID;
            this.carrierID = carrierID;
            this.routeID = routeID;
            this.startCity = startCity;
            this.endCity = endCity;
            this.type = type;
            this.rate = rate;
        }

        /// <summary>
        /// Returns a Trip's trip ID.
        /// </summary>
        public int TripID
        {
            get
            {
                return tripID;
            }
        }

        /// <summary>
        /// Returns a Trip's carrierID.
        /// </summary>
        public string CarrierID
        {
            get
            {
                return carrierID;
            }
        }

        /// <summary>
        /// Returns a trips's route ID.
        /// </summary>
        public int RouteID
        {
            get
            {
                return routeID;
            }
        }

        /// <summary>
        /// Returns a Trip's start city.
        /// </summary>
        public string StartCity
        {
            get
            {
                return startCity;
            }
        }

        /// <summary>
        /// Returns a trip's end city.
        /// </summary>
        public string EndCity
        {
            get
            {
                return endCity;
            }
        }

        /// <summary>
        /// Return's a trip's type. (FTL or LTL).
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Returns a trip's rate.
        /// </summary>
        public float Rate
        {
            get
            {
                return rate;
            }
        }

    }
}
