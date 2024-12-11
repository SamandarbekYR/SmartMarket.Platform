using CT = SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;

public class ContrAgentService : IContrAgentService
{

    private IContrAgentServer contrAgentServer;
    private IPartnerComanyServer partnerComanyServer;
    public ContrAgentService()
    {
        this.contrAgentServer=new ContrAgentServer();
        this.partnerComanyServer=new PartnerCompanyServer();
    }


    public async Task<bool> AddAsync(ContrAgentDto dto)
    {
       if(IsInternetAvailable())
        {
            dto.IsSynced = true;    
            await contrAgentServer.AddAsync(dto);
            return true;    
        }
       else
        {

            // local bazaga saqlanadi
            return false;
        }
    } 

    public async Task<bool> DeleteAsync(Guid Id)
    {
        if(IsInternetAvailable())
        {
            await contrAgentServer.DeleteAsync(Id);
            return true;
        }
        else
        {
            return false;   
        }
    }

    public async Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id)
    {
        if(IsInternetAvailable())
        {
            await contrAgentServer.UpdateAsync(dto,Id);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInternetAvailable()
    {
        try
        {
            using (var client = new WebClient()!)
            using (client.OpenRead("http://google.com"))
                return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<ContrAgentViewModels>> GetAll()
    {
        if (IsInternetAvailable())
        {
            var contragents= await contrAgentServer.GetAllAsync();

            return contragents.Select(a => new ContrAgentViewModels()
            {
                Id = a.Id,
                CompanyName = a.PartnerCompany.Name,
                FirstName=a.FirstName,
                LastName=a.LastName,
                PhoneNumber=a.PhoneNumber,
                DebtSum= Convert.ToDecimal(a.ContrAgentPayment?.Sum(c => c.TotalDebt)),
                PayedSum= Convert.ToDecimal(a.ContrAgentPayment?.Sum(c => c.LastPayment)),
                LastPayedSum=Convert.ToDecimal(a.ContrAgentPayment?.OrderByDescending(c => c.LastPaymentDate)
                    .FirstOrDefault()?.LastPayment ?? 0),
                LastPayedDate = a.ContrAgentPayment?.OrderByDescending(c => c.LastPaymentDate)
                    .FirstOrDefault()?.LastPaymentDate.ToString("MM/dd/yyyy") ?? "No Payment"
            }).ToList();
        }
        else
        {
            return new List<ContrAgentViewModels>();
        }
    }

    public async Task<List<ContrAgentViewModels>> GetByName(string name)
    {
        if(IsInternetAvailable())
        {
            var contrAgents = await contrAgentServer.GetContrAgentByNameAsync(name);

            return contrAgents.Select(a => new ContrAgentViewModels()
            {
                Id = a.Id,
                CompanyName = a.PartnerCompany.Name,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhoneNumber = a.PhoneNumber,
                DebtSum = Convert.ToDecimal(a.ContrAgentPayment?.Sum(c => c.TotalDebt)),
                PayedSum = Convert.ToDecimal(a.ContrAgentPayment?.Sum(c => c.LastPayment)),
                LastPayedSum = Convert.ToDecimal(a.ContrAgentPayment?.OrderByDescending(c => c.LastPaymentDate)
                    .FirstOrDefault()?.LastPayment ?? 0),
                LastPayedDate = a.ContrAgentPayment?.OrderByDescending(c => c.LastPaymentDate)
                    .FirstOrDefault()?.LastPaymentDate.ToString("MM/dd/yyyy") ?? "No Payment"
            }).ToList();
        }
        else
        {
            return new List<ContrAgentViewModels>();
        }
    }

    public async Task<CT.ContrAgentDto> GetById(Guid id)
    {
        if(IsInternetAvailable())
        { 
            var contrAgent = await contrAgentServer.GetByIdAsync(id);

            return contrAgent;
        }
        else
        {
            return new CT.ContrAgentDto();
        }
    }
}
