using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany;

using System.Net;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgentPayments;

public class ContrAgentPaymentService : IContrAgentPaymentService
{
    private readonly IContrAgentPaymentServer contrAgentPaymentServer;
    public ContrAgentPaymentService()
    {
        this.contrAgentPaymentServer = new ContrAgentPaymentServer();
    }

    public async Task<bool> AddAsync(AddContrAgentPaymentDto dto)
    {
        if(IsInternetAvailable())
        {
            await contrAgentPaymentServer.AddAsync(dto);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(AddContrAgentPaymentDto dto)
    {
        if (IsInternetAvailable())
        {
            await contrAgentPaymentServer.UpdateAsync(dto);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<ContrAgentPaymentDto>> FilterAsync(FilterContrAgentDto filter)
    {
        if (IsInternetAvailable())
        {
            var contrAgentPayments = await contrAgentPaymentServer.FilterContrAgentPaymentsAsync(filter);
            return contrAgentPayments;
        }
        else
        {
            return new List<ContrAgentPaymentDto>();
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

    public async Task<List<ContrAgentPaymentDto>> GetAllByContrAgentIdAsync(Guid Id)
    {
        if (IsInternetAvailable())
        {
            var result = await contrAgentPaymentServer.GetAllByContrAgentIdAsync(Id);
            return result;
        }
        else
        {
            return new List<ContrAgentPaymentDto>();
        }
    }
}
