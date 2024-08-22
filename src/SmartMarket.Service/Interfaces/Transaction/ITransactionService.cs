using SmartMarket.Service.DTOs.Transaction;

namespace SmartMarket.Service.Interfaces.Transaction;

public interface ITransactionService
{
    Task<bool> AddAsync(AddTransactionDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<TransactionDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddTransactionDto dto, Guid Id);
}
