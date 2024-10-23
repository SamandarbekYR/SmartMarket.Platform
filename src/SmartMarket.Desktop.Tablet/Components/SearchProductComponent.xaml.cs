using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;

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
        QuantityWindow quantityWindow = new QuantityWindow(this);
        quantityWindow.ShowDialog();

        var product = this.Tag as FullProductDto;

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
                secondPage.tb_search.Text = "";
                secondPage.st_searchproduct.Children.Clear();
            }
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
