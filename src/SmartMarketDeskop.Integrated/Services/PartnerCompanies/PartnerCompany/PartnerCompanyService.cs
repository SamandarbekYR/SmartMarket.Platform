using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany
{
    public class PartnerCompanyService : IPartnerCompanyService
    {

        private IPartnerComanyServer _partnerComanyServer;

        public PartnerCompanyService()
        {
            this._partnerComanyServer=new SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany.PartnerCompanyServer();
        }


        public async Task<bool> CreateCompany(AddPartnerCompanyDto partnerCompanyDto)
        {
            if(IsInternetAvailable())
            { 
                await _partnerComanyServer.AddCompany(partnerCompanyDto);
                return true;    
            }
            else
            {

                // localni bazaga saqlaymiz
                return false;
            }
        }

        public async Task<bool> DeleteCompany(Guid id)
        {
            if(IsInternetAvailable())
            {
                var res = await _partnerComanyServer.DeleteCompany(id);

                if(res)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PartnerCompanyView>> GetAllCompany()
        {
            if (IsInternetAvailable())
            {
                return await _partnerComanyServer.GetAllCompany();

            }
            else
            {
                return new List<PartnerCompanyView>();  
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

        public async Task<bool> UpdateCompany(Guid id, AddPartnerCompanyDto dto)
        {
            if (IsInternetAvailable())
            {
                var res = await _partnerComanyServer.UpdateCompany(id, dto);

                if (res)
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
}
