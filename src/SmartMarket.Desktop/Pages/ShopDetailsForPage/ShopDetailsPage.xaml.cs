using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    /// <summary>
    /// Interaction logic for ShopDetailsPage.xaml
    /// </summary>
    public partial class ShopDetailsPage : Page
    {
        public ShopDetailsPage()
        {
            InitializeComponent();
        }

        private void rbSaleHistory_Click(object sender, RoutedEventArgs e)
        {
            ShopHIstoryPage shopHIstoryPage = new ShopHIstoryPage();
            SalePageNavigator.Content = shopHIstoryPage;

            Main_Border.Visibility = Visibility.Visible;      

            Invalid_Prods_1.Visibility = Visibility.Collapsed;
            Invalid_Prods_2.Visibility = Visibility.Collapsed;

            Return_Prods_1.Visibility = Visibility.Collapsed;
            Return_Prods_2.Visibility = Visibility.Collapsed;

            History_Border_1.Visibility = Visibility.Visible;
            History_Border_2.Visibility = Visibility.Visible;
            History_Border_3.Visibility = Visibility.Visible;

        }

        private void rbTopSale_Click(object sender, RoutedEventArgs e)
        {
            TopSalePage topSalePage = new TopSalePage();    
            SalePageNavigator.Content= topSalePage;

            Main_Border.Visibility = Visibility.Collapsed;

            History_Border_1.Visibility = Visibility.Collapsed;
            History_Border_2.Visibility = Visibility.Collapsed;
            History_Border_3.Visibility = Visibility.Collapsed;

            Invalid_Prods_1.Visibility = Visibility.Collapsed;
            Invalid_Prods_2.Visibility = Visibility.Collapsed;

            Return_Prods_1.Visibility = Visibility.Collapsed;
            Return_Prods_2.Visibility = Visibility.Collapsed;

        }

        private void rbReturnProds_Click(object sender, RoutedEventArgs e)
        {
            ReturnedCargoPage returnedCargoPage = new ReturnedCargoPage();
            SalePageNavigator.Content= returnedCargoPage;

            Main_Border.Visibility = Visibility.Visible;

            History_Border_1.Visibility = Visibility.Collapsed;
            History_Border_2.Visibility = Visibility.Collapsed;
            History_Border_3.Visibility = Visibility.Collapsed;

            Invalid_Prods_1.Visibility = Visibility.Collapsed;
            Invalid_Prods_2.Visibility = Visibility.Collapsed;

            Return_Prods_1.Visibility = Visibility.Visible;
            Return_Prods_2.Visibility = Visibility.Visible;

        }

        private void rbInvalidProds_Click(object sender, RoutedEventArgs e)
        {
            InValidProductPage inValidProductPage = new InValidProductPage(this);
            SalePageNavigator.Content = inValidProductPage;

            Main_Border.Visibility = Visibility.Visible;

            History_Border_1.Visibility = Visibility.Collapsed;
            History_Border_2.Visibility = Visibility.Collapsed;
            History_Border_3.Visibility = Visibility.Collapsed;

            Return_Prods_1.Visibility = Visibility.Collapsed;
            Return_Prods_2.Visibility = Visibility.Collapsed;

            Invalid_Prods_1.Visibility = Visibility.Visible;
            Invalid_Prods_2.Visibility = Visibility.Visible;
        }

        public void SetValuesReturnProducts(int count, double totalCost)
        {
            lb_ReturnProductCount.Content = count.ToString();
            lb_ReturnProductTotalCost.Content = totalCost.ToString();
        }

        public void SetValuesInvalidProducts(int count, double totalCost)
        {
            lb_InvalidProductCount.Content = count.ToString();
            lb_InvalidProductTotalCost.Content = totalCost.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShopHIstoryPage shopHIstoryPage = new ShopHIstoryPage();
            SalePageNavigator.Content = shopHIstoryPage;
        }
    }
}
