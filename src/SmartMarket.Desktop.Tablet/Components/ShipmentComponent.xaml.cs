using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Service.DTOs.Order;
using SmartMarketDeskop.Integrated.Services.Orders;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for ShipmentComponent.xaml
/// </summary>
public partial class ShipmentComponent : UserControl
{
    private readonly IOrderService _orderService;

    public Func<Task> DelegateShipment { get; set; } = null!;

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    public ShipmentComponent()
    {
        InitializeComponent();

        this._orderService = new OrderService();
    }

    public void SetValues(OrderDto order)
    {
        lbPartnerName.Content = order.Partner.FirstName;
        lbShipmentAmount.Content = order.ProductOrderItems.Sum(l => l.Count * l.Product.SellPrice);
        this.Tag = order;
    }

    private void btnEditShipment_Click(object sender, System.Windows.RoutedEventArgs e)
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

    private async void Delete_Button_Click(object sender, RoutedEventArgs e)
    {
        var orderDto = this.Tag as OrderDto;
        var result = await _orderService.DeleteAsync(orderDto!.Id);
        if (result)
        {
            notifier.ShowSuccess("Jo'natma o'chirildi.");
            await DelegateShipment();
        }
        else
            notifier.ShowError("Jo'natma o'chirishda muammo bor.");
    }
}
