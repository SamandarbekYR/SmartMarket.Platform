using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.ContrAgents;

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
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (CreateButtonContainer.IsEnabled == false)
            return;

        CreateButtonContainer.IsEnabled = false;

        if(!string.IsNullOrEmpty(txtFirstName.Text) &&
            !string.IsNullOrEmpty(txtLastName.Text) &&
            !string.IsNullOrEmpty(txtPhoneNumber.Text))
        {
            ContrAgentDto contrAgentDto = new ContrAgentDto();

            {
                PartnerCompanyView  partnerCompany= partnerCompanyViews.Where(x => x.Name==Combo_CompanyName.SelectedValue).FirstOrDefault();
                contrAgentDto.CompanyId=partnerCompany.Id;    
            }
            contrAgentDto.FirstName=txtFirstName.Text;  
            contrAgentDto.LastName=txtLastName.Text;
            contrAgentDto.PhoneNumber=txtPhoneNumber.Text;  

            var res = await contrAgentService.AddAsync(contrAgentDto);
            if (res)
            {
                this.Close();
                notifier.ShowInformation("Agent muvaffaqiyatli yaratildi.");
            }
            else
            {
                notifier.ShowError("Agent qo'shishda xatolik yuz berdi");
                CreateButtonContainer.IsEnabled = true;
            }
        }
        else
        {
            notifier.ShowWarning("Agent malumotlari to'liq emas!");
            CreateButtonContainer.IsEnabled = true;
        }
    }

    public async void GetAllCompany()
    {
        partnerCompanyViews = await Task.Run(async () => await partnerCompanyService.GetAllCompany());

        if(partnerCompanyViews!=null && partnerCompanyViews.Any() )
        {
            Combo_CompanyName.ItemsSource = partnerCompanyViews.Select(a=>a.Name);
            Combo_CompanyName.Items.Refresh();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllCompany();
        EnableBlur();
    }

    private void btn_Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
