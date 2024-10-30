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

        public void SetValuesExpenses(double  cashSum, double cardSum, double transferMoney)
        {
            tbExpenseCashSum.Text = cashSum.ToString();
            tbExpenseCardSum.Text = cardSum.ToString();
            tbExpenseTransferMoney.Text = transferMoney.ToString();
            var totalSum = cashSum + cardSum + transferMoney;
            tbExpenseGeneralSum.Text = totalSum.ToString();
        }

        public void SetValuesSalesMoney(double cashSum, double CardSum, double transferMoney, double debtSum)
        {
            tbSaleCashSum.Text = cashSum.ToString();
            tbSaleCardSum.Text = CardSum.ToString();
            tbSaleTransferMoney.Text = transferMoney.ToString();
            tbSaleDebtSum.Text = debtSum.ToString();
            var totalSum = cashSum + CardSum + transferMoney + debtSum;
            tbSaleGeneralSum.Text = totalSum.ToString();
        }

        private void SetValuesCurrentlyAvailable(double cashSum, double cardSum, double transferMoney, double debtSum)
        {
            tbCurrnetlyCashSum.Text = cashSum.ToString();
            tbCurrentlyCardSum.Text = cardSum.ToString();
            tbCurrnetlyTransferMoney.Text = transferMoney.ToString();
            tbCurrnetlyDebtSum.Text = debtSum.ToString();
            var totalSum = cashSum + cardSum + transferMoney + debtSum;
            tbCurrentlyGeneralSum.Text = totalSum.ToString();
        }
    }
}