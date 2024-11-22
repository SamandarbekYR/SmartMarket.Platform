using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using SmartMarket.Service.DTOs.PayDesks;
using ToastNotifications.Messages;

namespace SmartMarket.Desktop.Windows.PaymentWindow
{
    /// <summary>
    /// Interaction logic for PayDeskCreateWindow.xaml
    /// </summary>
    public partial class PayDeskCreateWindow : Window
    {
        private IPayDeskService payDeskService;
        public PayDeskCreateWindow()
        {
            InitializeComponent();
            this.payDeskService = new PayDeskService();
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

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 50,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 50,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            txtPayDeskName.Text = string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
        
        private async void btnPayDeskCreate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPayDeskName.Text))
            {
                AddPayDesksDto dto = new AddPayDesksDto();
                dto.Name = txtPayDeskName.Text;

                var result = await payDeskService.CreatePayDesk(dto);

                if (result)
                {
                    txtPayDeskName.Text = string.Empty;
                    this.Close();
                    notifier.ShowSuccess("Kassa muvaffaqriyatli qo'shildi");
                }
                else
                    notifierThis.ShowError("Kassa qo'shishda xatolik yuz berdi");
            }
            else
                notifierThis.ShowWarning("Kassa malumoti to'liq emas!");
        }
    }
}
