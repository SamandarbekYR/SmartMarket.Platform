using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Service.DTOs.PayDesks;
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
        private PayDesksDto _payDesk;

        public CheckOutFirstPage()
        {
            InitializeComponent();
            _cashReportPage = new CashReportPage();
            _expenseService = new ExpenseService();
            _salesRequestsService = new SalesRequestService();
        }

        public async void SetPayDesk(PayDesksDto payDesk)
        {
            _payDesk = payDesk;
            await SalesMoney();
            await Expenses();
            CurrentlyAvailable();
        }

        private async Task<(double cashSum, double cardSum, double debtSum, double transferMoney)> SalesMoney()
        {

            var salesMoney = await _salesRequestsService.GetAll();
            var salesMoneyForPayDesk = salesMoney.Where(sr => sr.PayDeskId == _payDesk.Id).ToList();

            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                salesMoneyForPayDesk = salesMoneyForPayDesk.Where(
                    sr => sr.CreatedDate >= fromDateTime.SelectedDate 
                    && sr.CreatedDate <= toDateTime.SelectedDate).ToList();
            }
            else
            {
                salesMoneyForPayDesk = salesMoneyForPayDesk.Where(
                     sr => sr.CreatedDate!.Value.Date == DateTime.Today).ToList();
            }

            var cashSum = salesMoneyForPayDesk.Sum(sr => sr.CashSum);
            var cardSum = salesMoneyForPayDesk.Sum(sr => sr.CardSum);
            var debtSum = salesMoneyForPayDesk.Sum(sr => sr.DebtSum);
            var transferMoney = salesMoneyForPayDesk.Sum(sr => sr.TransferMoney);

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
            var expensesForPayDesk = expenses.Where(e => e.PayDeskId == _payDesk.Id).ToList();

            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                expensesForPayDesk = expensesForPayDesk.Where(
                    e => e.CreatedDate >= fromDateTime.SelectedDate 
                    && e.CreatedDate <= toDateTime.SelectedDate).ToList();
            }
            else
            {
                expensesForPayDesk = expensesForPayDesk.Where(
                                       e => e.CreatedDate.Date == DateTime.Today).ToList();
            }

            var cashSum = expensesForPayDesk.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var cardSum = expensesForPayDesk.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var transferMoney = expensesForPayDesk.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "Do'kon uchun").Sum(e => e.Amount);
            var totalSumForShop = cashSum + cardSum + transferMoney;
            Label_Naqd_Dokon.Content = cashSum;
            Label_Karta_Dokon.Content = cardSum;
            Label_Pul_Dokon.Content = transferMoney;
            Label_Jami_Dokon.Content = totalSumForShop;

            var cashSumForHimself = expensesForPayDesk.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var cardSumForHimself = expensesForPayDesk.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var transferMoneyForHimself = expensesForPayDesk.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "O'z hisobi uchun").Sum(e => e.Amount);
            var totalSumForHimself = cashSumForHimself + cardSumForHimself + transferMoneyForHimself;
            Label_Naqd_Hisob.Content = cashSumForHimself;
            Label_Karta_Hisob.Content = cardSumForHimself;
            Label_Pul_Hisob.Content = transferMoneyForHimself;
            Label_Jami_Hisob.Content = totalSumForHimself;

            var cashSumForCargo = expensesForPayDesk.Where(e => e.TypeOfPayment == "Naqd" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var cardSumForCargo = expensesForPayDesk.Where(e => e.TypeOfPayment == "Karta" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var transferMoneyForCargo = expensesForPayDesk.Where(e => e.TypeOfPayment == "Pul ko'chirish" && e.Reason == "Yuk uchun").Sum(e => e.Amount);
            var totalSumForCargo = cashSumForCargo + cardSumForCargo + transferMoneyForCargo;
            Label_Naqd_Yuk.Content = cashSumForCargo;
            Label_Karta_Yuk.Content = cardSumForCargo;
            Label_Pul_Yuk.Content = transferMoneyForCargo;
            Label_Jami_Yuk.Content = totalSumForCargo;

            var totalCashSum = cashSum + cashSumForHimself + cashSumForCargo;
            var totalCardSum = cardSum + cardSumForHimself + cardSumForCargo;
            var totalTransferMoney = transferMoney + transferMoneyForHimself + transferMoneyForCargo;

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
