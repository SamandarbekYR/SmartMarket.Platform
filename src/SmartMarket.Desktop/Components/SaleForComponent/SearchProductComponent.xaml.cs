using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Brushes = System.Windows.Media.Brushes;

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
        var product = this.Tag as FullProductDto;

        if (product!.Count > 0)
        {
            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is SalePage salePage)
                {
                    salePage.AddNewProductTvm(product!, 1);
                    break;
                }
            }
            SearchProductWindow searchProductWindow = GetSearchProductWindow();
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
