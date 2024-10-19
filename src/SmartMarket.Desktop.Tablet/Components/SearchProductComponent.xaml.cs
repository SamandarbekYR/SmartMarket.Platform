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

    public int Quantity { get; set; }

    public SearchProductComponent()
    {
        InitializeComponent();
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        QuantityWindow quantityWindow = new QuantityWindow();
        quantityWindow.ShowDialog();


    }

    public void SetData(ProductDto product)
    {
        lb_PCode.Content = product.PCode;
        lb_ProductName.Content = product.Name;
        lb_Price.Content = product.SellPrice;
        lb_CategoryName.Content = product.CategoryId.ToString();
        lb_Quantity.Content = Quantity;
    }
}
