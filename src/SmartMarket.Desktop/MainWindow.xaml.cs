using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Pages;
using SmartMarket.Desktop.Pages.SaleForPage;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartMarket.Desktop.Pages.ShopDetailsForPage;
using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using SmartMarket.Desktop.Pages.PartnerForPage;
using SmartMarket.Desktop.Pages.AccountSettingsForPage;
using SmartMarket.Desktop.Windows.Auth;
using SmartMarket.Desktop.Pages.ExpensesForPage;
using SmartMarket.Desktop.Pages.CashReportForPage;
using SmartMarket.Desktop.Pages.SettingsForPage;
using SmartMarketDeskop.Integrated.DBContext;
using Microsoft.EntityFrameworkCore;

namespace SmartMarket.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // Ba'zaga ulanish uchun connection string
            var connectionString = "Host=localhost;Database=SmartPartners_Desktop;User ID=postgres;Port=5432;Password=1234;";

            // DbContextOptions ni yaratish
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            // DbContext ni yaratish
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                // Ma'lumotlar bazasini yaratish va jadvalni o‘rnatish
                //context.Database.EnsureCreated();
                //context.Database.Migrate();
                // Jadvalga ma'lumot qo'shish yoki o‘zgartirish
              
            }


            MainPage mainPage = new MainPage();
            PageNavigator.Content = mainPage;
        }

        private void Btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }


        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            PageNavigator.Content = mainPage;
        }

        private void btnsale_Click(object sender, RoutedEventArgs e)
        {
            SalePage salePage=new SalePage();
            PageNavigator.Content = salePage;
        }

      
        private void btnShopworkers_Click(object sender, RoutedEventArgs e)
        {
            ShopWorkersPage shopWorkersPage=new ShopWorkersPage();
            PageNavigator.Content= shopWorkersPage; 
        }

        private void btnPartners_Click(object sender, RoutedEventArgs e)
        {
            PartnersPage partnersPage=new PartnersPage();
            PageNavigator.Content = partnersPage;   
        }

        private void btnAccountSettings_Click(object sender, RoutedEventArgs e)
        {
            AccountSettingsPage accountSettingsPage =new AccountSettingsPage();
            PageNavigator.Content = accountSettingsPage;    
        }

        private void btnSaleDetails_Click(object sender, RoutedEventArgs e)
        {
            ShopDetailsPage shopDetailsPage = new ShopDetailsPage();
            PageNavigator.Content = shopDetailsPage;
        }

        private void btnShopDetails_Click(object sender, RoutedEventArgs e)
        {
            ExpensesPage expensesPage=new ExpensesPage();
            PageNavigator.Content = expensesPage;
        }

        private void btnCashReport_Click(object sender, RoutedEventArgs e)
        {
            CashReportPage cashReportPage=new CashReportPage();
            PageNavigator.Content = cashReportPage;
        }

        private void tbsettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage settingsPage =new SettingsPage();
            PageNavigator.Content= settingsPage;
        }

      //  private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Visibility = Visibility.Collapsed;
        //    LoginWindow loginWindow = new LoginWindow();
        //    loginWindow.ShowDialog();
      //  }
    }
}