﻿using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Interfaces.Workers;

public interface ISalaryWorker : IRepository<SalaryWorkerView>
{
    public Task<List<SalaryWorkerView>> GetSalaryWorkersFullInformationAsync();
}