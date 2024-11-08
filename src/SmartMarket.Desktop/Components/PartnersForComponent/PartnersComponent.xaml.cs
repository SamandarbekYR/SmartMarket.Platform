using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Domain.Entities.Partners;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Components.PartnersForComponent;

/// <summary>
/// Interaction logic for PartnersComponent.xaml
/// </summary>
public partial class PartnersComponent : UserControl
{

    private readonly IPartnerService _partnerService;

    public Guid partnerId { get; set; }
    string message = string.Empty;

    public PartnersComponent()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    public void SetData(Partner partner)
    {
        lb_Firstname.Content = partner.FirstName;
        lb_Lastname.Content = partner.LastName;
        lb_Phone_Number.Content = partner.PhoneNumber;
        partnerId = partner.Id;

        this.Tag = partner;
    }

    private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) 
    {
        Partner_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8E8"));
    }

    private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Partner_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

    private void Action_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (this.Tag is Partner partner)
        {
            PartnersDetailWindow partnersDetailWindow = new PartnersDetailWindow();
            partnersDetailWindow.SetData(partner);
            partnersDetailWindow.ShowDialog();
        }
    }

    private async void Delete_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        message = lb_Firstname.Content.ToString() + " " + lb_Lastname.Content.ToString();

        var messageBox = new MessageBoxWindow(message + "ni o'chirmoqchimisiz?", MessageType.Confirmation, MessageButtons.OkCancel);
        var result = messageBox.ShowDialog();
        if (result == true)
        {
            bool deleted = await _partnerService.DeletePartner(partnerId);
            if (deleted)
                notifier.ShowInformation(message + " o'chirildi.");
            else
                notifier.ShowError(message + "ni o'chirishda muammo bor.");
        }
    }

    private void Edit_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        PartnerUpdateWindow partnerUpdateWindow = new PartnerUpdateWindow();
        partnerUpdateWindow.partnerId = partnerId;
        partnerUpdateWindow.firstname = lb_Firstname.Content.ToString()!;
        partnerUpdateWindow.lastname = lb_Lastname.Content.ToString()!;
        partnerUpdateWindow.phonenumber = lb_Phone_Number.Content.ToString()!;
        partnerUpdateWindow.ShowDialog();
    }
}
