using SmartMarket.DataAccess.Interfaces.Categories;
using SmartMarket.DataAccess.Interfaces.Customers;
using SmartMarket.DataAccess.Interfaces.Expenses;
using SmartMarket.DataAccess.Interfaces.Orders;
using SmartMarket.DataAccess.Interfaces.Partners;
using SmartMarket.DataAccess.Interfaces.PartnersCompany;
using SmartMarket.DataAccess.Interfaces.PayDesks;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.DataAccess.Interfaces.Transactions;
using SmartMarket.DataAccess.Interfaces.Workers;

namespace SmartMarket.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        ICategory Category { get; set; }
        ICustomer Customer { get; set; }
        IExpense Expense { get; set; }
        IOrder Order { get; set; }
        IPartner Partner { get; set; }
        IContrAgent ContrAgent { get; set; }
        IContrAgentPayment ContrAgentPayment { get; set; }
        IPartnerCompany PartnerCompany { get; set; }
        IPayDesk PayDesk { get; set; }
        IDebtors Debtors { get; set; }
        IProduct Product { get; set; }
        IDebtPayment DebtPayment { get; set; }
        IInvalidProduct InvalidProduct { get; set; }
        ILoadReport LoadReport { get; set; }
        IProductImage ProductImage { get; set; }
        IProductSale ProductSale { get; set; }
        IReplaceProduct ReplaceProduct { get; set; }
        ITransaction Transaction { get; set; }
        IPosition Position { get; set; }
        ISalary Salary { get; set; }
        ISalaryCheck SalaryCheck { get; set; }
        ISalaryWorker SalaryWorker { get; set; }
        IWorker Worker { get; set; }
        IWorkerDebt WorkerDebt { get; set; }
        IWorkerRole WorkerRole { get; set; }
    }
}
