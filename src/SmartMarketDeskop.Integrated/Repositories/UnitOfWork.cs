using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Interfaces.Customers;
using SmartMarketDeskop.Integrated.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Interfaces.Orders;
using SmartMarketDeskop.Integrated.Interfaces.Partners;
using SmartMarketDeskop.Integrated.Interfaces.PartnersCompany;
using SmartMarketDeskop.Integrated.Interfaces.PayDesks;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDeskop.Integrated.Interfaces.Scales;
using SmartMarketDeskop.Integrated.Interfaces.Transactions;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Repositories.Categories;
using SmartMarketDeskop.Integrated.Repositories.Customers;
using SmartMarketDeskop.Integrated.Repositories.Expenses;
using SmartMarketDeskop.Integrated.Repositories.Orders;
using SmartMarketDeskop.Integrated.Repositories.Partners;
using SmartMarketDeskop.Integrated.Repositories.PartnersCompany;
using SmartMarketDeskop.Integrated.Repositories.PayDesks;
using SmartMarketDeskop.Integrated.Repositories.Products;
using SmartMarketDeskop.Integrated.Repositories.Scales;
using SmartMarketDeskop.Integrated.Repositories.Transactions;
using SmartMarketDeskop.Integrated.Repositories.Workers;

namespace SmartMarketDeskop.Integrated.Repositories
{
    public class UnitOfWork(AppDbContext appDb) : IUnitOfWork
    {
        private readonly AppDbContext _appDb = appDb;

        public ICategory Category => new CategoryRepository(_appDb);

        public ICustomer Customer => new CustomerRepository(_appDb);

        public IExpense Expense => new ExpenseRepository(_appDb);

        public IOrder Order => new OrderRepository(_appDb);

        public IPartner Partner => new PartnerRepository(_appDb);

        public IContrAgent ContrAgent => new ContrAgentRepository(_appDb);

        public IContrAgentPayment ContrAgentPayment => new ContrAgentPaymentRepository(_appDb);

        public IPartnerCompany PartnerCompany => new PartnerCompanyRepository(_appDb);

        public IPayDesk PayDesk => new PayDeskRepository(_appDb);

        public IDebtors Debtors => new DebtorRepository(_appDb);

        public IProduct Product => new ProductRepository(_appDb);

        public IDebtPayment DebtPayment => new DebtPaymentRepository(_appDb);

        public IInvalidProduct InvalidProduct => new InvalidProductRepository(_appDb);

        public ILoadReport LoadReport => new LoadReportRepository(_appDb);

        public IProductImage ProductImage => new ProductImageRepository(_appDb);

        public IProductSale ProductSale => new ProductSaleRepository(_appDb);

        public IReplaceProduct ReplaceProduct => new ReplaceProductRepository(_appDb);

        public ITransaction Transaction => new TransactionRepository(_appDb);

        public IPosition Position => new PositionRepository(_appDb);

        public ISalary Salary => new SalaryRepository(_appDb);
        public IScale Scale => new ScaleRepository(_appDb);

        public ISalaryCheck SalaryCheck => new SalaryCheckRepository(_appDb);

        public ISalaryWorker SalaryWorker => new SalaryWorkerRepository(_appDb);

        public IWorker Worker => new WorkerRepository(_appDb);

        public IWorkerDebt WorkerDebt => new WorkerDebtRepository(_appDb);

        public IWorkerRole WorkerRole => new WorkerRoleRepository(_appDb);
    }
}
