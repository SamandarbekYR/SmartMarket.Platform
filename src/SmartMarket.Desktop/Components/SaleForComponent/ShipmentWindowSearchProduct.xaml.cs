using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Desktop.Windows.Sales;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

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

    public static ShipmentSearchProductWindow GetSearchProductWindow()
    {
        ShipmentSearchProductWindow searchWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(ShipmentSearchProductWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                searchWindow = (ShipmentSearchProductWindow)window;
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

        if (product!.Count > 0)
        {
            ShipmentsSaleWindow shipmentsSaleWindow = GetShipmentSaleWindow();
            shipmentsSaleWindow.AddNewProductTvm(product!, 1);

            ShipmentSearchProductWindow searchProductWindow = GetSearchProductWindow();
            searchProductWindow.Close();
        }
        else
        {
            lb_Quantity.Foreground = Brushes.Red;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 5,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2)
            };

            var transform = new TranslateTransform();
            lb_Quantity.RenderTransform = transform;

            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
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
