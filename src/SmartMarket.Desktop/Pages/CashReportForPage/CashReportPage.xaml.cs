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

namespace SmartMarket.Desktop.Pages.CashReportForPage
{
    /// <summary>
    /// Interaction logic for CashReportPage.xaml
    /// </summary>
    public partial class CashReportPage : Page
    {
        public CashReportPage()
        {
            InitializeComponent();

            CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
            CheckOutPageNavigator.Content = checkOutFirstPage;
        }

        private void btnCheckoutFirst_Click(object sender, RoutedEventArgs e)
        {
            CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
            CheckOutPageNavigator.Content = checkOutFirstPage;
        }

        private void bntCheckoutSecond_Click(object sender, RoutedEventArgs e)
        {
            CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
            CheckOutPageNavigator.Content = checkOutFirstPage;
        }
    }
}
