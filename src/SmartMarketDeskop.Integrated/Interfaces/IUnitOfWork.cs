using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Interfaces.Customers;
using SmartMarketDeskop.Integrated.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Interfaces.Orders;
using SmartMarketDeskop.Integrated.Interfaces.Partners;
using SmartMarketDeskop.Integrated.Interfaces.PartnersCompany;
using SmartMarketDeskop.Integrated.Interfaces.PayDesks;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDeskop.Integrated.Interfaces.Transactions;
using SmartMarketDeskop.Integrated.Interfaces.Workers;

namespace SmartMarketDeskop.Integrated.Interfaces;

public interface IUnitOfWork
{
    ICategory Category { get; }
    ICustomer Customer { get; }
    IExpense Expense { get; }
    IOrder Order { get; }
    IPartner Partner { get; }
    IContrAgent ContrAgent { get; }
    IContrAgentPayment ContrAgentPayment { get; }
    IPartnerCompany PartnerCompany { get; }
    IPayDesk PayDesk { get; }
    IDebtors Debtors { get; }
    IProduct Product { get; }
    IDebtPayment DebtPayment { get; }
    IInvalidProduct InvalidProduct { get; }
    ILoadReport LoadReport { get; }
    IProductImage ProductImage { get; }
    IProductSale ProductSale { get; }
    IReplaceProduct ReplaceProduct { get; }
    ITransaction Transaction { get; }
    IPosition Position { get; }
    ISalary Salary { get; }
    ISalaryCheck SalaryCheck { get; }
    ISalaryWorker SalaryWorker { get; }
    IWorker Worker { get; }
    IWorkerDebt WorkerDebt { get; }
    IWorkerRole WorkerRole { get; }
}