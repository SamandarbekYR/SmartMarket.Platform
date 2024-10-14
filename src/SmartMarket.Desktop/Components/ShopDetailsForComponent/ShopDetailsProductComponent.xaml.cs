using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.ViewModels.Products;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.ShopDetailsForComponent;

/// <summary>
/// Interaction logic for ShopDetailsProductComponent.xaml
/// </summary>
public partial class ShopDetailsProductComponent : UserControl
{

    public ShopDetailsProductComponent()
    {
        InitializeComponent();
    }

    public void SetValues(int id, long transactionNumber, string productName, double price, int count, double totalPrice)
    {
        lb_Count.Content = id.ToString();
        lb_Price.Content = price.ToString();
        lb_Productname.Content = productName; 
        lb_Product_Count.Content = count.ToString();
        lb_Total_Price.Content = totalPrice.ToString();
        lb_Transaction.Content = transactionNumber.ToString();
    }

    private void Return_Button_Click(object sender, RoutedEventArgs e)
    {
        var productSale = this.Tag as ProductSaleViewModel;

        ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow(productSale);
        returnProductViewWindow.ShowDialog();
    }
}
