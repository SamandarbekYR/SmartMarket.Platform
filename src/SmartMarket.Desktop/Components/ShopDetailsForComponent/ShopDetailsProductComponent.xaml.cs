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

    public void SetValues(int id,string Transaction, string productName, string barcode, string category, string worker, string discount,int count, string totalprice,string kassa,string price,string date)
    {
        lb_Count.Content = id.ToString();
        lb_Transaction.Content = Transaction;
        lb_Productname.Content = productName; 
        lb_Seller.Content = worker;
        lb_Discount.Content = discount;
        lb_Count.Content = count.ToString();
        lb_Total_Price.Content = totalprice;
        lb_Price.Content = price;
        lb_Date.Content = date;

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
