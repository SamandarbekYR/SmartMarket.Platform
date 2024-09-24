using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.Windows;
using System.Windows;
using System.Windows.Controls;

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
}
