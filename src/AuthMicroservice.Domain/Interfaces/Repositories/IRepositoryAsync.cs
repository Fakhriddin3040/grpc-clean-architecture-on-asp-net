using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IRepositoryAsync<TEntity> where TEntity : IGuid
	{
		Task<IQueryable<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(Guid id);
		Task<bool> CreateAsync(TEntity entity);
		Task<bool> UpdateAsync(Guid id, TEntity entity);
		Task<bool> DeleteAsync(Guid id);
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> SaveAsync();
	}
}