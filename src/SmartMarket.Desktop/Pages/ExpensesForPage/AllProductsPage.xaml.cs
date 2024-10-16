using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for AllProductsPage.xaml
    /// </summary>
    public partial class AllProductsPage : Page
    {
        private readonly IProductService _productService;
        public AllProductsPage()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        public async void GetAllProduct()
        {
            //St_AllProducts.Children.Clear();

            //int count = 1;

            //var products = await _productService.GetAll();

            //if (products != null)
            //{
            //    foreach (var product in products)
            //    {
            //        MainProductComponent productsComponent = new MainProductComponent();
            //        productsComponent.tbNumber.Text = count.ToString();
            //        productsComponent.SetData(product);
            //        St_AllProducts.Children.Add(productsComponent);
            //        count++;
            //    }
            //}
            //else
            //{ }

            for (int i = 0; i < 20; i++)
            {
                MainProductComponent productsComponent = new MainProductComponent();
                St_AllProducts.Children.Add(productsComponent);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
