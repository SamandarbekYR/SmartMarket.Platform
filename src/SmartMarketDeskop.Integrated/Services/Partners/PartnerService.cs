using SmartMarket.Domain.Entities.Partners;
using SmartMarketDeskop.Integrated.Server.Interfaces.Partners;
using SmartMarketDeskop.Integrated.Server.Repositories.Partners;
using SmartMarketDesktop.DTOs.DTOs.Partners;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Partners;

public class PartnerService : IPartnerService
{
    private readonly IPartnerServer _partnerServer;
    public PartnerService()
    {
        this._partnerServer = new PartnerServer();
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

    public async Task<bool> CreatePartner(PartnerCreateDto dto)
    {
        if (IsInternetAvailable())
        {
            bool result = await _partnerServer.AddAsync(dto);

            if (result)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeletePartner(Guid Id)
    {
        if (IsInternetAvailable())
        {
            await _partnerServer.DeleteAsync(Id);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<Partner>> GetAll()
    {
        if (IsInternetAvailable())
        {
            var partners = await _partnerServer.GetAllAsync();
            return partners;
        }
        else
        {
            return new List<Partner>();
        }
    }

    public async Task<bool> UpdatePartner(PartnerCreateDto partner, Guid Id)
    {
        if (IsInternetAvailable())
        {
            await _partnerServer.UpdateAsync(partner, Id);
            return true;
        }
        else
        {
            return false;
        }
    }
}

