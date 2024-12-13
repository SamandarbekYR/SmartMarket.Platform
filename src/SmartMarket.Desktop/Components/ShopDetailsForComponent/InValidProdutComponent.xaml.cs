﻿using System;
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

namespace SmartMarket.Desktop.Components.ShopDetailsForComponent
{
    /// <summary>
    /// Interaction logic for InValidProdutComponent.xaml
    /// </summary>
    public partial class InValidProdutComponent : UserControl
    {
        public InValidProdutComponent()
        {
            InitializeComponent();
        }

        public void SetValues(int id, long transactionNumber, string productName, double price, int count, double totalprice)
        {
            lb_Number.Content = id.ToString();
            lb_Count.Content = count.ToString();
            lb_Price.Content = price.ToString();
            lb_ProductName.Content = productName;
            lb_TotalPrice.Content = totalprice.ToString();
            lb_TransactionNo.Content = transactionNumber.ToString();
        }
    }
}   
