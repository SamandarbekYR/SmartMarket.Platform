using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;

using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;

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
        private ContrAgentDto _contrAgent;
        public PaymentContrAgentWindow(ContrAgentDto contrAgent)
        {
            InitializeComponent();
            _contrAgent = contrAgent;
            _contrAgentService = new ContrAgentService();
        }

        private async void GetAllContrAgents()
        {
            //var contrAgentPayments = _contrAgent.Conter
            //foreach (var contrAgent in contrAgents)
            //{
            //}
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
            }
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
