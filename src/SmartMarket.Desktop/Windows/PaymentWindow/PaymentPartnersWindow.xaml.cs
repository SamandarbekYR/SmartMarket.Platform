using SmartMarket.Desktop.Windows.Partners;

using SmartMarketDeskop.Integrated.Services.Partners;

using SmartMarketDesktop.DTOs.DTOs.Partners;

using System.Windows;
using System.Windows.Controls;

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

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
