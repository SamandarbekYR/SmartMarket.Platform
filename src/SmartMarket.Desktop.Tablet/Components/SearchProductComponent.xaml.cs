using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for SearchProductComponent.xaml
/// </summary>
public partial class SearchProductComponent : UserControl
{

    public int Quantity { get; set; } = 0;
    public int AvailabeCount { get; set; }

    public SearchProductComponent()
    {
        InitializeComponent();
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var product = this.Tag as FullProductDto;

        if (product!.Count > 0) 
        {
            QuantityWindow quantityWindow = new QuantityWindow(this);
            quantityWindow.ShowDialog();

            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is MainPage mainPage)
                {
                    mainPage.AddNewProductTvm(product!, Quantity);
                    mainPage.tb_search.Text = "";
                    mainPage.st_searchproduct.Children.Clear();
                    break;
                }
                else if (frame != null && frame.Content is SecondPage secondPage)
                {
                    secondPage.AddNewProduct(product, Quantity);
                    secondPage.tb_search.Text = "";
                    secondPage.st_searchproduct.Children.Clear();
                    break;
                }
            }
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
        AvailabeCount = product.Count;
        lb_ProductName.Content = product.Name;
        lb_Price.Content = product.SellPrice;
        lb_CategoryName.Content = product.CategoryName;
        lb_Quantity.Content = product.Count;
    }
}
