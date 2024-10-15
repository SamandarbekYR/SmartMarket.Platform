using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.ViewModels.Products;

using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.ShopDetailsForComponent;

/// <summary>
/// Interaction logic for ReturnProductComponent.xaml
/// </summary>
public partial class ReturnProductComponent : UserControl
{
    public ReturnProductComponent()
    {
        InitializeComponent();
    }

    public static ReturnProductWindow GetReturnProductWindow()
    {
        ReturnProductWindow mainWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(ReturnProductWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                mainWindow = (ReturnProductWindow)window;
                if (mainWindow != null)
                {
                    break;
                }
            }
        }
        return mainWindow!;
    }

    public void SetValues(int id, long Transaction, string productName, double price, int count, double totalprice, double discount, string worker, DateTime? date)
    {
        lb_Count.Content = id.ToString();
        lb_Transaction.Content = Transaction;
        lb_Productname.Content = productName;
        lb_Product_Count.Content = count.ToString();
        lb_Total_Price.Content = totalprice;
        lb_Price.Content = price;
        lb_Discount.Content = discount;
        lb_Saller.Content = worker;
        lb_Date.Content = date?.ToString("dd.MM.yyyy");
    }

    private void Return_Button_Click(object sender, RoutedEventArgs e)
    {
        var productSale = this.Tag as ProductSaleViewModel;

        ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow(productSale);
        ReturnProductWindow returnProductWindow = GetReturnProductWindow();
        returnProductWindow.Close();
        returnProductViewWindow.ShowDialog();
    }
}
