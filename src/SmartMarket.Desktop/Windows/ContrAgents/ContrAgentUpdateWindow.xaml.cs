using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
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
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.ContrAgents
{
    /// <summary>
    /// Interaction logic for ContrAgentUpdateWindow.xaml
    /// </summary>
    public partial class ContrAgentUpdateWindow : Window
    {
        private IContrAgentService contrAgentService;
        private IPartnerCompanyService partnerCompanyService;
        List<PartnerCompanyView> partnerCompanyViews=new List<PartnerCompanyView>();
        ContrAgentViewModels _contrAgentViewModels;

        public ContrAgentUpdateWindow()
        {
            InitializeComponent();
            this.contrAgentService = new ContrAgentService();
            this.partnerCompanyService = new PartnerCompanyService();

            GetAllCompany();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clear();
            this.Close();
        }

        private async void Br_Change_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContrAgentDto contrAgentDto = new ContrAgentDto();

            {
                PartnerCompanyView partnerCompany = partnerCompanyViews.Where(x => x.Name == Combo_Company.SelectedValue).FirstOrDefault();
                contrAgentDto.CompanyId = partnerCompany.Id;
            }
            contrAgentDto.FirstName = txtFirstName.Text;
            contrAgentDto.LastName = txtLastName.Text;
            contrAgentDto.PhoneNumber = txtPhoneNumber.Text;

           await contrAgentService.UpdateAsync(contrAgentDto,_contrAgentViewModels.Id);
            Clear();
            this.Close();
        }

        public async void GetAllCompany()
        {
            partnerCompanyViews=await partnerCompanyService.GetAllCompany();
            if(partnerCompanyViews!=null && partnerCompanyViews.Any() )
            {
                Combo_Company.ItemsSource = partnerCompanyViews.Select(a=>a.Name);
                Combo_Company.Items.Refresh();
            }
        }

        public void GetCompany(ContrAgentViewModels contrAgentView)
        {
            _contrAgentViewModels = contrAgentView;
        }

        public void Clear()
        {
            txtFirstName.Text=txtLastName.Text=txtPhoneNumber.Text=string.Empty;
        }
    }
}
