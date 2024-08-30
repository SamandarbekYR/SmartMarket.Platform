using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Workers.Position
{
    public interface IPositionService 
    {
        Task<bool> CreatePosition(PositionView positionVoew);
        Task<List<PositionView>> GetAllPosition();
        Task<bool> Update(PositionView positionView);
        Task<bool> Delete(Guid Id);
    }
}
