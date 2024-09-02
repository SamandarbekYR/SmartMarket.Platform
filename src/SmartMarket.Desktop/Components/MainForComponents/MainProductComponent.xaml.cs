using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.MainForComponents;

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
