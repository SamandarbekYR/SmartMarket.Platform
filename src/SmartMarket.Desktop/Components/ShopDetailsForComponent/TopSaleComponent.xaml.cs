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
    /// Interaction logic for TopSaleComponent.xaml
    /// </summary>
    public partial class TopSaleComponent : UserControl
    {
        public TopSaleComponent()
        {
            InitializeComponent();
        }

        public void SetValues(string ProductName,int count,string TotalPrice,string profit)
        {
            tbProductName.Text = ProductName;
            tbTotalPrice.Text = TotalPrice;
            tbCount.Text=count.ToString();
            tbProfit.Text = profit;
        }
    }
}
