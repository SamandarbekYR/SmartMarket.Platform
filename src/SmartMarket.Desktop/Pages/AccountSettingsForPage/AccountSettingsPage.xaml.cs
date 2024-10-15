using SmartMarket.Desktop.Components.AccountSettingsForComponent;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Desktop.Windows.Position;
using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Services.Workers.Worker;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.AccountSettingsForPage
{
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
            GetAllWorkers();
        }

        public async void GetAllWorkers()
        {
            var workers = await _workerService.GetAllAsync();
            ShowWorkers(workers);
        }

        private void ShowWorkers(IEnumerable<WorkerDto> workers)
        {
            Wr_Account.Visibility = Visibility.Visible;
            Wr_Account.Children.Clear();

            foreach (var item in workers)
            {
                AccountSettingsComponent accountSettingsComponent = new AccountSettingsComponent();
                accountSettingsComponent.Tag = item;
                accountSettingsComponent.SetData(item.FirstName, item.LastName, item.Position.Name);
                accountSettingsComponent.BorderThickness = new Thickness(15, 5, 15, 5);
                Wr_Account.Children.Add(accountSettingsComponent);
            }
        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchTerm = txtSearch.Text;
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var workers = await _workerService.GetWorkerByName(searchTerm);
                    ShowWorkers(workers);
                }
                else
                {
                    GetAllWorkers();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllWorkers();
        }

        private void btnAddPosition_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
