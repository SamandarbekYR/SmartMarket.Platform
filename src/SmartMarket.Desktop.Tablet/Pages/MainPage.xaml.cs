using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.BottomCenter,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

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

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 20; i++)
        {
            SearchProductComponent searchProductComponent = new SearchProductComponent();
            ProductComponent productComponent = new ProductComponent();

            st_searchproduct.Children.Add(searchProductComponent);
            st_product.Children.Add(productComponent);
        }
    }

    private void Sends_Button_Click(object sender, RoutedEventArgs e)
    {
        SecondPage secondPage = new SecondPage();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.PageNavigator.Content = secondPage;
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        MainWindow mainWindow = GetMainWindow();

        mainWindow.Close();
        loginWindow.ShowDialog();
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

    private void Edit_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Delete_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Plus_Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Minus_Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
