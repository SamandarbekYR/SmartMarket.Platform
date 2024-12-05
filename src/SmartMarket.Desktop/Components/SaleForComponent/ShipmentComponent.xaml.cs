 using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Windows.Sales;
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
    public ShipmentComponent()
    {
        InitializeComponent();
    }

    public void SetData(OrderDto dto)
    {
        lbFullName.Content = dto.Partner.FirstName + " " + dto.Partner.LastName;
        lbTotalSum.Content = dto.ProductOrderItems.Sum(x => x.Product.SellPrice * x.Count).ToString();
    }

    private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var shipment = this.Tag as OrderDto;
        var page = FindParentPage(this);
        if (page is SalePage salePage)
        {
            if(salePage.tvm.Transactions.Count == 0)
            {
                salePage.ConvertShipment(shipment!);
                salePage.save_button.Visibility = Visibility.Visible;
                SaleButton.Visibility = Visibility.Collapsed;
                CancelButton.Visibility = Visibility.Visible;
            }
            else
            {
                ShipmentsSaleWindow saleWindow = new ShipmentsSaleWindow();
                saleWindow.ConvertShipment(shipment!);
                saleWindow.ShowDialog();
                await salePage.GetAllOrders();
            }
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

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        var page = FindParentPage(this);
        if (page is SalePage salePage)
        {
            salePage.StopSale();
            salePage.save_button.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            SaleButton.Visibility = Visibility.Visible;
        }
    }
}
