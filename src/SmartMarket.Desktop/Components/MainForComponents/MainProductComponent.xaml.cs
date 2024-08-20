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

namespace SmartMarket.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainProductComponent.xaml
    /// </summary>
    public partial class MainProductComponent : UserControl
    {
        public MainProductComponent()
        {
            InitializeComponent();
        }

        public void SetValues(int id,string p_code ,string barcode,string productName, string category,string worker,string BodyPrice,int count,string TotalPrice,string Measure,string Price)
        {
            tbNumber.Text = id.ToString();
            tbP_Code.Text = p_code;
            TbBarcode.Text = barcode;
            tbProductName.Text = productName;
            tbCategory.Text = category;
            tbWorker.Text = worker;
            tbBodyPrice.Text = BodyPrice;
            tbCount.Text = count.ToString();
            tbTotalPrice.Text = TotalPrice;
            tbMeasure.Text = Measure;
            tbSalePrice.Text = Price;

        }
        
    }
}
