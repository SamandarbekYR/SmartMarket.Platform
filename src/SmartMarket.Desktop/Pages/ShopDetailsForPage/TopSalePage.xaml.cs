using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Services.Products.ProductSale;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
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

        public async void GetALlTopSale()
        {
            var topSaleProducts = await _productSaleService.GetAllAsync();
            ShowProductSales(topSaleProducts);
        }

        private async void FilterProductSales()
        {
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

            var filteredProductSales = await _productSaleService.FilterProductSaleAsync(filterProductSaleDto);

            ShowProductSales(filteredProductSales);
        }

        private void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
        {
            productSales = productSales.OrderByDescending(ps => ps.CreatedDate).ToList();
            var groupedProducts = productSales
                .GroupBy(p => p.Product.Name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    TotalCount = group.Sum(p => p.Count), // Mahsulotning umumiy sotilgan soni
                    TotalSales = group.Sum(p => p.Product.SellPrice * p.Count), // Jami sotuv summasi
                    Profit = group.Sum(p => (p.Product.SellPrice - p.Product.Price) * p.Count) // Foyda summasi
                }).OrderByDescending(p => p.TotalCount).ToList();

            if(!string.IsNullOrEmpty(countTextBox.Text))
            {
                int count = int.Parse(countTextBox.Text);
                groupedProducts = groupedProducts.Take(count).ToList();
            }

            St_TopSale.Visibility = Visibility.Visible;
            St_TopSale.Children.Clear();
            int rowNumber = 1;

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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterProductSales();
        }

        private async void CountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var topSaleProducts = await _productSaleService.GetAllAsync();
                ShowProductSales(topSaleProducts);
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
            return text.All(char.IsDigit); 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetALlTopSale();
        }
    }
}
