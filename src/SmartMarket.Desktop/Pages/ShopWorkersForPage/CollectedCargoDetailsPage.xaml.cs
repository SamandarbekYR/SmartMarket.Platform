using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xaml;

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

            ShowLoadReports(loadReports.ToList());
        }

        public async void SelectLoadReportsByWorker(WorkerListComponent component, WorkerDto worker)
        {
            EmptyDataLoadReport.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
            St_loadReports.Children.Clear();

            var loadReports = await Task.Run(async () => await _loadReportService.GetAll());

            List<SalesRequestDto> results = loadReports
                .Where(x => x.Worker.FirstName == worker.FirstName)
                .ToList();

            ShowLoadReports(results);
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

            //if (workerComboBox.SelectedItem != null)
            //{
            //    var selectionWorkerName = workerComboBox.SelectedItem?.ToString();

            //    if (!string.IsNullOrEmpty(selectionWorkerName) && !selectionWorkerName.Equals("Sotuvchi"))
            //    {
            //        filter.WorkerName = selectionWorkerName;
            //    }
            //}

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
            var shipments = loadReports.Where(x => x.IsShipment == true);

            if (shipments.Any())
            {
                foreach(var loadReport in shipments)
                {
                    CollectedCargoDetailsComponent collectedCargoDetailsComponent = new CollectedCargoDetailsComponent();
                    collectedCargoDetailsComponent.SetData(loadReport, count);
                    collectedCargoDetailsComponent.Tag = loadReport;
                    St_loadReports.Children.Add(collectedCargoDetailsComponent);
                    count++;
                }
            }
            else
            {
                EmptyDataLoadReport.Visibility = Visibility.Visible;
                EmptyDataLoadReport.Content = "Ma'lumot topilmadi.";
            }
        }
        private void toDateTime_SelectedDataChanged(object sender, SelectionChangedEventArgs e)
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

        private CollectedCargoDetailsComponent selectedControl = null!;
        public void SelectCargo(CollectedCargoDetailsComponent cargo)
        {
            if (selectedControl != null)
            {
                selectedControl.Cargo_Border.Background = Brushes.White;
            }

            cargo.Cargo_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));

            selectedControl = cargo;
        }
    }
}
