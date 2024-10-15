using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.WorkerRoles
{
    public interface IWorkerRoleService
    {
        Task<List<WorkerRole>> GetAllAsync();
    }
}
