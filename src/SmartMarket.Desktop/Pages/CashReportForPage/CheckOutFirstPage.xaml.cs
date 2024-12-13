﻿using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgentPayments;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.Partners;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.CashReportForPage;

/// <summary>
/// Interaction logic for CheckOutFirstPage.xaml
/// </summary>
public partial class CheckOutFirstPage : Page
{
    private IContrAgentPaymentService _contrAgentPaymentService;
    private ISalesRequestsService _salesRequestsService;
    private IExpenseService _expenseService;
    private IPartnerService _partnerService;
    private CashReportPage _cashReportPage;
    private PayDesksDto _payDesk;
    private int count = 0;
    public CheckOutFirstPage(CashReportPage cashReportPage)
    {
        InitializeComponent();
        _partnerService = new PartnerService();
        _expenseService = new ExpenseService();
        _salesRequestsService = new SalesRequestService();
        _contrAgentPaymentService = new ContrAgentPaymentService();
        _cashReportPage = cashReportPage;
    }

    public void SetPayDesk(PayDesksDto payDesk)
    {
        _payDesk = payDesk;
        Kassa_Name.Content = payDesk.Name;
        Kassa_Name1.Content = payDesk.Name;
        Kassa_Name_Harajat.Content = payDesk.Name;

        FilterForCashReport();
        count++;
    }

    private async Task<(double cashSum, double cardSum, double debtSum, double transferMoney)> ShowSalesMoney(IList<SalesRequestDto> salesMoneyForPayDesk, IList<Partner> partnerDebt)
    {
        var partnercashSum = partnerDebt.Where(e => e.PaymentType == "Naqd").Sum(e => e.PaidDebt) ?? 0;
        var partnercardSum = partnerDebt.Where(e => e.PaymentType == "Karta").Sum(e => e.PaidDebt) ?? 0;
        var partnertransferMoney = partnerDebt.Where(e => e.PaymentType == "Pul ko'chirish").Sum(e => e.PaidDebt) ?? 0;
        
        Label_Naqd_Qarz.Content = partnercashSum;
        Label_Karta_Qarz.Content = partnercardSum;
        Label_Pul_Qarz.Content = partnertransferMoney;
        Label_Jami_Qarz.Content = partnercardSum + partnertransferMoney + partnercashSum;

        var cashSumSalesMoney = salesMoneyForPayDesk.Sum(sr => sr.CashSum);
        var cardSumSalesMoney = salesMoneyForPayDesk.Sum(sr => sr.CardSum);
        var debtSumSalesMoney = salesMoneyForPayDesk.Sum(sr => sr.DebtSum);
        var transferMoneySalesMoney = salesMoneyForPayDesk.Sum(sr => sr.TransferMoney);

        Label_Naqd_Savdo.Content = cashSumSalesMoney;
        Label_Karta_Savdo.Content = cardSumSalesMoney;
        Label_Pul_Savdo.Content = transferMoneySalesMoney;
        Label_Nasiya_Savdo.Content = debtSumSalesMoney;
        Label_Jami_Savdo.Content = cashSumSalesMoney + cardSumSalesMoney + transferMoneySalesMoney + debtSumSalesMoney;

        var totalCashSum = cashSumSalesMoney + partnercashSum;
        var totalCardSum = cardSumSalesMoney+ partnercardSum;
        var totalTransferMoney = transferMoneySalesMoney + partnertransferMoney;

        return (totalCashSum, totalCardSum, debtSumSalesMoney, totalTransferMoney);
    }

    private async Task<(double totalCashSum, double totalCardSum, double totalTransferMoney)> ShowExpenses(List<FullExpenceDto> expensesForPayDesk, List<ContrAgentPaymentDto> contrAgentPaymentForPayDesk)
    {
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
        
        cashSumForCargo += contrAgentPaymentForPayDesk.Where(cap => cap.PaymentType == "Naqd").Sum(p => p.LastPayment);
        cardSumForCargo += contrAgentPaymentForPayDesk.Where(cap => cap.PaymentType == "Karta").Sum(p => p.LastPayment);
        transferMoneyForCargo += contrAgentPaymentForPayDesk.Where(cap => cap.PaymentType == "Pul ko'chirish").Sum(p => p.LastPayment);

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

    private async void FilterForCashReport()
    {
        FilterSalesRequestDto salesRequestDto = new FilterSalesRequestDto();
        FilterContrAgentDto contrAgentDto = new FilterContrAgentDto();
        FilterExpenseDto expenseDto = new FilterExpenseDto();
        FilterPartnerDto partnerDto = new FilterPartnerDto();

        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            salesRequestDto.FromDateTime = fromDateTime.SelectedDate;
            salesRequestDto.ToDateTime = toDateTime.SelectedDate;
            expenseDto.FromDateTime = fromDateTime.SelectedDate;
            expenseDto.ToDateTime = toDateTime.SelectedDate;
            partnerDto.FromDateTime = fromDateTime.SelectedDate;
            partnerDto.ToDateTime = toDateTime.SelectedDate;
            contrAgentDto.FromDateTime = fromDateTime.SelectedDate;
            contrAgentDto.ToDateTime= toDateTime.SelectedDate;
        }

        if (_payDesk?.Id != null)
        {
            salesRequestDto.PayDeskId = _payDesk.Id;
            expenseDto.PayDeskId = _payDesk.Id;
            partnerDto.Id = _payDesk.Id;
            contrAgentDto.PayDeskId = _payDesk.Id;
        }

        var contrAgentPaymentData = await Task.Run(async () => await _contrAgentPaymentService.FilterAsync(contrAgentDto));
        var salesMoneyData = await Task.Run(async () => await _salesRequestsService.FilterSalesRequest(salesRequestDto));
        var expensesData = await Task.Run(async () => await _expenseService.FilterExpense(expenseDto));
        var partnerData = await Task.Run(async () => await _partnerService.FilterPartnerAsync(partnerDto));

        var salesMoney = await ShowSalesMoney(salesMoneyData, partnerData);
        var expenses = await ShowExpenses(expensesData, contrAgentPaymentData);
        CurrentlyAvailable(salesMoney, expenses);
    }

    private async void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterForCashReport();
    }

    private void CurrentlyAvailable((double cashSum, double cardSum, double debtSum, double transferMoney) salesMoney,
                                    (double totalCashSum, double totalCardSum, double totalTransferMoney) expenses)
    {
        var totalCashSum = salesMoney.cashSum - expenses.totalCashSum;
        var totalCardSum = salesMoney.cardSum - expenses.totalCardSum;
        var totalTransferMoney = salesMoney.transferMoney - expenses.totalTransferMoney;

        _cashReportPage.SetValuesSalesMoney(totalCashSum, totalCardSum, totalTransferMoney, salesMoney.debtSum);
        _cashReportPage.SetValuesExpenses(expenses.totalCashSum, expenses.totalCardSum, expenses.totalTransferMoney);
        _cashReportPage.SetValuesCurrentlyAvailable(totalCashSum, totalCardSum, totalTransferMoney, salesMoney.debtSum);

        Label_Naqd_All.Content = totalCashSum;
        Label_Karta_All.Content = totalCardSum;
        Label_Pul_All.Content = totalTransferMoney;
        Label_Nasiya_All.Content = salesMoney.debtSum;
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (count == 0)
        {
            var salesMoneyData = await Task.Run(async () => await _salesRequestsService.FilterSalesRequest(new FilterSalesRequestDto()));
            var contrAgentPaymentData = await Task.Run(async () => await _contrAgentPaymentService.FilterAsync(new FilterContrAgentDto()));
            var partnerData = await Task.Run(async () => await _partnerService.FilterPartnerAsync(new FilterPartnerDto()));
            var expensesData = await Task.Run(async () => await _expenseService.FilterExpense(new FilterExpenseDto()));

            var salesMoney = await ShowSalesMoney(salesMoneyData, partnerData);
            var expenses = await ShowExpenses(expensesData, contrAgentPaymentData);
            CurrentlyAvailable(salesMoney, expenses);
        }
    }

    private void BtnCreatePayDesk_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        PayDeskCreateWindow window = new PayDeskCreateWindow();
        window.ShowDialog();
    }
}
