using SmartMarket.Desktop.Components.ShopDetailsForComponent;
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
        private List<ProductSaleViewModel> _cachedProductSales; 

        public ShopHIstoryPage()
        {
            InitializeComponent();
            _productSaleService = new ProductSaleService();
        }

        public async void GetAllProduct()
        {
            _cachedProductSales = await _productSaleService.GetAllAsync(); 

            List<string> workerNames = _cachedProductSales
                .Select(ps => ps.Worker.FirstName)
                .Distinct()
                .ToList();

            var defaultItem = new ComboBoxItem
            {
                Content = "Barcha sotuvchi",
               // IsEnabled = false,
                IsSelected = true,
            };

            workerComboBox.Items.Add(defaultItem);

            foreach (var workerName in workerNames)
            {
                workerComboBox.Items.Add(new ComboBoxItem
                {
                    Content = workerName,
                    IsEnabled = true
                });
            }

            workerComboBox.SelectedItem = defaultItem;

            ShowProductSales(_cachedProductSales); 
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                var fromDate = fromDateTime.SelectedDate.Value;
                var toDate = toDateTime.SelectedDate.Value;

                var filteredProductSales = _cachedProductSales.Where(ps =>
                    ps.CreatedDate >= fromDate &&
                    ps.CreatedDate <= toDate);

                ShowProductSales(filteredProductSales);
            }
        }

        private void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedWorkerName = (workerComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Barcha sotuvchi"))
            {
                var filteredProductSales = _cachedProductSales
                    .Where(ps => ps.Worker.FirstName == selectedWorkerName)
                    .ToList();

                ShowProductSales(filteredProductSales);
            }
            else
            {
                ShowProductSales(_cachedProductSales); 
            }
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchTerm = searchTextBox.Text.ToLower();

                var filteredProductSales = _cachedProductSales.Where(ps =>
                    ps.Product.Name.ToLower().Contains(searchTerm) ||
                    ps.TransactionNumber.ToString().Contains(searchTerm));

                ShowProductSales(filteredProductSales);
            }
        }

        private void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
        {
            St_productList.Children.Clear();
            int rowNumber = 1;

            foreach (var item in productSales)
            {
                ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                shopDetailsProductComponent.Tag = item;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
