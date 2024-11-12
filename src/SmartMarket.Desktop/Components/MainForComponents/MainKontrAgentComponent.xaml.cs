using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.LoadReports;
using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainKontrAgentComponent.xaml
/// </summary>
public partial class MainKontrAgentComponent : UserControl
{
    private IContrAgentService contrAgentService;
    public Func<Task> GetContrAgents { get; set; }
    public MainKontrAgentComponent()
    {
        InitializeComponent();
        this.contrAgentService = new ContrAgentService();
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
    public void GetData(ContrAgentViewModels contrAgent, int count)
    {
        tbNumber.Text = count.ToString();
        tbCompanyName.Text = contrAgent.CompanyName;  
        tbFirstname.Text = contrAgent.FirstName;  
        tbLastname.Text = contrAgent.LastName;
        tbPhonenumber.Text = contrAgent.PhoneNumber;
    }



    private async void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        var contragent = this.Tag as ContrAgentViewModels;

        //SelectCompanyAndContrAgentWindow selectCompanyAndContrAgentWindow = new SelectCompanyAndContrAgentWindow();
        //selectCompanyAndContrAgentWindow.ShowDialog();

        ContrAgentUpdateWindow updateWindow = new ContrAgentUpdateWindow();
        updateWindow.GetCompany(contragent);
        updateWindow.ShowDialog();
        await GetContrAgents();
    }

    private async void Delete_Button_Click(object sender, RoutedEventArgs e)
    {
        var contragent = this.Tag as ContrAgentViewModels;
        var message = tbFirstname.Text;

        var messageBox = new MessageBoxWindow(message + "ni o'chirilsinmi?", MessageType.Confirmation, MessageButtons.OkCancel);
        var result = messageBox.ShowDialog();

        if (result == true)
        {
            var res = await contrAgentService.DeleteAsync(contragent.Id);

            if (res)
            {
                notifier.ShowInformation("Contr agent muvaffaqiyatli o'chirildi.");
                await GetContrAgents();
            }
            else
                notifier.ShowError("O'chirishda xatolik yuz berdi.");
        }
    }

    private void Payment_Button_Click(object sender, RoutedEventArgs e)
    {
        PaymentKontrAgentWindow paymentKontrAgentWindow = new PaymentKontrAgentWindow();
        paymentKontrAgentWindow.ShowDialog();
    }

    private void History_Button_Click(object sender, RoutedEventArgs e)
    {
        var contrAgent = this.Tag as ContrAgentViewModels;

        if(contrAgent.Id != Guid.Empty)
        {
            LoadReportWindow loadReportWindow = new LoadReportWindow();
            loadReportWindow.SetContrAgentId(contrAgent.Id);
            loadReportWindow.ShowDialog();
        }
        else
        {
            notifier.ShowWarning("Contr agent topilmadi!");
        }
    }
}
