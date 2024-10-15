using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for SearchProductComponent.xaml
/// </summary>
public partial class SearchProductComponent : UserControl
{
    public SearchProductComponent()
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

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var product = this.Tag as ProductDto;

        foreach (Window window in Application.Current.Windows)
        {
            var frame = window.FindName("PageNavigator") as Frame; 
            if (frame != null && frame.Content is SalePage salePage)
            {
                salePage.AddNewProductTvm(product!);
                break;
            }
        }
        SearchProductWindow searchProductWindow = GetSearchProductWindow();
        searchProductWindow.Close();
    }

    public void SetData(ProductDto product)
    {
        lb_PCode.Content = product.PCode;
        lb_ProductName.Content = product.Name;  
        lb_Price.Content = product.SellPrice;
        lb_CategoryName.Content = product.CategoryId.ToString();
        lb_Quantity.Content = product.Count.ToString();
    }
}
