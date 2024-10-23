using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for SecondPage.xaml
/// </summary>
public partial class SecondPage : Page
{

    private readonly IProductService _productService; 

    public SecondPage()
    {
        InitializeComponent();
        this._productService = new ProductService();
    }


    #region Method

    public static MainWindow GetMainWindow()
    {
        MainWindow mainWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(MainWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                mainWindow = (MainWindow)window;
                if (mainWindow != null)
                {
                    break;
                }
            }
        }
        return mainWindow!;
    }

    private ProductComponent selectedProduct = null!;
    public void SelectProduct(ProductComponent product)
    {
        if (selectedProduct != null)
        {
            selectedProduct.product_Border.Background = Brushes.White;
        }

        product.product_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
        //EmptyPrice();
        //ColculateTotalPrice();
        selectedProduct = product;
    }

    private bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, @"^\d+$");
    }

    private void SetProduct(IList<FullProductDto> products)
    {
        Loader.Visibility = Visibility.Collapsed;
        st_searchproduct.Children.Clear();
        if (products.Count > 0)
        {
            foreach (var product in products)
            {
                SearchProductComponent searchProductComponent = new SearchProductComponent();
                searchProductComponent.Tag = product;
                searchProductComponent.SetData(product);
                st_searchproduct.Children.Add(searchProductComponent);
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    #endregion


    private void Exit_Button_Click(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.PageNavigator.Content = mainPage;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 20; i++)
        {
            ShipmentComponent shipmentComponent = new ShipmentComponent();
            ProductComponent productComponent = new ProductComponent();

            st_product.Children.Add(productComponent);
            st_shipments.Children.Add(shipmentComponent);
        }
    }

    private void Minus_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Plus_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Delete_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private CancellationTokenSource _cancellationTokenSource = null!;

    private async void tb_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        string search = tb_search.Text;

        st_searchproduct.Children.Clear();
        EmptyData.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            Loader.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(async () =>
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested) return;

                    IList<FullProductDto> products = new List<FullProductDto>();

                    if (IsNumeric(search) && search.Length >= 5)
                    {
                        products = await _productService.GetByPCode(search);
                    }
                    else if (!IsNumeric(search) && search.Length >= 1)
                    {
                        products = await _productService.GetByProductName(search);
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (search == tb_search.Text)
                        {
                            SetProduct(products);
                        }
                    });
                },
                _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                Loader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            st_searchproduct.Children.Clear();
        }
    }
}
