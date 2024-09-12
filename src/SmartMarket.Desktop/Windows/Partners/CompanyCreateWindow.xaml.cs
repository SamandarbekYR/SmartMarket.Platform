using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
    }
}
