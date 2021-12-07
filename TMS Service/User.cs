/*
 * FILE             : User.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : This class stores user information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Service
{
    /// <summary>
    /// This class stores user information.
    /// </summary>
    public class User
    {
        //DATA MEMBERS
        private string userName;
        private string role;

        /// <summary>
        /// Creates an instance of a User object.
        /// </summary>
        /// <param name="userName"><b>string</b></param>
        /// <param name="role"><b>string</b></param>
        public User(string userName, string role)
        {
            this.userName = userName;
            this.role = role;
        }

        /// <summary>
        /// Returns the User's username.
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
        }

        /// <summary>
        /// Returns the User's role.
        /// </summary>
        public string Role
        {
            get
            {
                return role;
            }
        }

    }
}
