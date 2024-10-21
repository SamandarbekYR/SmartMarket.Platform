using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainProductComponent.xaml
/// </summary>
public partial class MainProductComponent : UserControl
{
    private IProductService productService;
    ProductDto productViewModels = null!;
    public MainProductComponent()
    {
        InitializeComponent();
        this.productService = new ProductService();
    }

    public void GetData(FullProductDto product, int count)
    {
        tbNumber.Text = count.ToString();
        tbP_Code.Text = product.PCode;
        TbBarcode.Text = product.Barcode;
        tbProductName.Text = product.Name;
        tbPrice.Text = product.SellPrice.ToString();
        tbCount.Text = product.Count.ToString();

    }

    private void btnedit_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private async void btndelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        productViewModels = (this.Tag as ProductDto)!;

        var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);
        if(messageBoxResult == MessageBoxResult.Yes)
        {
            await productService.DeleteProduct(productViewModels!.Id);
        }
    }

    private void btnDocument_Click(object sender, RoutedEventArgs e)
    {

    }
}
