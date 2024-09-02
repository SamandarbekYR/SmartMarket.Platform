using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Position
{
    public class PositionService : IPosition
    {
        public Task<bool> Add(PositionView entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PositionView> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PositionView?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(PositionView entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PositionView entity)
        {
            throw new NotImplementedException();
        }
    }
}
