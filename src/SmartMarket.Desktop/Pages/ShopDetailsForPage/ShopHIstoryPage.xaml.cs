using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;

using System.Linq;
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

            var defaultItem = new ComboBoxItem
            {
                Content = "Sotuvchi",
                IsEnabled = false, 
                IsSelected = true ,

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

            ShowProductSales(productSales);
        }

        private async void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                var productSales = await _productSaleService.GetAllAsync();

                var fromDate = fromDateTime.SelectedDate;
                var toDate = toDateTime.SelectedDate;

                var filteredProductSales = productSales.Where(ps =>
                    (!fromDate.HasValue || ps.CreatedDate >= fromDate.Value) &&
                    (!toDate.HasValue || ps.CreatedDate <= toDate.Value));

                ShowProductSales(filteredProductSales);
            }
        }

        private async void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedWorkerName = (workerComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
           
            var productSales = await _productSaleService.GetAllAsync();

            if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Sotuvchi"))
            {
                var filteredProductSales = productSales
                    .Where(ps => ps.Worker.FirstName == selectedWorkerName)
                    .ToList();

                ShowProductSales(filteredProductSales);
            }
            else
            {
                ShowProductSales(productSales);
            }
        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var productSales = await _productSaleService.GetAllAsync();
                var searchTerm = searchTextBox.Text.ToLower();

                var filteredProductSales = productSales.Where(ps =>
                    ps.Product.Name.ToLower().Contains(searchTerm) ||
                    ps.TransactionNumber.ToString().Contains(searchTerm)
                );

                ShowProductSales(filteredProductSales);
            }
        }

        private void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
        {
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
