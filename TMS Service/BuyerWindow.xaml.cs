﻿/*
 * FILE             : BuyerWindow.xaml.cs
 * PROJECT          : TMS System - Software Quality
 * PROGRAMMER       : Gerritt Hooyer
 * FIRST VERSION    : 2021-11-27
 * DESCRIPTION      : Provides the GUI to the buyer.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMSProject;

namespace TMS_Service
{
    /// <summary>
    /// Interaction logic for BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        User user;
        MainWindow main;
        TMSDB tmsdb;

        public BuyerWindow(User user, MainWindow main)
        {
            InitializeComponent();
            this.user = user;
            this.main = main;
            sbiUserName.Content += user.UserName;
            tmsdb = new TMSDB();
            FillOrderGrid();
        }

        /// <summary>
        /// Logs the user out. Closes this window and unhides the login window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            //Show the login window
            main.Show();
            //And then close the buyer window
            this.Close();
        }

        /// <summary>
        /// Allows the user to view orders in the data grid.
        /// </summary>
        private void FillOrderGrid()
        {
            dgInfo.ItemsSource = null;

            //Instantiate some variables we'll need
            List<Order> orders = new List<Order>();
            string ordersString = tmsdb.GetOrders(user.UserName);
            string[] ordersStringArray = ordersString.Split(',');

            try
            {
                //Only fill the thing if SOME sort of string was created
                if (ordersString != string.Empty)
                {
                    //This for loop creates Order objects from the ordersStringArray
                    for (int i = 0; i < ordersStringArray.Length;)
                    {
                        //Create a new order
                        Order order = new Order(Int32.Parse(ordersStringArray[i]), ordersStringArray[i + 1], ordersStringArray[i + 2],
                            ordersStringArray[i + 3], ordersStringArray[i + 4], DateTime.Parse(ordersStringArray[i + 5]));
                        //Then we add it to the list
                        orders.Add(order);
                        //Then we iterate forward 6 indexes (the width of the table in columns)
                        i += 6;
                    }
                }
                else
                {
                    orders.Add(new Order(-1, null, null, null, null, DateTime.MinValue));
                }
            }
            catch (Exception ex)
            {
                
            }
            

            dgInfo.ItemsSource = orders;

        }

        /// <summary>
        /// Allows the user to view invoice information in the data grid.
        /// </summary>
        private void FillInvoiceGrid()
        {
            dgInfo.ItemsSource = null;

            List<Invoice> invoices = new List<Invoice>();
            string invoicesString = tmsdb.GetInvoices(user.UserName);
            string[] invoicesStringArray = invoicesString.Split(',');

            try
            {
                if(invoicesString != string.Empty)
                {
                    //This for loop creates Order objects from the ordersStringArray
                    for (int i = 0; i < invoicesStringArray.Length;)
                    {
                        //Create a new order
                        Invoice invoice = new Invoice(Int32.Parse(invoicesStringArray[i]), invoicesStringArray[i + 1], Int32.Parse(invoicesStringArray[i + 2]),
                            float.Parse(invoicesStringArray[i + 3]));
                        //Then we add it to the list
                        invoices.Add(invoice);
                        //Then we iterate forward 6 indexes (the width of the table in columns)
                        i += 4;
                    }
                }
                else
                {
                    invoices.Add(new Invoice(-1, null, -1, -1));
                }
            }
            catch (Exception ex)
            {
                
            }

            dgInfo.ItemsSource = invoices;

        }

        /// <summary>
        /// On the user click, it runs the FillInvoiceGrid function.
        /// This displays the invoice information to the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewInvoices_Click(object sender, RoutedEventArgs e)
        {
            FillInvoiceGrid();
        }

        /// <summary>
        /// On click, ir runs the fillOrderGrid function.
        /// This displays the orders to the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewOrders_Click(object sender, RoutedEventArgs e)
        {
            FillOrderGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder createOrder = new CreateOrder(user);
            createOrder.Show();
        }

        /// <summary>
        /// On this window closing, the login window is shown again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.Show();
        }
    }
}