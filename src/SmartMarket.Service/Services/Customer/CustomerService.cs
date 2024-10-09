using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Customers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.Interfaces.Customer;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.Common.Extentions;
using Microsoft.Extensions.Logging;

namespace SmartMarket.Service.Services.Customer
{
    public class CustomerService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddCustomerDto> validator,
                                 ILogger<CustomerService> logger) : ICustomerService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddCustomerDto> _validator = validator;
        private readonly ILogger<CustomerService> _logger = logger; 

        public async Task<bool> AddAsync(AddCustomerDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var customer = _mapper.Map<Et.Customer>(dto);
                return await _unitOfWork.Customer.Add(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a customer."); 
                throw; 
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var customer = await _unitOfWork.Customer.GetById(Id);
                if (customer == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Customer not found.");
                }

                return await _unitOfWork.Customer.Remove(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a customer.");
                throw;
            }
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            try
            {
                var customers = await _unitOfWork.Customer.GetAll().ToListAsync();
                return _mapper.Map<List<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm, PaginationParams @params)
        {
            try
            {
                var customers = await _unitOfWork.Customer.GetAll()
                                                    .Where(c => c.FirstName.Contains(searchTerm))
                                                    .AsNoTracking()
                                                    .ToPagedListAsync(@params);

                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for customers.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddCustomerDto dto, Guid Id)
        {
            try
            {
                var customer = await _unitOfWork.Customer.GetById(Id);
                if (customer == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Customer not found.");
                }

                _mapper.Map(dto, customer);

                return await _unitOfWork.Customer.Update(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a customer.");
                throw;
            }
        }
    }
}