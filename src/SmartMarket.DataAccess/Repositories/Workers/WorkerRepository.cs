﻿using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class WorkerRepository : Repository<Worker>, IWorker
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<Worker> _workers;

        public WorkerRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _workers = appDb.Set<Worker>();
        }

        public async Task<Worker?> GetPhoneNumberAsync(string phoneNumber)
        {
            return await _workers
                .Include(w => w.Position)
                .Include(w => w.WorkerRole)
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<List<Worker>> GetWorkersFullInformationAsync()
        {
            return await _workers
                .Include(w => w.Position)
                .Include(w => w.WorkerRole)
                .Include(w => w.SalaryChecks)
                .Include(w => w.WorkerDebts)
                .Include(w => w.Products)
                .Include(w => w.ProductSales)
                    .ThenInclude(ps => ps.Product)
                .Include(w => w.LoadReports)
                .Include(w => w.ReplaceProducts)
                .Include(w => w.InvalidProducts)
                .Include(w => w.Orders)
                .Include(w => w.Expenses)
                .ToListAsync();
        }
    }
}