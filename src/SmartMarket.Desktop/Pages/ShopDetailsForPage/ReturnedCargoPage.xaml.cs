using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Services.Products.ReplaceProduct;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    /// <summary>
    /// Interaction logic for ReturnedCargoPage.xaml
    /// </summary>
    public partial class ReturnedCargoPage : Page
    {
        private IReplaceProductService _replaceProductService;
        public ReturnedCargoPage()
        {
            InitializeComponent();
            _replaceProductService = new ReplaceProductService();
        }

        public async void GetAllProduct()
        {
            var replaceProducts = await _replaceProductService.GetAllAsync();

            List<string> workerNames = replaceProducts
                .Select(ps => ps.Worker.FirstName)
                .Distinct()
                .ToList();

            workerNames.Insert(0, "Barcha sotuvchi");
            workerComboBox.ItemsSource = workerNames;

            var today = DateTime.Today;
            replaceProducts = replaceProducts.Where(ps => ps.CreatedDate.Value.Date == today).ToList();

            ShowReplaceProducts(replaceProducts);
        }

        private async void FilterReplaceProducts()
        {
            FilterReplaceProductDto filterProductSaleDto = new FilterReplaceProductDto();

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

            var filteredProductSales = await _replaceProductService.FilterReplaceProductAsync(filterProductSaleDto);

            ShowReplaceProducts(filteredProductSales);
        }

        private void ShowReplaceProducts(IEnumerable<ReplaceProductDto> productSales)
        {
            productSales = productSales.OrderByDescending(ps => ps.CreatedDate).ToList();

            St_ReturnedProducts.Visibility = Visibility.Visible;
            St_ReturnedProducts.Children.Clear();
            int rowNumber = 1;

            foreach (var item in productSales)
            {
                double totalPrice = item.ProductSale.Product.Price * item.Count;
                ReturnedCargoComponent returnedCargoComponent = new ReturnedCargoComponent();
                returnedCargoComponent.Tag = item.Id;
                returnedCargoComponent.SetValues(
                    rowNumber,
                    item.ProductSale.TransactionNumber,
                    item.ProductSale.Product.Name,
                    item.ProductSale.Product.Price,
                    item.Count,
                    totalPrice);

                returnedCargoComponent.BorderThickness = new Thickness(2);
                St_ReturnedProducts.Children.Add(returnedCargoComponent);
                rowNumber++;
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
}
