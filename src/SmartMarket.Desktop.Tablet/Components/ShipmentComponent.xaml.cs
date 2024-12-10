using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Service.DTOs.Order;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for ShipmentComponent.xaml
/// </summary>
public partial class ShipmentComponent : UserControl
{
    public ShipmentComponent()
    {
        InitializeComponent();
    }

    public void SetValues(OrderDto order)
    {
        lbPartnerName.Content = order.Partner.FirstName;
        lbShipmentAmount.Content = order.ProductOrderItems.Sum(x => x.ItemTotalCost);
        this.Tag = order;
    }

    private async void btnEditShipment_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var orderDto = this.Tag as OrderDto;
        var page = FindParentPage(this);

        if (page is SecondPage secondPage)
        {
            secondPage.SelectOrder(this, orderDto!);
            btnEditShipment.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Visible;
        }
    }

    private Page FindParentPage(DependencyObject child)
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if(parentObject is Page page)
        {
            return page;
        }
        else if(parentObject != null)
        {
            return FindParentPage(parentObject);
        }

        return null!;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        var page = FindParentPage(this);
        if(page is SecondPage secondPage)
        {
            secondPage.StopSale();
            CancelButton.Visibility = Visibility.Collapsed;
            btnEditShipment.Visibility = Visibility.Visible;
        }
    }
}
