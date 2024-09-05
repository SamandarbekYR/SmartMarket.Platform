using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
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
using ToastNotifications;

namespace SmartMarket.Desktop.Windows.ContrAgents
{
    /// <summary>
    /// Interaction logic for ContrAgentCreateWindow.xaml
    /// </summary>
    public partial class ContrAgentCreateWindow : Window
    {
        private IContrAgentService contrAgentService;
        private IPartnerCompanyService partnerCompanyService;


        List<PartnerCompanyView> partnerCompanyViews=new List<PartnerCompanyView>();
        public ContrAgentCreateWindow()
        {
            InitializeComponent();
            contrAgentService = new ContrAgentService();
            this.partnerCompanyService=new PartnerCompanyService();


            GetAllCompany();
        }

        //Notifier notifier = new Notifier(cfg =>
        //{
        //    cfg.PositionProvider = new WindowPositionProvider(
        //        parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
        //        corner: Corner.TopRight,
        //        offsetX: 20,
        //        offsetY: 20);

        //    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
        //        notificationLifetime: TimeSpan.FromSeconds(3),
        //        maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        //    cfg.Dispatcher = Application.Current.Dispatcher;
        //});

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContrAgentDto contrAgentDto = new ContrAgentDto();

            {
                PartnerCompanyView  partnerCompany= partnerCompanyViews.Where(x => x.Name==Combo_CompanyName.SelectedValue).FirstOrDefault();
                contrAgentDto.CompanyId=partnerCompany.Id;    
            }
            contrAgentDto.FirstName=txtFirstName.Text;  
            contrAgentDto.LastName=txtLastName.Text;
            contrAgentDto.PhoneNumber=txtPhoneNumber.Text;  

            await contrAgentService.AddAsync(contrAgentDto);
            Clear();
            this.Close();

        }



        public async void GetAllCompany()
        {
            partnerCompanyViews = await partnerCompanyService.GetAllCompany();

            if(partnerCompanyViews!=null && partnerCompanyViews.Any() )
            {
                Combo_CompanyName.ItemsSource = partnerCompanyViews.Select(a=>a.Name);
                Combo_CompanyName.Items.Refresh();
            }
        }

        public void Clear()
        {
           txtFirstName.Text=txtLastName.Text=txtPhoneNumber.Text=string.Empty;
               
        }
    }
}
