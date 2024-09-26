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

    public void GetData(ProductViewModels product, int count)
    {
        tbNumber.Text = count.ToString();
        tbP_Code.Text = product.P_Code;
        TbBarcode.Text = product.BarCode;
        tbProductName.Text = product.ProductName;
        tbCategory.Text = product.CateogoryName;
        tbWorker.Text = product.WorkerName;
        tbBodyPrice.Text = product.Price.ToString();
        tbCount.Text = product.Count.ToString();
        tbTotalPrice.Text = product.TotalPrice.ToString();
        tbMeasure.Text = product.UnitOfMeasure.ToString();
        tbSalePrice.Text = product.SellPrice.ToString();

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
