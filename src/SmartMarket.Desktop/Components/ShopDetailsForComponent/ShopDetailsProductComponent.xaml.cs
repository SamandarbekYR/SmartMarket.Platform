using SmartMarket.Desktop.Windows;
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

    public void SetValues(int id, long transactionNumber, string productName, string barCode, string categoryName, string workerName, int discount, int count, double totalPrice, string kassa, double price, DateTime? date)
    {
        lb_Count.Content = id.ToString();
        lb_Transaction.Content = transactionNumber.ToString();
        lb_Productname.Content = productName; 
        lb_Seller.Content = workerName;
        lb_Discount.Content = discount.ToString();
        lb_Count.Content = count.ToString();
        lb_Total_Price.Content = totalPrice.ToString();
        lb_Price.Content = price.ToString();
        lb_Date.Content = date.ToString();

    }

    private void Return_Button_Click(object sender, RoutedEventArgs e)
    {
        ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow();
        ReturnProductWindow returnProductWindow = GetReturnProductWindow();
        returnProductWindow.Hide();
        returnProductViewWindow.ShowDialog();
        returnProductWindow.Show();
    }
}
