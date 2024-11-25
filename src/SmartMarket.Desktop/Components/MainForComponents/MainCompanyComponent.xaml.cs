using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainCompanyComponent.xaml
    /// </summary>
    public partial class MainCompanyComponent : UserControl
    {
        private IPartnerCompanyService _partnerCompanyService;
        public Func<Task> GetAllCompanies { get; set; }
        public bool selected { get; set; } = true;
        public MainCompanyComponent()
        {
            InitializeComponent();
            this._partnerCompanyService = new PartnerCompanyService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 50,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public void SetValues(PartnerCompanyView company, int count)
        {
            tbNumber.Text = count.ToString();
            tbName.Text = company.Name;
        }

        private async void BntDeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            var partnerCompanyView = this.Tag as PartnerCompanyView;
            var message = tbName.Text;

            var messageBox = new MessageBoxWindow($"{message}ni o'chirilsinmi?", MessageBoxWindow.MessageType.Confirmation, MessageBoxWindow.MessageButtons.OkCancel);
            var result = messageBox.ShowDialog();

            if(result == true)
            {
                var res = await _partnerCompanyService.DeleteCompany(partnerCompanyView!.Id);

                if(res)
                {
                    notifier.ShowSuccess($"{message} muvaffaqiyatli o'chirildi!");
                    await GetAllCompanies();
                }
                else
                {
                    notifier.ShowError($"{message} o'chirishda xatolik yuz berdi!");
                }
            }
        }

        private async void BtnEditCompany_Click(object sender, RoutedEventArgs e)
        {
            var partnerCompanyView = this.Tag as PartnerCompanyView;
            CompanyUpdateWindow window = new CompanyUpdateWindow();
            window.GetData(partnerCompanyView);
            window.ShowDialog();
            await GetAllCompanies();
        }
    }
}
