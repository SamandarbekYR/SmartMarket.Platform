using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for ExpensesPage.xaml
    /// </summary>
    public partial class ExpensesPage : Page
    {
        public ExpensesPage()
        {
            InitializeComponent();
            ExpenseListPage expenseListPage = new ExpenseListPage();
            ShopDetailsPageNavigator.Content = expenseListPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);
        }

        private void btnExpenses_Click(object sender, RoutedEventArgs e)
        {
            ExpenseListPage expenseListPage = new ExpenseListPage();
            ShopDetailsPageNavigator.Content = expenseListPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);
        }

        private void btnAllProduct_Click(object sender, RoutedEventArgs e)
        {
            AllProductsPage allProductsPage = new AllProductsPage();
            ShopDetailsPageNavigator.Content =allProductsPage;
            DisableForTwo(BrAllExpenses, BrPaymentSum);

        }

        private void btnRunningOutOfProduct_Click(object sender, RoutedEventArgs e)
        {
            RunningOutOfProductsPage runningOutOfProductsPage = new RunningOutOfProductsPage();
            ShopDetailsPageNavigator.Content=runningOutOfProductsPage;
            DisableForOne(BrRunningOutProduct);
        }

        private void btnCargoReport_Click(object sender, RoutedEventArgs e)
        {
            CargoReportPage cargoReportPage = new CargoReportPage();
            ShopDetailsPageNavigator.Content = cargoReportPage;
            DisableForOne(BrCargoReport);
        }





        public void DisableForOne(Border border)
        {
            BrAllExpenses.Visibility = Visibility.Collapsed;
            BrPaymentSum.Visibility = Visibility.Collapsed;
            BrRunningOutProduct.Visibility = Visibility.Collapsed;
            BrCargoReport.Visibility = Visibility.Collapsed;

            border.Visibility = Visibility.Visible;
        }


        public void DisableForTwo(Border border,Border border1)
        {
            BrAllExpenses.Visibility = Visibility.Collapsed;
            BrPaymentSum.Visibility = Visibility.Collapsed;
            BrRunningOutProduct.Visibility = Visibility.Collapsed;
            BrCargoReport.Visibility = Visibility.Collapsed;

            border.Visibility = Visibility.Visible;
            border1.Visibility = Visibility.Visible;    
        }

    }
}
