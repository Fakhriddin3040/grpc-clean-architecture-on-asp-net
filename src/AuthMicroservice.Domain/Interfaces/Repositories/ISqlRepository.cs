using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AuthMicroservice.Domain.Interfaces.Repositories;

public interface ISqlRepository<T>
{
	IQueryable<T> GetAll();
	T GetDetail(Guid id);
	bool Create(T obj);
	bool Update(Guid id, T obj);
	bool Delete(Guid id);
	bool Save();
	bool Any(Expression<Func<T, bool>> predicate);	
}