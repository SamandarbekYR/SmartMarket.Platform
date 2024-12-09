using SmartMarket.Desktop.Components.PaymentForComponent;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgentPayments;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
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
    /// Interaction logic for PaymentContrAgentWindow.xaml
    /// </summary>
    public partial class PaymentContrAgentWindow : Window
    {
        private IContrAgentService _contrAgentService;
        private IContrAgentPaymentService _contrAgentPaymentService;
        private ContrAgentViewModels _contrAgent;
        public PaymentContrAgentWindow(ContrAgentViewModels contrAgent)
        {
            InitializeComponent();
            _contrAgent = contrAgent;
            _contrAgentService = new ContrAgentService();
            _contrAgentPaymentService = new ContrAgentPaymentService();
        }

        private async void GetAllContrAgents()
        {
            St_paymentContrAgent.Children.Clear();

            var contrAgent = await Task.Run(async () => await _contrAgentService.GetById(_contrAgent.Id));

            var contrAgentPayments = contrAgent.ContrAgentPayment.Select(c => new ContrAgentPaymentDto
            {
                Id = c.Id,
                ContrAgentId = c.ContrAgentId,
                ContrAgent = c.ContrAgent,
                PayDeskId = c.PayDeskId,
                PayDesk = c.PayDesk,
                PaymentType = c.PaymentType,
                TotalDebt = c.TotalDebt,
                LastPayment = c.LastPayment,
                LastPaymentDate = c.LastPaymentDate
            }).ToList();

            ShowContrAgentPayments(contrAgentPayments);
        }

        private void ShowContrAgentPayments(List<ContrAgentPaymentDto> payments)
        {
            Loader.Visibility = Visibility.Collapsed;
            int count = 1;

            if(payments.Any())
            {
                foreach(var payment in payments)
                {
                    PaymentContrAgentComponent paymentContrAgentComponent = new PaymentContrAgentComponent();
                    paymentContrAgentComponent.SetValues(count,
                        payment.ContrAgent.PartnerCompany.Name,
                        payment.ContrAgent.FirstName,
                        payment.ContrAgent.LastName,
                        payment.ContrAgent.PhoneNumber,
                        payment.LastPayment,
                        payment.PaymentType,
                        payment.LastPaymentDate,
                        payment.PayDesk.Name);

                    St_paymentContrAgent.Children.Add(paymentContrAgentComponent);
                    count++;
                }
            }
            else
            {
                EmptyDataContrAgent.Visibility = Visibility.Visible;
            }
        }

        private async void FilterContrAgentPayments()
        {
            EmptyDataContrAgent.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
            St_paymentContrAgent.Children.Clear();

            FilterContrAgentDto filter = new FilterContrAgentDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filter.FromDateTime = fromDateTime.SelectedDate.Value;
                filter.ToDateTime = toDateTime.SelectedDate.Value; 
            }

            filter.Id = _contrAgent.Id;

            var contrAgentPayments = await Task.Run(async () => await _contrAgentPaymentService.FilterAsync(filter));
            ShowContrAgentPayments(contrAgentPayments);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterContrAgentPayments();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllContrAgents();
        }

        private void btnclose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }  
    }
}
