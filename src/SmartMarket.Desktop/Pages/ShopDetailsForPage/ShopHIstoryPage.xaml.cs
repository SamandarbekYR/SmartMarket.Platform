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
    private IExpenseService _expenseService;
    private ShopDetailsPage _shopDetailsPage;

    public ShopHIstoryPage(ShopDetailsPage shopDetailsPage)
    {
        InitializeComponent();
        _productSaleService = new ProductSaleService();
        _expenseService = new ExpenseService();
        _shopDetailsPage = shopDetailsPage;
    }

    public async void GetAllProduct()
    {
        St_productList.Children.Clear();

        var productSales = await _productSaleService.GetAllAsync();

        List<string> workerNames = productSales
            .Select(ps => ps.SalesRequest.Worker.FirstName)
            .Distinct()
            .ToList();
        workerNames.Insert(0, "Barcha sotuvchi");
        workerComboBox.ItemsSource = workerNames;

        var today = DateTime.Today;
        var filteredProductSales = productSales.Where(ps => ps.CreatedDate?.Date == today).ToList();

        await ShowProductSales(filteredProductSales);
    }

    private async void FilterProductSales()
    {
        St_productList.Children.Clear();

        FilterProductSaleDto filterProductSaleDto = new FilterProductSaleDto();

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

        var filteredProductSales = await _productSaleService.FilterProductSaleAsync(filterProductSaleDto);

        await ShowProductSales(filteredProductSales);

    }

    private async Task ShowProductSales(IList<ProductSaleViewModel> productSales)
    {
        productSales = productSales
             .Where(ps => ps.Count > 0)
             .OrderByDescending(ps => ps.CreatedDate)
             .ToList();


        FilterExpenseDto filterExpenseDto = new FilterExpenseDto();
        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterExpenseDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterExpenseDto.ToDateTime = toDateTime.SelectedDate.Value;
        }


        var expenses = await _expenseService.FilterExpense(filterExpenseDto);

        var totalCost = productSales.Sum(p => p.ItemTotalCost);
        var profit = productSales.Sum(p => (p.Product.SellPrice - p.Product.Price) * p.Count);
        var totalExpense = expenses.Sum(e => e.Amount);
        _shopDetailsPage.SetValuesShopHitory(totalCost, profit, totalExpense);

        St_productList.Children.Clear();
        Loader.Visibility = Visibility.Collapsed;
        int rowNumber = 1;

        if (productSales.Count > 0)
        {
            foreach (var item in productSales)
            {
                ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                shopDetailsProductComponent.Tag = item;
                shopDetailsProductComponent.SetValues(
                    rowNumber,
                    item.SalesRequest.TransactionId,
                    item.Product.Name,
                    item.Product.SellPrice,
                    item.Count,
                    item.ItemTotalCost);

                shopDetailsProductComponent.BorderThickness = new Thickness(2);
                St_productList.Children.Add(shopDetailsProductComponent);
                rowNumber++;
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
