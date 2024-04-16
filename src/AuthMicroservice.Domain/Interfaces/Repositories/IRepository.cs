using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetDetail(Guid id);

        Task<bool> Create(TEntity entity);

        Task<bool> Update(Guid id, TEntity entity);

        Task<bool> Delete(TEntity entity);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<bool> SaveChanges();
}
    }