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

    public void SetValues(int id, string Transaction, string productName, string barcode, string category, string worker, string discount, int count, string totalprice, string kassa, string price, string date)
    {
        lb_Count.Content = id.ToString();
        lb_Transaction.Content = Transaction;
        lb_Productname.Content = productName;
        lb_Count.Content = count.ToString();
        lb_Total_Price.Content = totalprice;
        lb_Price.Content = price;
    }

    private void Return_Button_Click(object sender, RoutedEventArgs e)
    {
        ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow(new ProductSaleViewModel());
        ReturnProductWindow returnProductWindow = GetReturnProductWindow();
        returnProductWindow.Close();
        returnProductViewWindow.ShowDialog();
    }
}
