using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.PayDesks;
using SmartMarketDesktop.ViewModels.Entities.PayDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.PayDesks
{
    public class PayDeskRepository : Repository<PayDeskView>, IPayDesk
    {
        public PayDeskRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
