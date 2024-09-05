using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Pages.PartnerForPage;
using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Pages.SettingsForPage;
using SmartMarket.Desktop.Pages.ShopDetailsForPage;
using System.Windows;

namespace SmartMarket.Desktop.Windows.Admin;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void Button_Main_Click(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        Page_Navigator.Content = mainPage;
    }

    private void Button_Sale_Click(object sender, RoutedEventArgs e)
    {
        SalePage salePage = new SalePage();
        Page_Navigator.Content = salePage;  
    }

    private void Button_Partner_Click(object sender, RoutedEventArgs e)
    {
        PartnersPage partnersPage = new PartnersPage();
        Page_Navigator.Content = partnersPage;  
    }

    private void Button_Shop_Details_Click(object sender, RoutedEventArgs e)
    {
        ShopDetailsPage shopDetailsPage = new ShopDetailsPage();
        Page_Navigator.Content = shopDetailsPage;
    }

    private void Button_Settings_Click(object sender, RoutedEventArgs e)
    {
        SettingsPage settingsPage = new SettingsPage();
        Page_Navigator.Content = settingsPage;  
    }

    private void Button_Close_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        Page_Navigator.Content = mainPage;
    }
}
