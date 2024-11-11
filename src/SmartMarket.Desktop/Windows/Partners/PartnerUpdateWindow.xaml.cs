using SmartMarketDesktop.DTOs.DTOs.Partners;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Controls;
using System.Windows.Interop;
using ToastNotifications;
using SmartMarketDeskop.Integrated.Services.Partners;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace SmartMarket.Desktop.Windows.Partners;

/// <summary>
/// Interaction logic for PartnerUpdateWindow.xaml
/// </summary>
public partial class PartnerUpdateWindow : Window
{

    private readonly IPartnerService _partnerService;

    public Guid partnerId { get; set; }
    public string firstname { get; set; } = "";
    public string lastname { get; set; } = "";
    public string phonenumber { get; set; } = "";

    public PartnerUpdateWindow()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
    }

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

    Notifier notifierthis = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    internal void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(this);

        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }

    private void phone_number_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        string text = textBox.Text;
        string filteredText = Regex.Replace(text, "[^0-9]+", "");

        if (text != filteredText)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = filteredText;
            textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        tb_Firstname.Text = firstname;
        tb_Lastname.Text = lastname;
        tb_PhoneNumber.Text = phonenumber.Substring(4);
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private async void Partner_Update_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateDto partner = new PartnerCreateDto();
        partner.FirstName = tb_Firstname.Text;
        partner.LastName = tb_Lastname.Text;
        partner.PhoneNumber = "+998" + tb_PhoneNumber.Text;

        bool result = await _partnerService.UpdatePartner(partner, partnerId);

        if (result)
        {
            this.Close();
            notifier.ShowSuccess("Hamkor yangilandi.");
        }
        else
            notifierthis.ShowError("Hamkorni yangilashda qandaydir xatolik bor!");
    }
}
