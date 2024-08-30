using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Partners;
using SmartMarketDesktop.ViewModels.Entities.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Partners
{
    public class PartnerRepository : Repository<PartnerView>, IPartner
    {
        public PartnerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}