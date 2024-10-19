using SmartMarket.Desktop.Tablet.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for SecondPage.xaml
/// </summary>
public partial class SecondPage : Page
{
    public SecondPage()
    {
        InitializeComponent();
    }

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
            SearchProductComponent searchProductComponent = new SearchProductComponent();
            ProductComponent productComponent = new ProductComponent();

            st_searchproduct.Children.Add(searchProductComponent);
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
}
