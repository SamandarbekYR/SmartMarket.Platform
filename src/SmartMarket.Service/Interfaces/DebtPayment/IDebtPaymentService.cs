using SmartMarket.Service.DTOs.DebtPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Interfaces.DebtPayment;

public interface IDebtPaymentService
{
    Task<bool> AddAsync(AddDebtPaymentDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<DebtPaymentDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddDebtPaymentDto dto, Guid Id);
}
