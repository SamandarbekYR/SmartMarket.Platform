using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage
{
    /// <summary>
    /// Interaction logic for CollectedCargoDetailsPage.xaml
    /// </summary>
    public partial class CollectedCargoDetailsPage : Page
    {
        private readonly ISalesRequestsService _loadReportService;
        public CollectedCargoDetailsPage()
        {
            InitializeComponent();
            this._loadReportService = new SalesRequestService();
        }

        private async Task GetAllLoadReports()
        {
            St_loadReports.Children.Clear();

            var loadReports = await Task.Run(async () => await _loadReportService.GetAll());

            List<string> workerNames = loadReports
                .Select(x => x.Worker.FirstName)
                .Distinct()
                .ToList();

            foreach(var workerName in workerNames)
            {
                workerComboBox.Items.Add(new ComboBoxItem { Content = workerName });
            }

            ShowLoadReports(loadReports.ToList());
        }

        private async void FilterLoadReports()
        {
            Loader.Visibility = Visibility.Visible;
            St_loadReports.Children.Clear();

            FilterSalesRequestDto filter = new FilterSalesRequestDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filter.FromDateTime = fromDateTime.SelectedDate.Value;
                filter.ToDateTime = toDateTime.SelectedDate.Value;
            }

            if (workerComboBox.SelectedItem != null)
            {
                var selectionWorkerName = workerComboBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectionWorkerName) && !selectionWorkerName.Equals("Sotuvchi"))
                {
                    filter.WorkerName = selectionWorkerName;
                }
            }

            if (filterTextBox != null)
            {
                filter.ProductName = filterTextBox.Text;
            }

            var filterLoadReports = await Task.Run(async () => await _loadReportService.FilterSalesRequest(filter));
            ShowLoadReports(filterLoadReports.ToList());
        }

        private void ShowLoadReports(List<SalesRequestDto> loadReports)
        {
            Loader.Visibility = Visibility.Collapsed;
            int count = 1;

            if(loadReports.Any())
            {
                foreach(var loadReport in loadReports)
                {
                    CollectedCargoDetailsComponent collectedCargoDetailsComponent = new CollectedCargoDetailsComponent();
                    collectedCargoDetailsComponent.SetData(loadReport, count);
                    St_loadReports.Children.Add(collectedCargoDetailsComponent);
                    count++;
                }
            }
            else
            {
                EmptyDataLoadReport.Visibility = Visibility.Visible;
            }
        }
        private void toDateTime_SelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLoadReports();
        }

        private void workerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLoadReports();
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                FilterLoadReports();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAllLoadReports();
        }
    }
}
