using AutoMapper;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Customers;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Scales;
using SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Products.Debtors;
using SmartMarket.Service.DTOs.Products.DebtPayment;
using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.DTOs.Scales;
using SmartMarket.Service.DTOs.Transaction;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarket.Service.DTOs.Workers.Salary;
using SmartMarket.Service.DTOs.Workers.SalaryCheck;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.DTOs.Workers.WorkerDebt;
using SmartMarket.Service.DTOs.Workers.WorkerRole;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarket.Service.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();

        /*-------------Worker----------*/

        CreateMap<AddWorkerDto, Worker>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ImgPath, opt => opt.Ignore());

        CreateMap<Worker, WorkerDto>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.WorkerRole, opt => opt.MapFrom(src => src.WorkerRole))
            .ForMember(dest => dest.SalaryChecks, opt => opt.MapFrom(src => src.SalaryChecks))
            .ForMember(dest => dest.WorkerDebts, opt => opt.MapFrom(src => src.WorkerDebts))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            .ForMember(dest => dest.ProductSales, opt => opt.MapFrom(src => src.ProductSales))
            .ForMember(dest => dest.LoadReports, opt => opt.MapFrom(src => src.LoadReports))
            .ForMember(dest => dest.ReplaceProducts, opt => opt.MapFrom(src => src.ReplaceProducts))
            .ForMember(dest => dest.InvalidProducts, opt => opt.MapFrom(src => src.InvalidProducts))
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
            .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses))
            .ReverseMap();

        CreateMap<Worker, WorkerDto>();

        /*---------Position-----------*/

        CreateMap<AddPositionDto, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Position, PositionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*--------Salary---------------*/

        CreateMap<AddSalaryDto, Salary>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Salary, SalaryDto>();

        /*-------SalaryCheck----------*/

        CreateMap<AddSalaryCheckDto, SalaryCheck>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SalaryCheck, SalaryCheckDto>();

        /*--------Customer-----------------*/

        CreateMap<AddCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Customer, CustomerDto>();

        /*--------Expence--------------*/

        CreateMap<AddExpenceDto, Expense>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Expense, ExpenceDto>();

        /*---------Order-----------------*/
        CreateMap<AddOrderDto, Order>()
            .ForMember(dest => dest.ProductOrderItems, opt => opt.MapFrom(src => src.ProductOrderItems));

        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.ProductOrderItems, opt => opt.MapFrom(src => src.ProductOrderItems));

        CreateMap<Order, OrderDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ProductOrderItems, opt => opt.MapFrom(src => src.ProductOrderItems));

        CreateMap<AddOrderProductDto, OrderProduct>();
        CreateMap<UpdateOrderProductDto, OrderProduct>();

        /*---------Partner-----------------*/
        CreateMap<AddPartnerDto, Partner>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Partner, PartnerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------PayDesk-----------------*/
        CreateMap<AddPayDesksDto, PayDesk>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PayDesk, PayDesksDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------DebtPayment-----------------*/
        CreateMap<AddDebtPaymentDto, DebtPayment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<DebtPayment, DebtPaymentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------Debtors-----------------*/
        CreateMap<AddDebtorsDto, Debtors>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Debtors, DebtorsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------InvalidProduct-----------------*/
        CreateMap<AddInvalidProductDto, InvalidProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<InvalidProduct, InvalidProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------LoadReport-----------------*/
        CreateMap<AddLoadReportDto, LoadReport>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<LoadReport, LoadReportDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------Product-----------------*/
        CreateMap<AddProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------ProductImage-----------------*/
        //ozgarishligi mumkin
        CreateMap<AddProductImageDto, ProductImage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ProductImage, ProductImageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------ProductSale-----------------*/

        CreateMap<ProductSale, ProductSaleViewModel>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.ReplaceProducts, opt => opt.MapFrom(src => src.ReplaceProducts))
            .ForMember(dest => dest.InvalidProducts, opt => opt.MapFrom(src => src.InvalidProducts));

        CreateMap<AddProductSaleDto, ProductSale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ProductSale, ProductSaleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------SalesRequest-----------------*/
        CreateMap<AddSalesRequestDto, SalesRequest>()
            .ForMember(dest => dest.ProductSaleItems, opt => opt.MapFrom(src => src.ProductSaleItems));

        CreateMap<AddProductSaleDto, ProductSale>();

        CreateMap<SalesRequest, SalesRequestDto>()
            .ForMember(dest => dest.Worker, opt => opt.MapFrom(src => src.Worker))
            .ForMember(dest => dest.PayDesk, opt => opt.MapFrom(src => src.PayDesk))
            .ForMember(dest => dest.ProductSaleItems, opt => opt.MapFrom(src => src.ProductSaleItems))
            .ReverseMap();

        /*---------Scale-----------------*/
        CreateMap<AddScaleDto, Scale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Scale, ScaleDto>();

        CreateMap<ScaleDto, Scale>();

        /*---------ReplaceProduct-----------------*/
        CreateMap<AddReplaceProductDto, ReplaceProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ReplaceProduct, ReplaceProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductSale, opt => opt.MapFrom(src => src.ProductSale))
            .ForMember(dest => dest.Worker, opt => opt.MapFrom(src => src.Worker));

        /*---------Transaction-----------------*/
        CreateMap<AddTransactionDto, Transaction>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------SalaryWorker-----------------*/
        CreateMap<AddSalaryWorkerDto, SalaryWorker>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SalaryWorker, SalaryWorkerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------WorkerDebt-----------------*/
        CreateMap<AddWorkerDebtDto, WorkerDebt>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<WorkerDebt, WorkerDebtDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------WorkerRole-----------------*/
        CreateMap<AddWorkerRoleDto, WorkerRole>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<WorkerRole, WorkerRoleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------ContrAgent-----------------*/
        CreateMap<AddContrAgentDto, ContrAgent>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ContrAgent, ContrAgentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------ContrAgentPayment-----------------*/
        CreateMap<AddContrAgentPaymentDto, ContrAgentPayment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ContrAgentPayment, ContrAgentPaymentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        /*---------PartnerCompany-----------------*/
        CreateMap<AddPartnerCompanyDto, PartnerCompany>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PartnerCompany, PartnerCompanyDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Product, Product>(); 
        CreateMap<Worker, Worker>(); 
        CreateMap<Transaction, Transaction>();
        CreateMap<PayDesk, PayDesk>();
        CreateMap<ReplaceProduct, ReplaceProduct>();
        CreateMap<InvalidProduct, InvalidProduct>();
    }
}
