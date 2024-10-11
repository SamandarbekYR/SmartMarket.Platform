using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Desktop.Windows;
using SmartMarket.Service.DTOs.Products.InvalidProduct;

using SmartMarketDeskop.Integrated.Services.Products.InvalidProduct;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    /// <summary>
    /// Interaction logic for InValidProductPage.xaml
    /// </summary>
    public partial class InValidProductPage : Page
    {
        private IInvalidProductService _invalidProductService;
        private ShopDetailsPage _shopDetailsPage;
        public InValidProductPage(ShopDetailsPage shopDetailsPage)
        {
            InitializeComponent();
            _invalidProductService = new InvalidProductService();
            _shopDetailsPage = shopDetailsPage;
        }

        public async void GetAllProduct()
        {
            var invalidProducts = await _invalidProductService.GetAllAsync();

            List<string> workerNames = invalidProducts
                .Select(ps => ps.Worker.FirstName)
                .Distinct()
                .ToList();

            workerNames.Insert(0, "Barcha sotuvchi");
            workerComboBox.ItemsSource = workerNames;

            var today = DateTime.Today;
            invalidProducts = invalidProducts.Where(ps => ps.CreatedDate.Date == today).ToList();

            ShowInvalidProducts(invalidProducts);
        }

        private async void FilterInvalidProducts()
        {
            FilterInvalidProductDto filterInvalidProductDto = new FilterInvalidProductDto();

            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filterInvalidProductDto.FromDateTime = fromDateTime.SelectedDate.Value;
                filterInvalidProductDto.ToDateTime = toDateTime.SelectedDate.Value;
            }

            var selectedWorkerName = workerComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Barcha sotuvchi"))
            {
                filterInvalidProductDto.WorkerName = selectedWorkerName;
            }

            var searchTerm = searchTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filterInvalidProductDto.ProductName = searchTerm;
            }

            var filterInvalidProducts = await _invalidProductService.FilterInvalidProductAsync(filterInvalidProductDto);

            ShowInvalidProducts(filterInvalidProducts);
        }

        private void ShowInvalidProducts(IEnumerable<InvalidProductDto> invalidProducts)
        {
            invalidProducts = invalidProducts.OrderByDescending(ps => ps.CreatedDate).ToList();

            var count = invalidProducts.Sum(sum => sum.Count);
            var totalCost = invalidProducts.Sum(sum => sum.ProductSale.Product.SellPrice * sum.Count);
            _shopDetailsPage.SetValuesInvalidProducts(count, totalCost);
            St_InValidProducts.Children.Clear();
            int rowNumber = 1;

            foreach (var item in invalidProducts)
            {
                double totalPrice = item.ProductSale.Product.SellPrice * item.Count;
                InValidProdutComponent inValidProdutComponent = new InValidProdutComponent();
                inValidProdutComponent.Tag = item.Id;
                inValidProdutComponent.SetValues(
                    rowNumber,
                    item.ProductSale.TransactionNumber,
                    item.ProductSale.Product.Name,
                    item.ProductSale.Product.Price,
                    item.Count,
                    totalPrice);

                inValidProdutComponent.BorderThickness = new Thickness(2);
                St_InValidProducts.Children.Add(inValidProdutComponent);
                rowNumber++;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterInvalidProducts();
        }

        private void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterInvalidProducts();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FilterInvalidProducts();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
