using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
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
            bool result = await _partnerServer.DeleteAsync(Id);
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
            bool result = await _partnerServer.UpdateAsync(partner, Id);
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

    public async Task<PartnerDto> GetByName(string name)
    {
        if (IsInternetAvailable())
        {
            var partner = await _partnerServer.GetByNameAsync(name);
            return partner;
        }
        else
        {
            return null!;
        }
    }

    public async Task<bool> UpdatePartnerDebtSum(double debtSum, Guid Id)
    {
        if (IsInternetAvailable())
        {
            bool result = await _partnerServer.UpdateDebtSumAsync(debtSum, Id);

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
}

