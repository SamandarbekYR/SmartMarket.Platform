using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarketDeskop.Integrated.Services.Products.ReplaceProduct;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage;

/// <summary>
/// Interaction logic for ReturnedCargoPage.xaml
/// </summary>
public partial class ReturnedCargoPage : Page
{
    private IReplaceProductService _replaceProductService;
    private ShopDetailsPage _shopDetailsPage;
    public ReturnedCargoPage(ShopDetailsPage shopDetailsPage)
    {
        InitializeComponent();
        _replaceProductService = new ReplaceProductService();
        _shopDetailsPage = shopDetailsPage;
    }

    public async void GetAllProduct()
    {
        St_ReturnedProducts.Children.Clear();

        var replaceProducts = await Task.Run(async () => await _replaceProductService.GetAllAsync());

        List<string> workerNames = replaceProducts
            .Select(ps => ps.Worker!.FirstName)
            .Distinct()
            .ToList();
        workerNames.Insert(0, "Barcha sotuvchi");
        workerComboBox.ItemsSource = workerNames;

        var today = DateTime.Today;
        var filteredProducts = replaceProducts.Where(ps => ps.CreatedDate!.Value.Date == today).ToList();

        ShowReplaceProducts(filteredProducts);
    }

    private async void FilterReplaceProducts()
    {
        St_ReturnedProducts.Children.Clear();

        FilterReplaceProductDto filterReplaceProductDto = new FilterReplaceProductDto();

        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterReplaceProductDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterReplaceProductDto.ToDateTime = toDateTime.SelectedDate.Value;
        }

        var selectedWorkerName = workerComboBox.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Barcha sotuvchi"))
        {
            filterReplaceProductDto.WorkerName = selectedWorkerName;
        }

        var searchTerm = searchTextBox.Text.ToLower();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            filterReplaceProductDto.ProductName = searchTerm;
        }

        var filteredProducts = await Task.Run(async () => await _replaceProductService.FilterReplaceProductAsync(filterReplaceProductDto));

        ShowReplaceProducts(filteredProducts);
    }

    private void ShowReplaceProducts(IList<ReplaceProductDto> replaceProducts)
    {
        replaceProducts = replaceProducts.OrderByDescending(ps => ps.CreatedDate).ToList();

        var count = replaceProducts.Sum(sum => sum.Count);
        var totalCost = replaceProducts.Sum(sum => sum.ProductSale!.Product.SellPrice * sum.Count);
        _shopDetailsPage.SetValuesReturnProducts(count, totalCost);

        Loader.Visibility = Visibility.Collapsed;
        St_ReturnedProducts.Children.Clear();
        int rowNumber = 1;

        if (replaceProducts.Count > 0)
        {
            foreach (var item in replaceProducts)
            {
                double totalPrice = item.ProductSale!.Product.SellPrice * item.Count;
                ReturnedCargoComponent returnedCargoComponent = new ReturnedCargoComponent();
                returnedCargoComponent.Tag = item.Id;
                returnedCargoComponent.SetValues(
                    rowNumber,
                    item.ProductSale.SalesRequest.TransactionId,
                    item.ProductSale.Product.Name,
                    item.ProductSale.Product.SellPrice,
                    item.Count,
                    totalPrice);

                returnedCargoComponent.BorderThickness = new Thickness(2);
                St_ReturnedProducts.Children.Add(returnedCargoComponent);
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
        FilterReplaceProducts();
    }

    private void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterReplaceProducts();
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            FilterReplaceProducts();
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllProduct();
    }

}
