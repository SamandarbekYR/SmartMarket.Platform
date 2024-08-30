using SmartMarketDeskop.Integrated.Interfaces.Workers.Position;
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

        string Base_Url = "";
        public async Task<bool> CreatePosition(PositionView positionVoew)
        {
            try
            {
                HttpClient client = new HttpClient();   
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PositionView>> GetAllPosition()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PositionView positionView)
        {
            throw new NotImplementedException();
        }
    }
}
