using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.PartnersCompany
{
    public interface IContrAgent : IRepository<ContrAgentView>
    {
        Task<List<ContrAgentView>> GetContrAgentsFullInformationAsync();
    }
}
