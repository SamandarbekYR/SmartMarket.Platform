using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Services.Expenses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for CargoReportPage.xaml
    /// </summary>
    public partial class CargoReportPage : Page
    {
        private readonly ILoadReportService loadReportService;
        public CargoReportPage()
        {
            InitializeComponent();
            this.loadReportService = new LoadReportService();
        }

        public async void GetAllCargoReport()
        {
            St_CargoReports.Children.Clear();

            var loadReports = await Task.Run(async () => await loadReportService.GetAll());

            List<string> workerNames = loadReports
                .Select(x => x.ContrAgent.PartnerCompany.Name)
                .Distinct()
                .ToList();

            foreach(var worker in workerNames)
            {
                companyComboBox.Items.Add(worker);
            }

            showLoadReport(loadReports);
        }

        private async void FilterLoadReport()
        {
            Loader.Visibility = Visibility.Visible;
            St_CargoReports.Children.Clear();
            FilterLoadReportDto loadReportDto = new FilterLoadReportDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                loadReportDto.FromDateTime = fromDateTime.SelectedDate.Value;
                loadReportDto.ToDateTime = toDateTime.SelectedDate.Value;
            }

            if(companyComboBox.SelectedItem != null)
            {
                var selectedCompanyName = companyComboBox.SelectedItem?.ToString();

                if(!string.IsNullOrEmpty(selectedCompanyName) && !selectedCompanyName.Equals("Sotuvchi"))
                {
                    loadReportDto.CompanyName = selectedCompanyName;
                }
            }

            if(!string.IsNullOrEmpty(filterTextBox.Text))
            {
                loadReportDto.ProductName = filterTextBox.Text;
            }

            var filterLoadReports = await Task.Run(async () => await loadReportService.FilterAsync(loadReportDto));
            showLoadReport(filterLoadReports);
        }

        private void showLoadReport(IEnumerable<LoadReportDto> loadReports)
        {
            St_CargoReports.Children.Clear();

            Loader.Visibility = Visibility.Collapsed;

            int count = 1;

            if (loadReports != null)
            {
                foreach (var report in loadReports)
                {
                    CargoReportComponent cargoReportComponent = new CargoReportComponent();
                    cargoReportComponent.tbNumber.Text = count.ToString();
                    cargoReportComponent.SetData(report);
                    St_CargoReports.Children.Add(cargoReportComponent);
                    count++;
                }
            }
            else 
            {
                EmptyDataLoadReport.Visibility = Visibility.Visible;
            }
        }


        private void companyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLoadReport();
        }

        private void toDateTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLoadReport();
        }

        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                FilterLoadReport();
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        { 
            GetAllCargoReport();
        }
    }
}
