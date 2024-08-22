using AutoMapper;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Customers;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.DTOs.DebtPayment;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.DTOs.Salary;
using SmartMarket.Service.DTOs.SalaryCheck;
using SmartMarket.Service.DTOs.Workers;

namespace SmartMarket.Service.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();

        /*-------------Worker----------*/

        CreateMap<AddWrokerDto, Worker>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

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
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

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
    }
}
