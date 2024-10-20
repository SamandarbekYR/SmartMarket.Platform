using SmartMarket.Desktop.Tablet.Pages;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Desktop.Tablet.Windows.Partners;
using SmartMarket.Domain.Entities.Partners;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Tablet.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Tablet.Components;

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
    }

    public static MainWindow GetMainWindow()
    {
        MainWindow mainWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(MainWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                mainWindow = (MainWindow)window;
                if (mainWindow != null)
                {
                    break;
                }
            }
        }
        return mainWindow!;
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
        MainWindow mainWindow = GetMainWindow();
        MainPage mainPage = new MainPage();
        var partner = this.Tag as Partner;
        mainPage.Partner = partner!;
        mainWindow.PageNavigator.Content = mainPage;
    }

    private void Action_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {

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
