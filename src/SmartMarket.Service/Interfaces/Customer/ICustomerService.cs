﻿using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Customer;

namespace SmartMarket.Service.Interfaces.Customer;

public interface ICustomerService
{
    Task<bool> AddAsync(AddCustomerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<CustomerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddCustomerDto dto, Guid Id);

    Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm, PaginationParams @params);
}
