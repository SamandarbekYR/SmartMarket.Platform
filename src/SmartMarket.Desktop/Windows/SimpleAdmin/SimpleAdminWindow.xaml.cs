using SmartMarket.Desktop.Pages.SaleForPage;
using System.Windows;

namespace SmartMarket.Desktop.Windows.SimpleAdmin;

/// <summary>
/// Interaction logic for SimpleAdminWindow.xaml
/// </summary>
public partial class SimpleAdminWindow : Window
{
    public SimpleAdminWindow()
    {
        InitializeComponent();
    }


    private void Button_Close_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        SalePage salePage = new SalePage();
        Page_Navigator.Content = salePage;
    }
}
