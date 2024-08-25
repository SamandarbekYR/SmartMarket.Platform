using SmartMarket.DataAccess.Interfaces;
using SmartMarket.DataAccess.Repositories;
using SmartMarket.Service.Interfaces.Auth;
using SmartMarket.Service.Interfaces.Category;
using SmartMarket.Service.Interfaces.Common;
using SmartMarket.Service.Interfaces.ContrAgent;
using SmartMarket.Service.Interfaces.ContrAgentPayment;
using SmartMarket.Service.Interfaces.Customer;
using SmartMarket.Service.Interfaces.Debtor;
using SmartMarket.Service.Interfaces.DebtPayment;
using SmartMarket.Service.Interfaces.Expence;
using SmartMarket.Service.Interfaces.InvalidProduct;
using SmartMarket.Service.Interfaces.LoadReport;
using SmartMarket.Service.Interfaces.Order;
using SmartMarket.Service.Interfaces.Partner;
using SmartMarket.Service.Interfaces.PartnerCompany;
using SmartMarket.Service.Interfaces.PayDesks;
using SmartMarket.Service.Interfaces.Positions;
using SmartMarket.Service.Interfaces.Product;
using SmartMarket.Service.Interfaces.ProductSale;
using SmartMarket.Service.Interfaces.ReplaceProduct;
using SmartMarket.Service.Interfaces.Salary;
using SmartMarket.Service.Interfaces.SalaryCheck;
using SmartMarket.Service.Interfaces.SalaryWorker;
using SmartMarket.Service.Interfaces.Transaction;
using SmartMarket.Service.Interfaces.WorkerDebt;
using SmartMarket.Service.Interfaces.WorkerRole;
using SmartMarket.Service.Interfaces.Workers;
using SmartMarket.Service.Services.Auth;
using SmartMarket.Service.Services.Category;
using SmartMarket.Service.Services.Common;
using SmartMarket.Service.Services.ContrAgent;
using SmartMarket.Service.Services.ContrAgentPayment;
using SmartMarket.Service.Services.Customer;
using SmartMarket.Service.Services.Debtors;
using SmartMarket.Service.Services.DebtPayment;
using SmartMarket.Service.Services.Expence;
using SmartMarket.Service.Services.InvalidProduct;
using SmartMarket.Service.Services.LoadReport;
using SmartMarket.Service.Services.Order;
using SmartMarket.Service.Services.Partner;
using SmartMarket.Service.Services.PartnerCompany;
using SmartMarket.Service.Services.PayDesks;
using SmartMarket.Service.Services.Positions;
using SmartMarket.Service.Services.Product;
using SmartMarket.Service.Services.ProductSale;
using SmartMarket.Service.Services.ReplaceProduct;
using SmartMarket.Service.Services.Salary;
using SmartMarket.Service.Services.SalaryCheck;
using SmartMarket.Service.Services.SalaryWorker;
using SmartMarket.Service.Services.Transaction;
using SmartMarket.Service.Services.WorkerDebt;
using SmartMarket.Service.Services.WorkerRole;
using SmartMarket.Service.Services.Workers;

namespace SmartMarket.WebApi.Configurations
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder )
        {
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<ISalaryService, SalaryService>();
            builder.Services.AddScoped<ISalaryCheckService, SalaryCheckService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IExpenceService, ExpenceService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPartnerService, PartnerService>();
            builder.Services.AddScoped<IPayDeskService, PayDeskService>();
            builder.Services.AddScoped<IDebtPaymentService, DebtPaymentService>();
            builder.Services.AddScoped<IDebtorsService, DebtorsService>();
            builder.Services.AddScoped<IInvalidProductService, InvalidProductService>();
            builder.Services.AddScoped<ILoadReportService, LoadReportService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductSaleService, ProductSaleService>();
            builder.Services.AddScoped<IReplaceProductService, ReplaceProductService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ISalaryWorkerService, SalaryWorkerService>();
            builder.Services.AddScoped<IWorkerDebtService, WorkerDebtService>();
            builder.Services.AddScoped<IWorkerRoleService, WorkerRoleService>();
            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<IContrAgentService, ContrAgentService>();
            builder.Services.AddScoped<IContrAgentPaymentService, ContrAgentPaymentService>();
            builder.Services.AddScoped<IPartnerCompanyService, PartnerCompanyService>();
        }
    }
}
