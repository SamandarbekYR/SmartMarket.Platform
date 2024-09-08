using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Workers
{
    public interface IWorkerRoleServer
    {
        Task<List<WorkerRole>> GetAll();
    }
}
