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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllCompany();
            EnableBlur();
        }
    }
}
