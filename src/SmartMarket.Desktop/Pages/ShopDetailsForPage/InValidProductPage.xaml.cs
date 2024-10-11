using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Services.Products.InvalidProduct;

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
    /// Interaction logic for InValidProductPage.xaml
    /// </summary>
    public partial class InValidProductPage : Page
    {
        private IInvalidProductService _invalidProductService;
        public InValidProductPage()
        {
            InitializeComponent();
            _invalidProductService = new InvalidProductService();
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

        private void ShowInvalidProducts(IEnumerable<InvalidProductDto> productSales)
        {
            productSales = productSales.OrderByDescending(ps => ps.CreatedDate).ToList();

            St_InValidProducts.Visibility = Visibility.Visible;
            St_InValidProducts.Children.Clear();
            int rowNumber = 1;

            foreach (var item in productSales)
            {
                double totalPrice = item.ProductSale.Product.Price * item.Count;
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
