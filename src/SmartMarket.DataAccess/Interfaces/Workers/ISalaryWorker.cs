﻿using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Interfaces.Workers
{
    public interface ISalaryWorker : IRepository<SalaryWorker>
    {
        public Task<List<SalaryWorker>> GetSalaryWorkersFullInformationAsync();
    }
}
