using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ExpensesForPage;

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

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await Task.WhenAll(
            Task.Run(() => GetExpenseSummary()),
            Task.Run(() => GetRunningOutOfProductCount()),
            Task.Run(() => GetLoadReportStatistics()),
            Task.Run(() => GetSaleProductStatistics())
        );
    }

    private async Task GetExpenseSummary()
    {
        var expenseSummary = await expenceService.GetExpenseSummary();

        if (expenseSummary == null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                expenseTotalCash.Text = "0";
                expenseTotalCard.Text = "0";
                expenseTotalAmount.Text = "0";
            });
        }
        else
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                expenseTotalCash.Text = expenseSummary.TotalCash.ToString();
                expenseTotalCard.Text = expenseSummary.TotalCard.ToString();
                expenseTotalAmount.Text = expenseSummary.TotalAmount.ToString();
            });
        }
    }

    private async Task GetRunningOutOfProductCount()
    {
        var runningOutOfProduct = await productService.GetFinishedProducts();
        Application.Current.Dispatcher.Invoke(() =>
        {
            runningOutOfProductCount.Text = runningOutOfProduct.Count.ToString();
        });
    }

    private async Task GetLoadReportStatistics()
    {
        var loadReportStatistics = await loadReportService.GetStatisticsAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            if (loadReportStatistics != null)
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
        });
    }

    private async Task GetSaleProductStatistics()
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

            Application.Current.Dispatcher.Invoke(() =>
            {
                saleProductCashAmount.Text = cashAmount.ToString();
                saleProductCardAmount.Text = cardAmount.ToString();
                saleProductTotalAmount.Text = totalAmount.ToString();
                saleProductBenefit.Text = totalAmount.ToString();
            });
        }
        else
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                saleProductCashAmount.Text = "0";
                saleProductCardAmount.Text = "0";
                saleProductTotalAmount.Text = "0";
                saleProductBenefit.Text = "0";
            });
        }
    }


}
