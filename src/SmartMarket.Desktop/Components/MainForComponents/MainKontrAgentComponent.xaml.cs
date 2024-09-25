using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.MainForComponents;

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


    public void GetData(ContrAgentViewModels contrAgent)
    {
        tbCompanyName.Text = contrAgent.CompanyName;  
        tbFirstname.Text = contrAgent.FirstName;  
        tbLastname.Text = contrAgent.LastName;
        tbPhonenumber.Text = contrAgent.PhoneNumber;
        tbDebtAmount.Text = contrAgent.DebtSum.ToString();
        tbPayedSum.Text = contrAgent.PayedSum.ToString();
        tbLastPayedSum.Text = contrAgent.LastPayedSum.ToString();
        tbLastPayedDate.Text = contrAgent.LastPayedDate;
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
