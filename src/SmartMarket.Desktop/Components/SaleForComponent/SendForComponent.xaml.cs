﻿using Microsoft.AspNetCore.SignalR.Client;
using SmartMarket.Service.DTOs.Products.Product;

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Components.SaleForComponent
{
    /// <summary>
    /// Interaction logic for SendForComponent.xaml
    /// </summary>
    public partial class SendForComponent : UserControl
    {
        public SendForComponent()
        {
            InitializeComponent();
        }

        public void SetValues(string firstName, string lastName, double totalSum)
        {
            tbFullName.Text = firstName + " " + lastName;
            tbTotalSum.Text = totalSum.ToString();
        }
    }
}
