using SmartMarket.Desktop.Components.CashReportForComponent;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.CashReportForPage
{
    /// <summary>
    /// Interaction logic for CashReportPage.xaml
    /// </summary>
    public partial class CashReportPage : Page
    {
        private IPayDeskService _payDeskService;
        public CashReportPage()
        {
            InitializeComponent();

            this._payDeskService = new PayDeskService();
        }

        public async void GetAllPayDesk()
        {
            var payDesks = await _payDeskService.GetAll();
            PayDeskLoader.Visibility = Visibility.Collapsed;

            St_PayDeskList.Children.Clear();

            foreach (var payDesk in payDesks)
            {
                CashReportComponent cashReportComponent = new CashReportComponent();
                cashReportComponent.Tag = payDesk;
                cashReportComponent.SetValues(payDesk.Name);
                St_PayDeskList.Children.Add(cashReportComponent);
            }
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

        public void SetValuesCurrentlyAvailable(double cashSum, double cardSum, double transferMoney, double debtSum)
        {
            tbCurrnetlyCashSum.Text = cashSum.ToString();
            tbCurrentlyCardSum.Text = cardSum.ToString();
            tbCurrnetlyTransferMoney.Text = transferMoney.ToString();
            tbCurrnetlyDebtSum.Text = debtSum.ToString();
            var totalSum = cashSum + cardSum + transferMoney + debtSum;
            tbCurrentlyGeneralSum.Text = totalSum.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllPayDesk();

            CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
            CheckOutPageNavigator.Content = checkOutFirstPage;
        }
    }
}