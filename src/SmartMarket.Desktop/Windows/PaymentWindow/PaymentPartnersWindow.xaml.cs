using SmartMarket.Desktop.Windows.Partners;

using SmartMarketDeskop.Integrated.Services.Partners;

using SmartMarketDesktop.DTOs.DTOs.Partners;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.PaymentWindow
{
    public partial class PaymentPartnersWindow : Window
    {
        private IPartnerService _partnerService;
        private Guid _partnerId;
        public Func<Task> RefreshPartnerPage { get; set; }

        public PaymentPartnersWindow(Guid partnerId)
        {
            _partnerId = partnerId;
            _partnerService = new PartnerService();
            InitializeComponent();
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

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur(); 
        }

        private void tbPayAmount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*(\\.[0-9]*)?$"); 
            e.Handled = !regex.IsMatch(((TextBox)sender).Text + e.Text);
        }

        private void tbPayAmount_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                Regex regex = new Regex("^[0-9]*(\\.[0-9]*)?$");
                if (!regex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        private async void PartnerPayment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (BtnPay.IsEnabled == false) return;

            BtnPay.IsEnabled = false;
            ComboBoxItem selectedItem = (ComboBoxItem)cbPayment.SelectedItem;
            string selectedValue = selectedItem?.Content.ToString()!;

            PartnerCreateDto partnerCreateDto = new PartnerCreateDto()
            {
                LastPayment = double.Parse(tbPayAmount.Text),
                LastPaymentDate = DateTime.Now,
                PaymentType = selectedValue!,
            };

            await _partnerService.UpdatePartner(partnerCreateDto, _partnerId);

            this.Close();
        }
    }
}
