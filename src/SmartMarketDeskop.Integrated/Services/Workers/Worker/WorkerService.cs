using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Worker
{
    public class WorkerService : IWorkerService
    {
       
        public async Task<bool> CreateProduct(WorkerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(Guid Id)
        {
            throw new NotImplementedException();
        }

      

        public Task<bool> UpdateProduct(WorkerDto worker, Guid Id)
        {
            throw new NotImplementedException();
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
    }
}
