﻿using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ExpensesForPage;

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

    public async Task GetAllProducts()
    {
        Loader.Visibility = Visibility.Visible;
        St_AllProducts.Children.Clear();

        var products = await Task.Run(async () => await _productService.GetAll());

        Loader.Visibility = Visibility.Collapsed;

        int productCount = 1;

        if (products != null)
        {
            foreach (var product in products)
            {
                MainProductComponent productComponent = new MainProductComponent();
                productComponent.Tag = product;
                productComponent.GetData(product, productCount);
                productComponent.GetProducts = GetAllProducts;
                St_AllProducts.Children.Add(productComponent);
                productCount++;
            }
        }
        else
        {
            EmptyDataAllProducts.Visibility = Visibility.Visible;
        }
    }


    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllProducts();
    }
}
