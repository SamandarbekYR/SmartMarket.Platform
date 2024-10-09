using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    public partial class ShopHIstoryPage : Page
    {
        private IProductSaleService _productSaleService;

        public ShopHIstoryPage()
        {
            InitializeComponent();
            _productSaleService = new ProductSaleService();
        }

        public async void GetAllProduct()
        {
            var productSales = await _productSaleService.GetAllAsync();

            List<string> workerNames = productSales
                .Select(ps => ps.Worker.FirstName)
                .Distinct()
                .ToList();

            workerNames.Insert(0, "Barcha sotuvchi");
            workerComboBox.ItemsSource = workerNames;

            var today = DateTime.Today;
            productSales = productSales.Where(ps => ps.CreatedDate.Value.Date == today).ToList();

            ShowProductSales(productSales);
        }

        private async void FilterProductSales()
        {
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

            ShowProductSales(filteredProductSales);
        }

        private void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
        {
            productSales = productSales.OrderByDescending(ps => ps.CreatedDate).ToList();

            St_productList.Visibility = Visibility.Visible;
            St_productList.Children.Clear();
            int rowNumber = 1;

            foreach (var item in productSales)
            {
                ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                shopDetailsProductComponent.Tag = rowNumber;
                shopDetailsProductComponent.SetValues(
                    rowNumber,
                    item.TransactionNumber,
                    item.Product.Name,
                    item.Product.Price,
                    item.Count,
                    item.TotalCost);

                shopDetailsProductComponent.BorderThickness = new Thickness(2);
                St_productList.Children.Add(shopDetailsProductComponent);
                rowNumber++;
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
}
