using SmartMarket.Domain.Entities;

namespace SmartMarket.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity
        : BaseEntity
    {
        Task<Guid> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(TEntity entity);
        Task<TEntity?> GetById(Guid id);
        IQueryable<TEntity> GetAll();
    }
}
