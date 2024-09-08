using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Position
{
    public class PositionService : IPositionService
    {
        public Task<bool> CreateProduct(PositionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModels>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(PositionDto positionDto, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
