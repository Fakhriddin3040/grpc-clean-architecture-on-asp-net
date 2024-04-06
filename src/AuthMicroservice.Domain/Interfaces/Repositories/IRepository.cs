using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Entities;
using Microsoft.CodeAnalysis.Operations;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IRepository<TEntity> where TEntity : IGuid
	{
		Task<IQueryable<TEntity>> GetAll();
		Task<TEntity> GetDetail(Guid id);
		Task Create(TEntity entity);
		Task Update(Guid id, TEntity entity);
		Task Delete(TEntity entity);
		Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
		Task<bool> SaveChanges();

	}
}