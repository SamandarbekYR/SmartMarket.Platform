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
        tbPrice.Text = product.Price.ToString();
        tbProductName.Text = product.Name;
        tbTotalPrice.Text = (double.Parse(tbQuantity.Text) * product.Price).ToString();
    }
}
