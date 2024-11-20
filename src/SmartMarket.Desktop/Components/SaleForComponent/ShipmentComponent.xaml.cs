using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.ViewModels.Transactions;
using SmartMarket.Service.DTOs.Order;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for ShipmentComponent.xaml
/// </summary>
public partial class ShipmentComponent : UserControl
{
    TransactionViewModel tvm;

    public ShipmentComponent()
    {
        InitializeComponent();
        this.tvm = new TransactionViewModel();
    }

    public void SetData(OrderDto dto)
    {
        lbFullName.Content = dto.Partner.FirstName + " " + dto.Partner.LastName;
        lbTotalSum.Content = dto.ProductOrderItems.Sum(x => x.Product.SellPrice * x.Count).ToString();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var shipment = this.Tag as OrderDto;
        var page = FindParentPage(this);
        if (page is SalePage salePage)
        {
            salePage.ConvertShipment(shipment!);
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
}
