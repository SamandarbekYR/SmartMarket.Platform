using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces;
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
using SmartMarket.DataAccess.Repositories.Categories;
using SmartMarket.DataAccess.Repositories.Customers;
using SmartMarket.DataAccess.Repositories.Experses;
using SmartMarket.DataAccess.Repositories.Orders;
using SmartMarket.DataAccess.Repositories.Partners;
using SmartMarket.DataAccess.Repositories.PartnersCompany;
using SmartMarket.DataAccess.Repositories.PayDesks;
using SmartMarket.DataAccess.Repositories.Products;
using SmartMarket.DataAccess.Repositories.Transactions;
using SmartMarket.DataAccess.Repositories.Workers;
using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Repositories
{
    public class UnitOfWork(AppDbContext appDb) : IUnitOfWork, IDisposable
    {
        public ICategory Category { get; set; } = new CategoryRepository(appDb);
        public ICustomer Customer { get; set; } = new CustomerRepository(appDb);
        public IExpense Expense { get; set; } = new ExpenseRepository(appDb);
        public IOrder Order { get; set; } = new OrderRepository(appDb);
        public IPartner Partner { get; set; } = new PartnerRepository(appDb);
        public IContrAgent ContrAgent { get; set; } = new ContrAgentRepository(appDb);
        public IContrAgentPayment ContrAgentPayment { get; set; } = new ContrAgentPaymentRepository(appDb);
        public IPartnerCompany PartnerCompany { get; set; } = new PartnerCompanyRepository(appDb);
        public IPayDesk PayDesk { get; set; } = new PayDeskRepository(appDb);
        public IDebtors Debtors { get; set; } = new DebtorRepository(appDb);
        public IProduct Product { get; set; } = new ProductRepository(appDb);
        public IDebtPayment DebtPayment { get; set; } = new DebtPaymentRepository(appDb);
        public IInvalidProduct InvalidProduct { get; set; } = new InvalidProductRepository(appDb);
        public ILoadReport LoadReport { get; set; } = new LoadReportRepository(appDb);
        public IProductImage ProductImage { get; set; } = new ProductImageRepository(appDb);
        public IProductSale ProductSale { get; set; } = new ProductSaleRepository(appDb);
        public IReplaceProduct ReplaceProduct { get; set; } = new ReplaceProductRepository(appDb);
        public ITransaction Transaction { get; set; } = new TransactionRepository(appDb);
        public IPosition Position { get; set; } = new PositionRepository(appDb);
        public ISalesRequest SalesRequest { get; set; } = new SalesRequestRepository(appDb);
        public ISalary Salary { get; set; } = new SalaryRepository(appDb);
        public ISalaryCheck SalaryCheck { get; set; } = new SalaryCheckRepository(appDb);
        public ISalaryWorker SalaryWorker { get; set; } = new SalaryWorkerRepository(appDb);
        public IWorker Worker { get; set; } = new WorkerRepository(appDb);
        public IWorkerDebt WorkerDebt { get; set; } = new WorkerDebtRepository(appDb);
        public IWorkerRole WorkerRole { get; set; } = new WorkerRoleRepository(appDb);
        public IOrderItem OrderItem { get; set; } = new OrderItemRepository(appDb);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await appDb.SaveChangesAsync();
        }
    }
}
