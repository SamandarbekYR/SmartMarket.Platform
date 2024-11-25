using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Partners
{
    /// <summary>
    /// Interaction logic for CompanyUpdateWindow.xaml
    /// </summary>
    public partial class CompanyUpdateWindow : Window
    {
        private readonly IPartnerCompanyService _partnerCompanyService;
        PartnerCompanyView companyView;
        public CompanyUpdateWindow()
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
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
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
            txtCompanyName.Text = companyView.Name;
            txtPhoneNumber.Text = companyView.PhoneNumber.ToString();
        }

        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
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

        public void GetData(PartnerCompanyView company)
        {
            companyView = company;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Clear()
        {
            txtPhoneNumber.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtDescribtion.Text = string.Empty;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtPhoneNumber.Text) && !string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrEmpty(txtDescribtion.Text))
            {
                AddPartnerCompanyDto companyDto = new AddPartnerCompanyDto();
                companyDto.PhoneNumber = txtPhoneNumber.Text;
                companyDto.Name = txtCompanyName.Text;
                companyDto.Description = txtDescribtion.Text;

                var result = await _partnerCompanyService.UpdateCompany(companyView.Id, companyDto);

                if(result)
                {
                    Clear();
                    this.Close();
                    notifier.ShowSuccess("Firma yangilandi.");
                }
                else
                {
                    notifier.ShowError("Firmani yangilashda xatolik yuz berdi.");
                }
            }
            else
            {
                notifierThis.ShowWarning("Firma malumotlari to'liq emas!");
            }
        }
    }
}
