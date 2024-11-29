using SmartMarketDeskop.Integrated.Services.Partners;

using SmartMarketDesktop.DTOs.DTOs.Partners;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.PaymentWindow
{
    /// <summary>
    /// Interaction logic for PaymentPartnersWindow.xaml
    /// </summary>
    public partial class PaymentPartnersWindow : Window
    {
        private IPartnerService _partnerService;
        private Guid _partnerId;

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
            PartnerCreateDto partnerCreateDto = new PartnerCreateDto()
            {
                LastPayment = double.Parse(tbPayAmount.Text),
                LastPaymentDate = DateTime.Now,
            };

            await _partnerService.UpdatePartner(partnerCreateDto, _partnerId);

            this.Close();
        }
    }
}
