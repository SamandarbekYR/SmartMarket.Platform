using SmartMarket.Desktop.Pages.AccountSettingsForPage;
using SmartMarket.Desktop.Pages.CashReportForPage;
using SmartMarket.Desktop.Pages.ExpensesForPage;
using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Pages.PartnerForPage;
using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Pages.SettingsForPage;
using SmartMarket.Desktop.Pages.ShopDetailsForPage;
using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using System.Windows;

namespace SmartMarket.Desktop.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Btnclose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }


    private void btnsale_Click(object sender, RoutedEventArgs e)
    {
        SalePage salePage = new SalePage();
        PageNavigator.Content = salePage;
    }


    private void btnShopworkers_Click(object sender, RoutedEventArgs e)
    {
        ShopWorkersPage shopWorkersPage = new ShopWorkersPage();
        PageNavigator.Content = shopWorkersPage;
    }

    private void btnPartners_Click(object sender, RoutedEventArgs e)
    {
        PartnersPage partnersPage = new PartnersPage();
        PageNavigator.Content = partnersPage;
    }

    private void btnAccountSettings_Click(object sender, RoutedEventArgs e)
    {
        AccountSettingsPage accountSettingsPage = new AccountSettingsPage();
        PageNavigator.Content = accountSettingsPage;
    }

    private void btnSaleDetails_Click(object sender, RoutedEventArgs e)
    {
        ShopDetailsPage shopDetailsPage = new ShopDetailsPage();
        PageNavigator.Content = shopDetailsPage;
    }

    private void btnShopDetails_Click(object sender, RoutedEventArgs e)
    {
        ExpensesPage expensesPage = new ExpensesPage();
        PageNavigator.Content = expensesPage;
    }

    private void btnCashReport_Click(object sender, RoutedEventArgs e)
    {
        CashReportPage cashReportPage = new CashReportPage();
        PageNavigator.Content = cashReportPage;
    }

    private void tbsettings_Click(object sender, RoutedEventArgs e)
    {
        SettingsPage settingsPage = new SettingsPage();
        PageNavigator.Content = settingsPage;
    }



    private void btnMain_Click(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        PageNavigator.Content = mainPage;
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double windowHeight = this.ActualHeight;
        double windowWidth = this.ActualWidth;

        btnMain.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnsale.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnSaleDetails.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnShopworkers.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnPartners.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnAccountSettings.FontSize = Math.Min(windowHeight, windowWidth) / 62;
        btnShopDetails.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        btnCashReport.FontSize = Math.Min(windowHeight, windowWidth) / 60;
        tbsettings.FontSize = Math.Min(windowHeight, windowWidth) / 62;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        PageNavigator.Content = mainPage;
    }
}
