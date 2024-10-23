using SmartMarketDesktop.ViewModels.Entities;

namespace SmartMarketDeskop.Integrated.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> Add(TEntity entity);
    Task<bool> Update(TEntity entity);
    Task<bool> Remove(TEntity entity);
    Task<TEntity?> GetById(Guid id);
    IQueryable<TEntity> GetAll();
}
