using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clear();
            this.Close();
        }

        private async void Br_Change_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtFirstName.Text) &&
               !string.IsNullOrEmpty(txtLastName.Text) &&
               !string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                ContrAgentDto contrAgentDto = new ContrAgentDto();
                {
                    PartnerCompanyView partnerCompany = partnerCompanyViews.Where(x => x.Name == Combo_Company.SelectedValue.ToString()).FirstOrDefault();
                    contrAgentDto.CompanyId = partnerCompany.Id;
                }
                contrAgentDto.FirstName = txtFirstName.Text;
                contrAgentDto.LastName = txtLastName.Text;
                contrAgentDto.PhoneNumber = txtPhoneNumber.Text;

                var res = await contrAgentService.UpdateAsync(contrAgentDto,_contrAgentViewModels.Id);

                if(res)
                {
                    Clear();
                    this.Close();
                    notifier.ShowInformation("Agent malumotlari yangilandi.");
                }
                else
                {
                    notifier.ShowError("Agentni o'zgartirishda xatolik yuz berdi");
                }
            }
            else
            {
                notifier.ShowWarning("Agent malumotlari to'liq emas!");
            }
        }

        public async void GetContrAgent()
        {
            partnerCompanyViews=await partnerCompanyService.GetAllCompany();
            if(partnerCompanyViews!=null && partnerCompanyViews.Any() )
            {
                Combo_Company.ItemsSource = partnerCompanyViews.Select(a=>a.Name);
                Combo_Company.Items.Refresh();
            }

            Combo_Company.SelectedValue = _contrAgentViewModels.CompanyName;
            txtFirstName.Text = _contrAgentViewModels.FirstName;
            txtLastName.Text = _contrAgentViewModels.LastName;
            txtPhoneNumber.Text = _contrAgentViewModels.PhoneNumber;
        }

        public void GetCompany(ContrAgentViewModels contrAgentView)
        {
            _contrAgentViewModels = contrAgentView;
        }

        public void Clear()
        {
            txtFirstName.Text=txtLastName.Text=txtPhoneNumber.Text=string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetContrAgent();
            EnableBlur();
        }
    }
}
