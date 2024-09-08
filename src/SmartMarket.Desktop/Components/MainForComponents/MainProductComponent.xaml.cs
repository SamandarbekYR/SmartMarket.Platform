using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainProductComponent.xaml
/// </summary>
public partial class MainProductComponent : UserControl
{
    private IProductService productService;
    ProductViewModels productViewModels;
    public MainProductComponent()
    {
        InitializeComponent();
        this.productService = new ProductService();
    }

    public void GetData(int Number,string p_code ,string barcode,string productName, string category,string worker,double price,int count,double TotalPrice,string UnitofMeasure,double SellPrice)
    {
        tbNumber.Text = Number.ToString();
        tbP_Code.Text = p_code;
        TbBarcode.Text = barcode;
        tbProductName.Text = productName;
        tbCategory.Text = category;
        tbWorker.Text = worker;
        tbBodyPrice.Text = price.ToString();
        tbCount.Text = count.ToString();
        tbTotalPrice.Text = TotalPrice.ToString();
        tbMeasure.Text = UnitofMeasure.ToString();
        tbSalePrice.Text = SellPrice.ToString();

    }

    private void btnedit_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private async void btndelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        productViewModels = this.Tag as ProductViewModels;

        var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);
        if(messageBoxResult == MessageBoxResult.Yes)
        {
            await productService.DeleteProduct(productViewModels.Id);
        }

    }
}
