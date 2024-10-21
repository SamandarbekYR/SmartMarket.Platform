using SmartMarket.Desktop.Components.AccountSettingsForComponent;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Desktop.Windows.Position;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarketDeskop.Integrated.Services.Workers.Worker;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.AccountSettingsForPage;

/// <summary>
/// Interaction logic for AccountSettingsPage.xaml
/// </summary>
public partial class AccountSettingsPage : Page
{
    private IWorkerService _workerService;
    public AccountSettingsPage()
    {
        InitializeComponent();
        _workerService = new WorkerService();
    }

    public async Task GetAllWorkers()
    {
        var workers = await Task.Run(() => _workerService.GetAllAsync());
        ShowWorkers(workers);
    }

    private void ShowWorkers(IList<WorkerDto> workers)
    {
        Wr_Account.Children.Clear();
        Loader.Visibility = Visibility.Collapsed;

        if (workers.Count > 0)
        {
            foreach (var item in workers)
            {
                AccountSettingsComponent accountSettingsComponent = new AccountSettingsComponent();
                accountSettingsComponent.Tag = item;
                accountSettingsComponent.SetData(item);
                Wr_Account.Children.Add(accountSettingsComponent);
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var searchTerm = txtSearch.Text;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var workers = await Task.Run(() => _workerService.GetWorkerByName(searchTerm));
                ShowWorkers(workers);
            }
            else
            {
                await GetAllWorkers();
            }
        }
    }

    private void btnAddAccount_Click(object sender, RoutedEventArgs e)
    {
        AccountCreateWindow accountCreateWindow = new AccountCreateWindow();
        accountCreateWindow.ShowDialog();
    }

    private void btnAddPosition_Click(object sender, RoutedEventArgs e)
    {
        PositionCreateWindow positionCreateWindow = new PositionCreateWindow();
        positionCreateWindow.ShowDialog();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllWorkers();
    }
}
