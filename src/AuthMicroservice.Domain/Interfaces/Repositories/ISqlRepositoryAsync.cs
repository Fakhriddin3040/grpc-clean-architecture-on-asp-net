using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.Fields;

public interface ISqlRepositoryAsync<T> where T : IGuid
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T> GetDetailAsync(Guid id);
    Task<bool> CreateAsync(T obj);
    Task<bool> UpdateAsync(Guid id, T obj);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<bool> SaveAsync();
}