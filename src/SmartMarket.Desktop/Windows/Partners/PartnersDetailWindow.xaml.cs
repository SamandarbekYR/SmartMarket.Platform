using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Domain.Entities.Partners;
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
using ToastNotifications;

using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Partners
{
    /// <summary>
    /// Interaction logic for PartnersDetailWindow.xaml
    /// </summary>
    public partial class PartnersDetailWindow : Window
    {
        public PartnersDetailWindow()
        {
            InitializeComponent();
        }
        public Guid partnerId { get; set; }
        public void SetData(Partner partner)
        {
            partnerId = partner.Id;
            firstName_TextBlock.Text = partner.FirstName;
            lastName_TextBlock.Text = partner.LastName;
            phoneNumber_TextBlock.Text = partner.PhoneNumber;
            var totalDebt = partner.Debtors.Sum(d => d.DeptSum);
            totalDebt_TextBlock.Text = totalDebt.ToString();
            lastPayment_TextBlock.Text = partner.LastPayment.ToString("yyyy-MM-dd");
            lastPaymentHour_TextBlock.Text = partner.LastPayment.ToString("HH:mm:ss");
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

        private async void btnPartnerPayment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PaymentPartnersWindow paymentPartnersWindow = new PaymentPartnersWindow(partnerId);
            paymentPartnersWindow.Show();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
    }
}
