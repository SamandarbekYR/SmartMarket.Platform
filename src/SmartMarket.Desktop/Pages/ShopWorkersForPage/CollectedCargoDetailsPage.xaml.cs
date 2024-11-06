using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Services.Expenses;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage
{
    /// <summary>
    /// Interaction logic for CollectedCargoDetailsPage.xaml
    /// </summary>
    public partial class CollectedCargoDetailsPage : Page
    {
        private readonly ILoadReportService _loadReportService;
        public CollectedCargoDetailsPage()
        {
            InitializeComponent();
            this._loadReportService = new LoadReportService();
        }

        private async Task GetAllLoadReports()
        {
            St_loadReports.Children.Clear();

            var loadReports = await Task.Run(async () => await _loadReportService.GetAllCollected());

            ShowLoadReports(loadReports);
        }

        private void ShowLoadReports(List<CollectedLoadReportDto> loadReports)
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAllLoadReports();
        }
    }
}
