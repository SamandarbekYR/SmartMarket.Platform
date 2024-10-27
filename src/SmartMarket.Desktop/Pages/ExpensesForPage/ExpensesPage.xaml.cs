using SmartMarket.Domain.Entities.Products;
using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for ExpensesPage.xaml
    /// </summary>
    public partial class ExpensesPage : Page
    {
        private readonly IExpenseService expenceService;
        private readonly IProductService productService;
        private readonly ILoadReportService loadReportService;
        private readonly ISalesRequestsService salesRequestsService;
        public ExpensesPage()
        {
            InitializeComponent();
            ExpenseListPage expenseListPage = new ExpenseListPage();
            ShopDetailsPageNavigator.Content = expenseListPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);
            this.expenceService = new ExpenseService();
            this.productService = new ProductService();
            this.loadReportService = new LoadReportService();
            this.salesRequestsService = new SalesRequestService();
        }

        private void btnExpenses_Click(object sender, RoutedEventArgs e)
        {
            ExpenseListPage expenseListPage = new ExpenseListPage();
            ShopDetailsPageNavigator.Content = expenseListPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);
        }

        private void btnAllProduct_Click(object sender, RoutedEventArgs e)
        {
            AllProductsPage allProductsPage = new AllProductsPage();
            ShopDetailsPageNavigator.Content =allProductsPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);

        }

        private void btnRunningOutOfProduct_Click(object sender, RoutedEventArgs e)
        {
            RunningOutOfProductsPage runningOutOfProductsPage = new RunningOutOfProductsPage();
            ShopDetailsPageNavigator.Content=runningOutOfProductsPage;
            DisableForOne(BrRunningOutProduct);
        }

        private void btnCargoReport_Click(object sender, RoutedEventArgs e)
        {
            CargoReportPage cargoReportPage = new CargoReportPage();
            ShopDetailsPageNavigator.Content = cargoReportPage;
            DisableForOne(BrCargoReport);
        }

        public void DisableForOne(Border border)
        {
            BrAllExpenses.Visibility = Visibility.Collapsed;
            BrPaymentSum.Visibility = Visibility.Collapsed;
            BrRunningOutProduct.Visibility = Visibility.Collapsed;
            BrCargoReport.Visibility = Visibility.Collapsed;

            border.Visibility = Visibility.Visible;
        }

        public void DisableForTwo(Border border,Border border1)
        {
            BrAllExpenses.Visibility = Visibility.Collapsed;
            BrPaymentSum.Visibility = Visibility.Collapsed;
            BrRunningOutProduct.Visibility = Visibility.Collapsed;
            BrCargoReport.Visibility = Visibility.Collapsed;

            border.Visibility = Visibility.Visible;
            border1.Visibility = Visibility.Visible;    
        }

        private async void GetExpenseSummary()
        {
            var expenseSummary = await expenceService.GetExpenseSummary();

            if(expenseSummary == null)
            {
                expenseTotalCash.Text = "0";
                expenseTotalCard.Text = "0";
                expenseTotalAmount.Text = "0";
            }

            expenseTotalCash.Text = expenseSummary.TotalCash.ToString();
            expenseTotalCard.Text = expenseSummary.TotalCard.ToString();
            expenseTotalAmount.Text = expenseSummary.TotalAmount.ToString();
        }

        private async void GetRunningOutOfProductCount()
        {
            int count = 0;
            var runningOutOfProduct = await productService.GetFinishedProducts();

            if(runningOutOfProduct.Any())
            {
                foreach(var runningProduct in runningOutOfProduct)
                {
                    count++;
                }
            }

            runningOutOfProductCount.Text = count.ToString();
        }

        private async void GetLoadReportStatistics()
        {
            int count = 0;
            var loadReportStatistics = await loadReportService.GetStatisticsAsync();

            if(loadReportStatistics != null)
            {
                loadReportCount.Text = "Olingan yukalar soni: " + loadReportStatistics.Count.ToString();
                loadReportProductCount.Text = loadReportStatistics.ProductCount.ToString();
                loadReportProductCountKG.Text = loadReportStatistics.ProductCountKG.ToString();
                loadReportTotalAmount.Text = loadReportStatistics.TotalAmount.ToString();
            }
            else
            {
                loadReportProductCount.Text = "0";
                loadReportProductCountKG.Text = "0";
                loadReportTotalAmount.Text = "0";
            }
        }

        private async void GetSaleProductStatistics()
        {
            double cashAmount = 0, cardAmount = 0, totalAmount = 0;
            var salesRequests = await salesRequestsService.GetAll();

            if (salesRequests.Any())
            {
                foreach (var salesRequest in salesRequests)
                {
                    cashAmount += salesRequest.CashSum;
                    cardAmount += salesRequest.CardSum;
                    totalAmount += salesRequest.TotalCost;
                }

                saleProductCashAmount.Text = cashAmount.ToString();
                saleProductCardAmount.Text = cardAmount.ToString();
                saleProductTotalAmount.Text = totalAmount.ToString();
                saleProductBenefit.Text = totalAmount.ToString();
            }
            else
            {
                saleProductCashAmount.Text = "0";
                saleProductCardAmount.Text = "0";
                saleProductTotalAmount.Text = "0";
                saleProductBenefit.Text = "0";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetExpenseSummary();
            GetRunningOutOfProductCount();
            GetLoadReportStatistics();
            GetSaleProductStatistics();
        }
    }
}
