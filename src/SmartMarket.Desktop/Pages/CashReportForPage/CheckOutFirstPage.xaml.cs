using SmartMarket.Service.DTOs.Products.SalesRequest;

using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;

using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.CashReportForPage
{
    /// <summary>
    /// Interaction logic for CheckOutFirstPage.xaml
    /// </summary>
    public partial class CheckOutFirstPage : Page
    {
        private IExpenseService _expenseService;
        private ISalesRequestsService _salesRequestsService;
        private CashReportPage _cashReportPage;

        public CheckOutFirstPage()
        {
            InitializeComponent();
            _expenseService = new ExpenseService();
            _salesRequestsService = new SalesRequestService();
        }

        private async Task<(double cashSum, double cardSum, double debtSum, double transferMoney)> SalesMoney()
        {

            var salesMoney = await _salesRequestsService.GetAll();
            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                salesMoney = salesMoney.Where(
                    sr => sr.CreatedDate >= fromDateTime.SelectedDate 
                    && sr.CreatedDate <= toDateTime.SelectedDate).ToList();
            }
            else
            {
                 salesMoney = salesMoney.Where(
                     sr => sr.CreatedDate!.Value.Date == DateTime.Today).ToList();
            }

            var cashSum = salesMoney.Sum(sr => sr.CashSum);
            var cardSum = salesMoney.Sum(sr => sr.CardSum);
            var debtSum = salesMoney.Sum(sr => sr.DebtSum);
            var transferMoney = salesMoney.Sum(sr => sr.TransferMoney);

            Label_Naqd_Savdo.Content = cashSum;
            Label_Karta_Savdo.Content = cardSum;
            Label_Pul_Savdo.Content = transferMoney;
            Label_Nasiya_Savdo.Content = debtSum;

            Label_Jami_Savdo.Content = cashSum + cardSum + transferMoney + debtSum;

            return (cashSum, cardSum, debtSum, transferMoney);
        }

        private async Task<(double totalCashSum, double totalCardSum, double totalTransferMoney)> Expenses()
        {
            var expenses = await _expenseService.GetAll();
            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                expenses = expenses.Where(
                    e => e.CreatedDate >= fromDateTime.SelectedDate 
                    && e.CreatedDate <= toDateTime.SelectedDate).ToList();
            }
            else
            {
                expenses = expenses.Where(
                                       e => e.CreatedDate.Date == DateTime.Today).ToList();
            }

            var cashSum = expenses.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var cardSum = expenses.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var transferMoney = expenses.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var totalSumForShop = cashSum + cardSum + transferMoney;
            Label_Naqd_Dokon.Content = cashSum;
            Label_Karta_Dokon.Content = cardSum;
            Label_Pul_Dokon.Content = transferMoney;
            Label_Jami_Dokon.Content = totalSumForShop;


            var cashSumForHimself = expenses.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var cardSumForHimself = expenses.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var transferMoneyForHimself = expenses.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var totalSumForHimself = cashSumForHimself + cardSumForHimself + transferMoneyForHimself;
            Label_Naqd_Hisob.Content = cashSumForHimself;
            Label_Karta_Hisob.Content = cardSumForHimself;
            Label_Pul_Hisob.Content = transferMoneyForHimself;
            Label_Jami_Hisob.Content = totalSumForHimself;

            var cashSumForCargo = expenses.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var cardSumForCargo = expenses.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var transferMoneyForCargo = expenses.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var totalSumForCargo = cashSumForCargo + cardSumForCargo + transferMoneyForCargo;
            Label_Naqd_Yuk.Content = cashSumForCargo;
            Label_Karta_Yuk.Content = cardSumForCargo;
            Label_Pul_Yuk.Content = transferMoneyForCargo;
            Label_Jami_Yuk.Content = totalSumForCargo;

            var totalCashSum = cashSum + cashSumForHimself + cashSumForCargo;
            var totalCardSum = cardSum + cardSumForHimself + cardSumForCargo;
            var totalTransferMoney = transferMoney + transferMoneyForHimself + transferMoneyForCargo;

            _cashReportPage.SetValuesExpenses(totalCashSum, totalCardSum, totalTransferMoney);

            return (totalCashSum, totalCardSum, totalTransferMoney);
        }

        private async void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await SalesMoney();
            await Expenses();
            CurrentlyAvailable();
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await SalesMoney();
            await Expenses();
            CurrentlyAvailable();
        }

        private async void CurrentlyAvailable()
        {
            var salesMoney = await SalesMoney();
            var expenses = await Expenses();

            var totalCashSum = salesMoney.cashSum - expenses.totalCashSum;
            var totalCardSum = salesMoney.cardSum - expenses.totalCardSum;
            var totalTransferMoney = salesMoney.transferMoney - expenses.totalTransferMoney;

            Label_Naqd_All.Content = totalCashSum;
            Label_Karta_All.Content = totalCardSum;
            Label_Pul_All.Content = totalTransferMoney;
            Label_Nasiya_All.Content = salesMoney.debtSum;

           
        }
    }
}
