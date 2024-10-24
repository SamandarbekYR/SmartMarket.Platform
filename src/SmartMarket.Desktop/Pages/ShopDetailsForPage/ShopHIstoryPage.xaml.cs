using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage;

public partial class ShopHIstoryPage : Page
{
    private IProductSaleService _productSaleService;
    private SalesRequestService _salesRequestService;
    private IExpenseService _expenseService;
    private ShopDetailsPage _shopDetailsPage;

    public ShopHIstoryPage(ShopDetailsPage shopDetailsPage)
    {
        InitializeComponent();
        _productSaleService = new ProductSaleService();
        _salesRequestService = new SalesRequestService();
        _expenseService = new ExpenseService();
        _shopDetailsPage = shopDetailsPage;
    }

    public async void GetAllProduct()
    {
        St_productList.Children.Clear();

        var productSales = await _salesRequestService.GetAll();

        List<string> workerNames = productSales
            .Select(ps => ps.Worker.FirstName)
            .Distinct()
            .ToList();
        workerNames.Insert(0, "Barcha sotuvchi");
        workerComboBox.ItemsSource = workerNames;

        var today = DateTime.Today;
        var filteredProductSales = productSales.Where(ps => ps.CreatedDate.Date == today).ToList();

        await ShowProductSales(filteredProductSales);
    }

    private async void FilterProductSales()
    {
        St_productList.Children.Clear();

        FilterSalesRequestDto filterProductSaleDto = new FilterSalesRequestDto();

        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterProductSaleDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterProductSaleDto.ToDateTime = toDateTime.SelectedDate.Value;
        }

        var selectedWorkerName = workerComboBox.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Barcha sotuvchi"))
        {
            filterProductSaleDto.WorkerName = selectedWorkerName;
        }

        var searchTerm = searchTextBox.Text.ToLower();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            filterProductSaleDto.ProductName = searchTerm;
        }

        var filteredProductSales = await _salesRequestService.FilterSalesRequest(filterProductSaleDto);

        await ShowProductSales(filteredProductSales);

    }

    private async Task ShowProductSales(IList<SalesRequestDto> productSales)
    {
        productSales = productSales
             .Where(ps => ps.ProductSaleItems.Any(item => item.Count != 0))
             .OrderByDescending(ps => ps.CreatedDate)
             .ToList();


        FilterExpenseDto filterExpenseDto = new FilterExpenseDto();
        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterExpenseDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterExpenseDto.ToDateTime = toDateTime.SelectedDate.Value;
        }

        var expenses = await Task.Run(async () => await _expenseService.FilterExpense(filterExpenseDto));

        var totalSellPrice = productSales.Sum(p => p.TotalCost);
        var totalPrice = productSales.Sum(p =>
                            p.ProductSaleItems.Sum(item =>
                                item.Product.Price * item.Count));
        var profit = totalSellPrice - totalPrice;
        var totalExpense = expenses.Sum(e => e.Amount);
        _shopDetailsPage.SetValuesShopHitory(totalSellPrice, profit, totalExpense);

        St_productList.Children.Clear();
        Loader.Visibility = Visibility.Collapsed;
        int rowNumber = 1;

        if (productSales.Count > 0)
        {
            foreach (var item in productSales)
            {
                foreach (var productSaleItem in item.ProductSaleItems)
                {
                    ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                    shopDetailsProductComponent.Tag = item.ProductSaleItems.FirstOrDefault(p => p.Id == productSaleItem.Id);
                    shopDetailsProductComponent.SetValues(
                        rowNumber,
                        item.TransactionId,
                        productSaleItem.Product.Name,
                        productSaleItem.Product.SellPrice,
                        productSaleItem.Count,
                        productSaleItem.ItemTotalCost);

                    shopDetailsProductComponent.BorderThickness = new Thickness(2);
                    St_productList.Children.Add(shopDetailsProductComponent);
                    rowNumber++;
                }
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterProductSales();
    }

    private void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterProductSales();
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            FilterProductSales();
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllProduct();
    }

}
