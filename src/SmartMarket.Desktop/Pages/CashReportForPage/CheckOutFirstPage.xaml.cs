using SmartMarket.Service.DTOs.Products.SalesRequest;

using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;

using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.CashReportForPage
{
    /// <summary>
    /// Interaction logic for CheckOutFirstPage.xaml
    /// </summary>
    public partial class CheckOutFirstPage : Page
    {
        private IExpenseService _expenseService;
        private ISalesRequestsService _salesRequestsService;

        public CheckOutFirstPage()
        {
            InitializeComponent();
            _expenseService = new ExpenseService();
            _salesRequestsService = new SalesRequestService();
        }

        private async void SalesMoney()
        {

            var salesMoney = await _salesRequestsService.GetAll();
            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                salesMoney = salesMoney.Where(
                    sr => sr.CreatedDate >= fromDateTime.SelectedDate 
                    && sr.CreatedDate <= toDateTime.SelectedDate).ToList();
            }
            else
            {
                 salesMoney = salesMoney.Where(
                     sr => sr.CreatedDate!.Value.Date == DateTime.Today).ToList();
            }

            var cashSum = salesMoney.Sum(sr => sr.CashSum);
            var cardSum = salesMoney.Sum(sr => sr.CardSum);
            var debtSum = salesMoney.Sum(sr => sr.DebtSum);
            var transferMoney = salesMoney.Sum(sr => sr.TransferMoney);

            Label_Naqd_Savdo.Content = cashSum;
            Label_Karta_Savdo.Content = cardSum;
            Label_Pul_Savdo.Content = transferMoney;
            Label_Nasiya_Savdo.Content = debtSum;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SalesMoney();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //var products = _productSaleService.GetProducts();
            //foreach (var product in products)
            //{
            //    RunningProductComponent runningProductComponent = new RunningProductComponent();
            //    runningProductComponent.SetData(product);
            //    spProducts.Children.Add(runningProductComponent);
            //}
        }


    }
}
