using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Customers;
using SmartMarketDesktop.ViewModels.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Customers
{
    public class CustomerRepository : Repository<CustomerView>, ICustomer
    {
        public CustomerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}