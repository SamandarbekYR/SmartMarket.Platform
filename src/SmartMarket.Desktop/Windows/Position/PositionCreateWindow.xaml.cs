using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.Services.Workers.Position;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace SmartMarket.Desktop.Windows.Position;

/// <summary>
/// Interaction logic for PositionCreateWindow.xaml
/// </summary>
public partial class PositionCreateWindow : Window
{
    private IPositionService _positionService;
    public PositionCreateWindow()
    {
        InitializeComponent();
        _positionService = new PositionService();
    }

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

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private async void btnPositionCreate_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (btnPositionCreateContainer.IsEnabled == false) return;

        btnPositionCreateContainer.IsEnabled = false;
        var name = txtName.Text;
        if (!string.IsNullOrEmpty(name))
        {
            AddPositionDto addPositionDto = new AddPositionDto()
            {
                Name = name
            };
           
            var result = await _positionService.AddAsync(addPositionDto);
            if (result)
            {
                this.Close();
                notifier.ShowInformation("Pozitsiya muvaddaqiyatli yaratildi.");
            }
            else
            {
                notifier.ShowError("Pozitsiya yaratishda xato yuz berdi.");
                btnPositionCreateContainer.IsEnabled = true;

            }
        }
        else
        {
            MessageBox.Show("Xatolik mavjud! Dasturni qo'llab quvvatlovchi shaxslarga murojaat qiling!");
            btnPositionCreateContainer.IsEnabled = true;
        }
    }

    private void brClose_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }
}
