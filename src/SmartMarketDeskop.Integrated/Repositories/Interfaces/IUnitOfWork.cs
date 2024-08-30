using SmartMarketDeskop.Integrated.Repositories.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Customers;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Orders;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Partners;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.PartnersCompany;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.PayDesks;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Transactions;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICategory Category { get;}
    ICustomer Customer { get;}
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