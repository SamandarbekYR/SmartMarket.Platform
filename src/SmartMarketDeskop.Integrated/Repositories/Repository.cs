using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbSet<TEntity> _dbSet;
        private readonly AppDbContext _appDbContext;
        public Repository(AppDbContext appDbContext)
        {  
            _dbSet = appDbContext.Set<TEntity>();
            _appDbContext = appDbContext;
        }


        public async Task<bool> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<TEntity> GetAll()
        =>_dbSet.AsQueryable();

        public async Task<TEntity?> GetById(Guid id)
        {
            TEntity? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
