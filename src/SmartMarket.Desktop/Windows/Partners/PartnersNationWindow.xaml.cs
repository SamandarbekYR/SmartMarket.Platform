using SmartMarket.Desktop.Components.PartnersForComponent;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Partners;

/// <summary>
/// Interaction logic for PartnersNationWindow.xaml
/// </summary>
public partial class PartnersNationWindow : Window
{
    private readonly IPartnerService _partnerService;

    public PartnersNationWindow()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
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

    private PartnerNationComponent selectedPartner = null!;
    public async void SelectPartner(PartnerNationComponent partner, Guid partnerId)
    {
        if (selectedPartner != null)
        {
            selectedPartner.br_Partner.Background = Brushes.White;
        }

        partner.br_Partner.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));

        selectedPartner = partner;
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Create_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateWindow partnerCreateWindow = new PartnerCreateWindow();
        this.Close();
        partnerCreateWindow.ShowDialog();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        GetPartners();
    }

    private async void GetPartners()
    {
        var partners = await _partnerService.GetAll();

        St_Nationer.Children.Clear();
        int count = 1;

        if(partners.Count > 0)
        {
            foreach (var partner in partners)
            {
                PartnerNationComponent partnerNationComponent = new PartnerNationComponent();
                partnerNationComponent.Tag = partner;
                partnerNationComponent.SetData(partner, count);
                St_Nationer.Children.Add(partnerNationComponent);
                count++;
            }
        }
    }

    private void SetPartner(PartnerDto dto)
    {
        St_Nationer.Children.Clear();
        if (dto != null)
        {
            Partner partner = new Partner()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                LastPayment = dto.LastPayment,
                TotalDebt = dto.TotalDebt
            };
            PartnerNationComponent partnerNationComponent = new PartnerNationComponent();
            partnerNationComponent.Tag = partner;
            partnerNationComponent.SetData(partner, 1);
            St_Nationer.Children.Add(partnerNationComponent);
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedPartner != null)
        {
            this.Close();
        }
        else
        {
            notifier.ShowInformation("Hamkor tanlanmagan.");
        }

    }

    private async void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        string search = tb_search.Text;

        await Task.Run(async () =>
        {
            if (search.Length >= 3)
            {
                var partner = await _partnerService.GetByName(search);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SetPartner(partner);
                });
            }
        });
    }
}
