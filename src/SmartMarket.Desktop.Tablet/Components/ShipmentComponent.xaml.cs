using SmartMarket.Service.DTOs.Order;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for ShipmentComponent.xaml
/// </summary>
public partial class ShipmentComponent : UserControl
{
    public Func<Task> EditShipment { get; set; }
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
    {if(EditShipment != null && this.Tag is OrderDto order)
        {
            await EditShipment();
        }

    }
}
