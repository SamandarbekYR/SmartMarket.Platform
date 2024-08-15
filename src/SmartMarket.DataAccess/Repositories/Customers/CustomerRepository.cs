using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Customers;
using SmartMarket.Domain.Entities.Customers;

namespace SmartMarket.DataAccess.Repositories.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomer
    {
        public CustomerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
