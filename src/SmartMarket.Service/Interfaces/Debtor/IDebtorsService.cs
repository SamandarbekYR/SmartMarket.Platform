using SmartMarket.Service.DTOs.Debtors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Interfaces.Debtor;

public interface IDebtorsService
{
    Task<bool> AddAsync(AddDebtorsDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<DebtorsDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddDebtorsDto dto, Guid Id);
}
