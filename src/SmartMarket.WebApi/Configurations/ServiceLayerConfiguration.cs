﻿using SmartMarket.DataAccess.Interfaces;
using SmartMarket.DataAccess.Repositories;
using SmartMarket.Service.Interfaces.Auth;
using SmartMarket.Service.Interfaces.Category;
using SmartMarket.Service.Interfaces.Common;
using SmartMarket.Service.Interfaces.Customer;
using SmartMarket.Service.Interfaces.Expence;
using SmartMarket.Service.Interfaces.Order;
using SmartMarket.Service.Interfaces.Partner;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgent;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.Interfaces.PartnersCompany.PartnerCompany;
using SmartMarket.Service.Interfaces.PayDesks;
using SmartMarket.Service.Interfaces.Products.Debtor;
using SmartMarket.Service.Interfaces.Products.DebtPayment;
using SmartMarket.Service.Interfaces.Products.InvalidProduct;
using SmartMarket.Service.Interfaces.Products.LoadReport;
using SmartMarket.Service.Interfaces.Products.Product;
using SmartMarket.Service.Interfaces.Products.ProductSale;
using SmartMarket.Service.Interfaces.Products.ReplaceProduct;
using SmartMarket.Service.Interfaces.Transaction;
using SmartMarket.Service.Interfaces.Worker.Positions;
using SmartMarket.Service.Interfaces.Worker.Salary;
using SmartMarket.Service.Interfaces.Worker.SalaryCheck;
using SmartMarket.Service.Interfaces.Worker.SalaryWorker;
using SmartMarket.Service.Interfaces.Worker.WorkerDebt;
using SmartMarket.Service.Interfaces.Worker.WorkerRole;
using SmartMarket.Service.Interfaces.Worker.Workers;
using SmartMarket.Service.Services.Auth;
using SmartMarket.Service.Services.Category;
using SmartMarket.Service.Services.Common;
using SmartMarket.Service.Services.Customer;
using SmartMarket.Service.Services.Expence;
using SmartMarket.Service.Services.Order;
using SmartMarket.Service.Services.Partner;
using SmartMarket.Service.Services.PartnersCompany.ContrAgent;
using SmartMarket.Service.Services.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.Services.PartnersCompany.PartnerCompany;
using SmartMarket.Service.Services.PayDesks;
using SmartMarket.Service.Services.Products.Debtor;
using SmartMarket.Service.Services.Products.DebtPayment;
using SmartMarket.Service.Services.Products.InvalidProduct;
using SmartMarket.Service.Services.Products.LoadReport;
using SmartMarket.Service.Services.Products.Product;
using SmartMarket.Service.Services.Products.ProductSale;
using SmartMarket.Service.Services.Products.ReplaceProduct;
using SmartMarket.Service.Services.Transaction;
using SmartMarket.Service.Services.Worker.Positions;
using SmartMarket.Service.Services.Worker.Salary;
using SmartMarket.Service.Services.Worker.SalaryCheck;
using SmartMarket.Service.Services.Worker.SalaryWorker;
using SmartMarket.Service.Services.Worker.WorkerDebt;
using SmartMarket.Service.Services.Worker.WorkerRole;
using SmartMarket.Service.Services.Worker.Workers;

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