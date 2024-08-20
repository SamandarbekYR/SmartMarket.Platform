using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ShopHIstoryPage shopHIstoryPage = new ShopHIstoryPage();
            SalePageNavigator.Content = shopHIstoryPage;
        }

        private void rbSaleHistory_Click(object sender, RoutedEventArgs e)
        {
            ShopHIstoryPage shopHIstoryPage = new ShopHIstoryPage();
            SalePageNavigator.Content = shopHIstoryPage;
        }

        private void rbTopSale_Click(object sender, RoutedEventArgs e)
        {
            TopSalePage topSalePage = new TopSalePage();    
            SalePageNavigator.Content= topSalePage;
            
        }

        private void rbReturnProds_Click(object sender, RoutedEventArgs e)
        {
          ReturnedCargoPage returnedCargoPage = new ReturnedCargoPage();
           SalePageNavigator.Content= returnedCargoPage;    
        }

        private void rbInvalidProds_Click(object sender, RoutedEventArgs e)
        {
            InValidProductPage inValidProductPage = new InValidProductPage();
            SalePageNavigator.Content= inValidProductPage;
        }




    }
}
