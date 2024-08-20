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

namespace SmartMarket.Desktop.Components.ShopDetailsForComponent
{
    /// <summary>
    /// Interaction logic for ShopDetailsProductComponent.xaml
    /// </summary>
    public partial class ShopDetailsProductComponent : UserControl
    {
        public ShopDetailsProductComponent()
        {
            InitializeComponent();
        }



        public void SetValues(int id,string Transaction, string productName, string barcode, string category, string worker, string discount,int count, string totalprice,string kassa,string price,string date)
        {
             tbNumber.Text=id.ToString();
            tbTransactionNo.Text=Transaction;
            tbProductName.Text= productName; 
            tbBarcode.Text=barcode;
            tbCategory.Text=category;
            tbWorkerName.Text = worker;
            tbDiscount.Text=discount;
            tbCount.Text=count.ToString();
            tbTotalPrice.Text = totalprice;
            tbKassa.Text=kassa; 
            tbPrice.Text=price;
            tbDate.Text=date;

        }

       

    }
}
