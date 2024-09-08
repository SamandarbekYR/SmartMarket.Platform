using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainKontrAgentComponent.xaml
    /// </summary>
    public partial class MainKontrAgentComponent : UserControl
    {
        private IContrAgentService contrAgentService;
        public MainKontrAgentComponent()
        {
            InitializeComponent();
            this.contrAgentService = new ContrAgentService();
        }


        public void GetData(long Number,string CompanyName,string FirstName, string LastName,string PhoneNumber,decimal DebtSum,decimal PayedSum,decimal LastPayedSum,string LastPayDate)
        {
            tbNumber.Text=Number.ToString();
            tbCompanyName.Text=CompanyName.ToString();  
            tbFirstname.Text=FirstName.ToString();  
            tbLastname.Text=LastName.ToString();
            tbPhonenumber.Text=PhoneNumber.ToString();
            tbDebtAmount.Text=DebtSum.ToString();
            tbPayedSum.Text=PayedSum.ToString();
            tbLastPayedSum.Text=LastPayedSum.ToString();
            tbLastPayedDate.Text=LastPayDate.ToString();
        }



        private async void btnedit_Click(object sender, RoutedEventArgs e)
        {
            var contragent = this.Tag as ContrAgentViewModels;

            //SelectCompanyAndContrAgentWindow selectCompanyAndContrAgentWindow = new SelectCompanyAndContrAgentWindow();
            //selectCompanyAndContrAgentWindow.ShowDialog();

            ContrAgentUpdateWindow updateWindow = new ContrAgentUpdateWindow();
            updateWindow.GetCompany(contragent);
            updateWindow.ShowDialog();



        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var contragent = this.Tag as ContrAgentViewModels;


            var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                await contrAgentService.DeleteAsync(contragent.Id);
            }
        }
    }
}
