using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly IProductServer server;
        public AllProductsPage()
        {
            InitializeComponent();
            this.server = new ProductServer();
        }

        public async void GetAllProduct()
        {
            //St_AllProducts.Children.Clear();

            //var products = await server.GetAllAsync();

            //int count = 1;

            //if (products != null)
            //{
            //    foreach (var product in products)
            //    {
            //        ProductsComponent productsComponent = new ProductsComponent();
            //        productsComponent.tbNumber.Text = count.ToString();
            //        productsComponent.SetData(product);
            //        St_AllProducts.Children.Add(productsComponent);
            //        count++;

            //        MessageBox.Show("Componentlar yuklanyapdi");
            //    }

            //    MessageBox.Show($"jami {count - 1} component qoshildi");
            //}
            //else
            //{ MessageBox.Show("component null"); }

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
