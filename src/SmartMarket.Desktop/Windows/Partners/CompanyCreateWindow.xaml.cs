using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Partners
{
    /// <summary>
    /// Interaction logic for CompanyCreateWindow.xaml
    /// </summary>
    public partial class CompanyCreateWindow : Window
    {
        private IPartnerCompanyService _partnerCompanyService;
        public CompanyCreateWindow()
        {
            InitializeComponent();
            this._partnerCompanyService = new PartnerCompanyService();
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
                offsetX: 50,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Clear()
        {
            txtCompanyName.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtDescribtion.Text = string.Empty;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateButton.IsEnabled == false) return;

            CreateButton.IsEnabled = false;
            if (!string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrEmpty(txtPhoneNumber.Text) && !string.IsNullOrEmpty(txtDescribtion.Text))
            {
                AddPartnerCompanyDto partnerCompanyDto = new AddPartnerCompanyDto();
                partnerCompanyDto.Name = txtCompanyName.Text;
                partnerCompanyDto.PhoneNumber = txtPhoneNumber.Text;
                partnerCompanyDto.Description = txtDescribtion.Text;

                var result = await _partnerCompanyService.CreateCompany(partnerCompanyDto);
                if(result)
                {
                    Clear();
                    this.Close();
                    notifier.ShowSuccess($"{txtCompanyName.Text} muvaffaqiyatli yaratildi.");
                }
                else
                {
                    notifierThis.ShowError("Firma yaratishda xatolik yuz berdi!");
                    CreateButton.IsEnabled = true;
                }
            }
            else
            {
                notifierThis.ShowWarning("Firma malumotlari to'liq emas!");
                CreateButton.IsEnabled = true;
            }
        }
    }
}
