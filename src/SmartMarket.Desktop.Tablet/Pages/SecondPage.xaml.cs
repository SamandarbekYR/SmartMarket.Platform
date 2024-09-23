using SmartMarket.Desktop.Tablet.Components;
using System.Windows;
using System.Windows.Controls;

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
            SearchProductComponent searchProductComponent = new SearchProductComponent();
            ProductComponent productComponent = new ProductComponent();

            st_searchproduct.Children.Add(searchProductComponent);
            st_product.Children.Add(productComponent);
        }
    }
}
