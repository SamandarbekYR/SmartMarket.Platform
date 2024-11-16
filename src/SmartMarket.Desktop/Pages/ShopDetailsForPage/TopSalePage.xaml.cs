using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage;

/// <summary>
/// Interaction logic for TopSalePage.xaml
/// </summary>
public partial class TopSalePage : Page
{
    private readonly IProductSaleService _productSaleService;
    public TopSalePage()
    {
        InitializeComponent();
        _productSaleService = new ProductSaleService();
    }

    public async void GetAllTopSale()
    {
        St_TopSale.Children.Clear();

        var topSaleProducts = await Task.Run(async () => await _productSaleService.GetAllAsync());

        var today = DateTime.Today;
        var filteredTopSales = topSaleProducts.Where(ps => ps.CreatedDate!.Value.Date == today).ToList();

        await ShowProductSales(filteredTopSales);
    }

    private async void FilterProductSales()
    {
        St_TopSale.Children.Clear();

        FilterProductSaleDto filterProductSaleDto = new FilterProductSaleDto();

        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterProductSaleDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterProductSaleDto.ToDateTime = toDateTime.SelectedDate.Value;
        }

        var searchTerm = searchTextBox.Text.ToLower();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            filterProductSaleDto.ProductName = searchTerm;
        }

        var filteredProductSales = await Task.Run(async () => await _productSaleService.FilterProductSaleAsync(filterProductSaleDto));

        await ShowProductSales(filteredProductSales);
    }

    private async Task ShowProductSales(IList<ProductSaleViewModel> productSales)
    {
        productSales = productSales.Where(ps => ps.Count != 0)
            .OrderByDescending(ps => ps.CreatedDate).ToList();

        var groupedProducts = productSales
            .GroupBy(p => p.Product.Name)
            .Select(group => new
            {
                ProductName = group.Key,
                TotalCount = group.Sum(p => p.Count), // Mahsulotning umumiy sotilgan soni
                TotalSales = group.Sum(p => p.Product.SellPrice * p.Count), // Jami sotuv summasi
                Profit = group.Sum(p => (p.Product.SellPrice - p.Product.Price) * p.Count) // Foyda summasi
            }).OrderByDescending(p => p.TotalCount).ToList();

        // Foydalanuvchi tomonidan kiritilgan son bo'yicha eng ko'p sotilganlarni olish
        if (!string.IsNullOrEmpty(countTextBox.Text))
        {
            int count;
            if (int.TryParse(countTextBox.Text, out count))
            {
                groupedProducts = groupedProducts.Take(count).ToList();
            }
        }
        Loader.Visibility = Visibility.Collapsed;
        St_TopSale.Children.Clear();
        int rowNumber = 1;

        if (groupedProducts.Count > 0)
        {
            EmptyData.Visibility = Visibility.Collapsed;
            foreach (var item in groupedProducts)
            {
                TopSaleComponent topSaleComponent = new TopSaleComponent();
                topSaleComponent.Tag = rowNumber;
                topSaleComponent.SetValues(
                    item.ProductName,
                    item.TotalCount,
                    item.TotalSales,
                    item.Profit);

                topSaleComponent.BorderThickness = new Thickness(2);
                St_TopSale.Children.Add(topSaleComponent);
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

    private async void CountTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var topSaleProducts = await Task.Run(async () => await _productSaleService.GetAllAsync());
            await ShowProductSales(topSaleProducts);
        }
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            FilterProductSales();
        }
    }

    private void CountTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private void CountTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            string text = (string)e.DataObject.GetData(typeof(string));
            if (!IsTextAllowed(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private static bool IsTextAllowed(string text)
    {
        return text.All(char.IsDigit); // Faqat raqamlar kiritish mumkin
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllTopSale();
    }

}
