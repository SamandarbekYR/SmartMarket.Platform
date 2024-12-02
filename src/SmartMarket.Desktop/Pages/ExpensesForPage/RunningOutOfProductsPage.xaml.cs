using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for RunningOutOfProductsPage.xaml
    /// </summary>
    public partial class RunningOutOfProductsPage : Page
    {
        private readonly IProductService _productService;
        public RunningOutOfProductsPage()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        private async void FilterLoadReports()
        {
            EmptyDataRunningOutOfProduct.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
            St_Products.Children.Clear();

            var productName = filterTextBox.Text;
            if (!string.IsNullOrEmpty(productName))
            {
                var runningOutOfProducts = await Task.Run(async () => await _productService.GetByProductName(productName));
                ShowRunningOutOfProducts(runningOutOfProducts);
            }
            else
            {
                Loader.Visibility = Visibility.Collapsed;
                EmptyDataRunningOutOfProduct.Visibility = Visibility.Visible;
            }
        }

        public async void GetAll()
        {
            St_Products.Children.Clear();

            var products = await Task.Run(async () => await _productService.GetFinishedProducts());

            ShowRunningOutOfProducts(products);
        }

        private void ShowRunningOutOfProducts(List<FullProductDto> products)
        {
            Loader.Visibility = Visibility.Collapsed;

            int count = 1;

            if (products.Any())
            {
                foreach (var product in products)
                {
                    RunningProductComponent runningProductComponent = new RunningProductComponent();
                    runningProductComponent.SetData(product, count);
                    runningProductComponent.Tag = product;
                    St_Products.Children.Add(runningProductComponent);
                    count++;
                }
            }
            else
            {
                EmptyDataRunningOutOfProduct.Visibility = Visibility.Visible;
            }
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }

        private void filterTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                FilterLoadReports();
            }
        }
    }
}
