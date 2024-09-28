using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for SaleProductForComponent.xaml
/// </summary>
public partial class SaleProductForComponent : UserControl
{
    public Guid Id { get; set; }
    public string Barcode { get; set; }
    public int AvailableCount { get; set; }

    public SaleProductForComponent()
    {
        InitializeComponent();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var page = FindParentPage(this);
        if (page is SalePage salePage)
        {
            salePage.SelectCategory(this);
        }
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

    public void SetData(ProductDto product, int count)
    {
        tbNumber.Text = count.ToString();
        Id = product.Id;
        Barcode = product.Barcode;
        AvailableCount = product.Count;
    }
}
