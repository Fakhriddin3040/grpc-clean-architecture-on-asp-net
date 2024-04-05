using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Entities;
using Microsoft.CodeAnalysis.Operations;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IRepository<T> where T : IGuid
	{
		IQueryable<T> GetAll();
		T GetById(Guid id);
		bool Create(T entity);
		bool Update(T entity);
		bool Delete(Guid id);
		bool Save();
		bool Any(Expression<Func<T, bool>> expression);
	}
}