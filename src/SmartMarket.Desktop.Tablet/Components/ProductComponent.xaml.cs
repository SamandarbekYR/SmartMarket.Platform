using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for ProductComponent.xaml
/// </summary>
public partial class ProductComponent : UserControl
{

    public Guid Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public int AvailableCount { get; set; }

    public ProductComponent()
    {
        InitializeComponent();
    }

    private Page FindParentPage(DependencyObject child)
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject is Page page)
        {
            return page;
        }
        else if (parentObject != null)
        {
            return FindParentPage(parentObject);
        }
        return null!;
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var page = FindParentPage(this);
        if (page is MainPage mainPage)
        {
            mainPage.SelectProduct(this);
        }
    }

    public void SetData(ProductDto productDto)
    {

    }
}
