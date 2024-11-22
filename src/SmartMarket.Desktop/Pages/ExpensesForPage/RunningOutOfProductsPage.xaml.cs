﻿using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;

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

        public async void GetAll()
        {
            St_Products.Children.Clear();

            int count = 1;

            var products = await Task.Run(async () => await _productService.GetFinishedProducts());

            Loader.Visibility = Visibility.Collapsed;

            if (products != null)
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
    }
}
