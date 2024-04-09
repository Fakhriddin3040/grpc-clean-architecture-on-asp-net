using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetDetail(Guid id);
        bool Update(Guid id, TEntity entity);
        bool Delete(Guid id);
        
    }
}