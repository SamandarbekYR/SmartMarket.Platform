using SmartMarket.Desktop.Components.PartnersForComponent;
using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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

    public Guid PartnerId { get; set; }

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
        PartnerId = partnerId;
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Create_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateWindow partnerCreateWindow = new PartnerCreateWindow();
        partnerCreateWindow.Show();
        this.Close();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        await GetPartners();
    }

    private async Task GetPartners()
    {
        var partners = await Task.Run(() => _partnerService.GetAll());
        Loader.Visibility = Visibility.Collapsed;

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            St_Nationer.Children.Clear();
            int count = 1;

            if (partners.Count > 0)
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
            else
            {
                EmptyData.Visibility = Visibility.Visible;
            }
        });
    }

    private void SetPartner(PartnerDto dto)
    {
        EmptyData.Visibility = Visibility.Collapsed;
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
        else 
        { 
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedPartner != null)
        {
            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is SalePage salePage)
                {
                    salePage.ConvertTransaction(true, PartnerId);
                    break;
                }
            }
            this.Close();
        }
        else
        {
            notifier.ShowInformation("Hamkor tanlanmagan.");
        }

    }

    private CancellationTokenSource _cancellationTokenSource;
    private async void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        string search = tb_search.Text;

        try
        {
            await Task.Delay(500, token);
        }
        catch (TaskCanceledException)
        {
            return; 
        }

        if (token.IsCancellationRequested)
        {
            EmptyData.Visibility = Visibility.Collapsed;
            return;
        }

        St_Nationer.Children.Clear();
        EmptyData.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            Loader.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(async () =>
                {
                    if (search.Length >= 1)
                    {
                        var partner = await _partnerService.GetByName(search);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SetPartner(partner);
                        });
                    }
                }, token);
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                Loader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            St_Nationer.Children.Clear();
            await GetPartners();
        }
    }

}
