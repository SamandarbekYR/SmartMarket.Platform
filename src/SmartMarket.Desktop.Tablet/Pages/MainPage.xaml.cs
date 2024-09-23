using SmartMarket.Desktop.Tablet.Components;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 20; i++)
        {
            SearchProductComponent searchProductComponent = new SearchProductComponent();
            ProductComponent productComponent = new ProductComponent();

            st_searchproduct.Children.Add(searchProductComponent);
            st_product.Children.Add(productComponent);
        }
    }
}
