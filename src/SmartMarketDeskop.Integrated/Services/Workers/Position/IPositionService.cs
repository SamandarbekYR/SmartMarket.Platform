using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Position
{
    public interface IPositionService
    {
        Task<bool> CreateProduct(PositionDto dto);
        Task<bool> UpdateProduct(PositionDto positionDto, Guid Id);
        Task<bool> DeleteProduct(Guid Id);
        Task<List<ProductViewModels>> GetAll();
    }
}
