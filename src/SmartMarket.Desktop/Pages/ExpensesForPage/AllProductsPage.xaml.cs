using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.Services.Products.Product;
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

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for AllProductsPage.xaml
    /// </summary>
    public partial class AllProductsPage : Page
    {
        private readonly IProductService productService;
        public AllProductsPage()
        {
            InitializeComponent();
            this.productService = new ProductService();
        }

        public async void GetAllProduct()
        {
            //St_AllProducts.Children.Clear();

            //int count = 1;

            //var products = await productService.GetAll();

            //if (products != null)
            //{
            //    foreach (var product in products)
            //    {
            //        ProductsComponent productsComponent = new ProductsComponent();
            //        productsComponent.tbNumber.Text = count.ToString();
            //        productsComponent.SetData(product);
            //        St_AllProducts.Children.Add(productsComponent);
            //        count++;
            //    }
            //}
            //else
            //{ }

            for(int i = 0; i < 20; i++)
            {
                ProductsComponent productsComponent = new ProductsComponent();
                St_AllProducts.Children.Add(productsComponent);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
