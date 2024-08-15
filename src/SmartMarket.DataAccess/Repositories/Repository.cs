using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities;

namespace SmartMarket.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity
    : BaseEntity
{
    private DbSet<TEntity> _dbSet;
    private AppDbContext _appDb;

    public Repository(AppDbContext appDb)
    {
        _dbSet = appDb.Set<TEntity>();
        _appDb = appDb;
    }
    public bool Add(TEntity entity)
    {
        _dbSet.Add(entity);
        int result = _appDb.SaveChanges();

        return result > 1 ? true : false;
    }
    public IQueryable<TEntity> GetAll()
    => _dbSet;
    public TEntity? GetById(Guid id)
    {
        TEntity? entity = _dbSet.FirstOrDefault(x => x.Id == id);
        if (entity == null)
        {
            return null;
        }

        return entity;
    }
    public bool Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        int result = _appDb.SaveChanges();

        return result > 1 ? true : false;
    }
    public bool Update(TEntity entity)
    {
        _dbSet.Update(entity);
        int result = _appDb.SaveChanges();

        return result > 1 ? true : false;
    }
}
