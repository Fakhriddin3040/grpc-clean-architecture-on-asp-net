using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Entities;
using Microsoft.CodeAnalysis.Operations;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IRepository<TEntity> where TEntity : IGuid
	{
		IQueryable<TEntity> GetAll();
		Task<IQueryable<TEntity>> GetAllAsync();
		TEntity GetDetail(Guid id);
		Task<TEntity> GetDetailAsync(Guid id);
		bool Create(TEntity entity);
		Task<bool> CreateAsync(TEntity entity);
		bool Update(TEntity entity);
		Task<bool> UpdateAsync(Guid id, TEntity entity);
		bool Delete(Guid id);
		Task<bool> DeleteAsync(Guid id);
		bool Save();
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
		bool Any(Expression<Func<TEntity, bool>> expression);
		Task<bool> SaveAsync();

	}
}