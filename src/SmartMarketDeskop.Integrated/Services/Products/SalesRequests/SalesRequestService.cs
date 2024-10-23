using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using System.Net.NetworkInformation;

namespace SmartMarketDeskop.Integrated.Services.Products.SalesRequests;

public class SalesRequestService : ISalesRequestsService
{
    private readonly ISalesRequestsServer _salesRequestsServer;
    public SalesRequestService()
    {
        this._salesRequestsServer = new SalesRequestServer();
    }

    public bool IsInternetAvailable()
    {
        try
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send("8.8.8.8", 1000);
                return reply.Status == IPStatus.Success;
            }
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> CreateSalesRequest(AddSalesRequestDto dto)
    {
        if (IsInternetAvailable())
        {
            var result = await _salesRequestsServer.AddAsync(dto);
            if (result)
                return true;
            else
                return false;
            
        }
        else
        {
            // local baza uchun malumot saqlanadi
            return false;
        }
    }

    public async Task<IList<SalesRequestDto>> GetAll()
    {
        if (IsInternetAvailable())
        {
            var sales = await _salesRequestsServer.GetAllAsync();
            return sales;
        }
        else
        {
            // local baza uchun malumot saqlanadi
            return new List<SalesRequestDto>();
        }
    }

    public async Task<SalesRequestDto> GetById(Guid id)
    {
        if (IsInternetAvailable())
        {
            var sale = await _salesRequestsServer.GetByIdAsync(id);
            return sale;
        }
        else
        {
            // local baza uchun malumot saqlanadi
            return new SalesRequestDto();
        }
    }

    public async Task<bool> UpdateSalesRequest(AddSalesRequestDto dto, Guid id)
    {
        if (IsInternetAvailable())
        {
            var result = await _salesRequestsServer.UpdateAsync(dto, id );
            if (result)
                return true;
            else
                return false;

        }
        else
        {
            // local baza uchun malumot saqlanadi
            return false;
        }
    }
}
