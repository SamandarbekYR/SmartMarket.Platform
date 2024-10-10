using SmartMarket.Desktop.Windows.ProductsForWindow;
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
        Guid productSaleId = (Guid)((Button)sender).Tag;

        ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow(productSaleId);
        returnProductViewWindow.ShowDialog();
    }
}
