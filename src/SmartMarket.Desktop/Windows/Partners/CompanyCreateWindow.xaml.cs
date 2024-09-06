using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
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

namespace SmartMarket.Desktop.Windows.Partners
{
    /// <summary>
    /// Interaction logic for CompanyCreateWindow.xaml
    /// </summary>
    public partial class CompanyCreateWindow : Window
    {
        private IPartnerCompanyService partnerCompanyService;
        public CompanyCreateWindow()
        {
            InitializeComponent();
            this.partnerCompanyService = new PartnerCompanyService();
        }

        private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PartnerCompanyDto partnerCompanyDto = new PartnerCompanyDto();
            partnerCompanyDto.Name=txtCompanyName.Text;
            partnerCompanyDto.PhoneNumber=txtPhoneNumber.Text;
            partnerCompanyDto.Describtion=txtDescribtion.Text;

            await partnerCompanyService.CreateCompany(partnerCompanyDto);
            Clear();
            this.Close();   
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clear();
            this.Close();
        }

        public void Clear()
        {
            txtPhoneNumber.Text=txtDescribtion.Text=txtCompanyName.Text=string.Empty;   
        }
    }
}
