using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

    private async void FilterProducts()
    {
        EmptyDataAllProducts.Visibility = Visibility.Collapsed;
        Loader.Visibility = Visibility.Visible;
        St_AllProducts.Children.Clear();

        var productName = filterTextBox.Text;

        if (!string.IsNullOrEmpty(productName))
        {
            var filterProducts = await Task.Run(async () => await _productService.GetByProductName(productName));
            ShowProducts(filterProducts);
        }
        else
        {
            Loader.Visibility = Visibility.Collapsed;
            EmptyDataAllProducts.Visibility = Visibility.Visible;
        }
    }

    public async Task GetAllProducts()
    {
        Loader.Visibility = Visibility.Visible;
        St_AllProducts.Children.Clear();

        var products = await Task.Run(async () => await _productService.GetAll());

        ShowProducts(products);
    }

    private void ShowProducts(List<FullProductDto> products)
    {
        Loader.Visibility = Visibility.Collapsed;

        int productCount = 1;

        if (products.Any())
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

    private void filterTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
        {
            FilterProducts();
        }
    }
}
