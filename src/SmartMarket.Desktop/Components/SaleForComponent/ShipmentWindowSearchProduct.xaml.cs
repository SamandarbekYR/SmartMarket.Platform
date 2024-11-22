using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Desktop.Windows.Sales;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for SearchProductComponent.xaml
/// </summary>
public partial class ShipmentWindowSearchProduct : UserControl
{
    public ShipmentWindowSearchProduct()
    {
        InitializeComponent();
    }

    public static SearchProductWindow GetSearchProductWindow()
    {
        SearchProductWindow searchWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(SearchProductWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                searchWindow = (SearchProductWindow)window;
                if (searchWindow != null)
                {
                    break;
                }
            }
        }
        return searchWindow!;
    }

    public static ShipmentsSaleWindow GetShipmentSaleWindow()
    {
        ShipmentsSaleWindow shipmentWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(ShipmentsSaleWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                shipmentWindow = (ShipmentsSaleWindow)window;
                if (shipmentWindow != null)
                {
                    break;
                }
            }
        }
        return shipmentWindow!;
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var product = this.Tag as FullProductDto;

        ShipmentsSaleWindow shipmentsSaleWindow = GetShipmentSaleWindow();
        shipmentsSaleWindow.AddNewProductTvm(product!, 1);

        SearchProductWindow searchProductWindow = GetSearchProductWindow();
        searchProductWindow.Close();
    }

    public void SetData(FullProductDto product)
    {
        lb_PCode.Content = product.PCode;
        lb_ProductName.Content = product.Name;  
        lb_Price.Content = product.SellPrice;
        lb_CategoryName.Content = product.CategoryName;
        lb_Quantity.Content = product.Count.ToString();
    }
}
