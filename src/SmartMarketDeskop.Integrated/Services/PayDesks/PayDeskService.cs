using SmartMarket.Service.DTOs.PayDesks;
using SmartMarketDeskop.Integrated.Server.Interfaces.PayDesks;
using SmartMarketDeskop.Integrated.Server.Repositories.PayDesks;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.PayDesks;

public class PayDeskService : IPayDeskService
{
    private readonly IPayDesksServer _payDeskServer;

    public PayDeskService()
    {
        this._payDeskServer = new PayDeskServer();
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
    public async Task<bool> CreatePayDesk(AddPayDesksDto dto)
    {
        if (IsInternetAvailable())
        {
            bool result = await _payDeskServer.AddAsync(dto);

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

    public async Task<bool> DeletePayDesk(Guid id)
    {
        if (IsInternetAvailable())
        {
            bool result = await _payDeskServer.DeleteAsync(id);

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

    public async Task<List<PayDesksDto>> GetAll()
    {
        if (IsInternetAvailable())
        {
            var paydesk = await _payDeskServer.GetAllAsync();
            return paydesk;
        }
        else
        {
            return new List<PayDesksDto>();
        }
    }

    public async Task<bool> UpdatePayDesk(AddPayDesksDto dto, Guid id)
    {
        if (IsInternetAvailable())
        {
            bool result = await _payDeskServer.UpdateAsync(dto, id);

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
