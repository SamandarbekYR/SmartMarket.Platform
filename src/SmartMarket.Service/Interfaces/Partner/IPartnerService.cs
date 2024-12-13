﻿using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Partner;

namespace SmartMarket.Service.Interfaces.Partner;

public interface IPartnerService
{
    Task<bool> AddAsync(AddPartnerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PartnerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddPartnerDto dto, Guid Id);
    Task<bool> UpdatePartnerDebtSumAsync(double debtSum, Guid Id);

    Task<PartnerDto> GetPartnerByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<PartnerDto>> FilterExpenceAsync(FilterPartnerDto filterDto);
    Task<PartnerDto> GetPartnerByFirstNameAsync(string phoneNumber);
}
