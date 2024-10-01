using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Interfaces.PartnersCompany.ContrAgent
{
    public interface IContrAgentService
    {
        Task<bool> AddAsync(AddContrAgentDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<IEnumerable<ContrAgentDto>> GetAllAsync(PaginationParams @params);
        Task<bool> UpdateAsync(AddContrAgentDto dto, Guid Id);
        Task<ContrAgentDto> GetContrAgentByNameAsync(string name);
        Task<ContrAgentDto> GetContrAgentByNumberAsync(string number);
    }
}