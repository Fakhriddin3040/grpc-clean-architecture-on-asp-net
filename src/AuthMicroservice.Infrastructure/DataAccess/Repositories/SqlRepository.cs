using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Domain.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthMicroservice.Infrastructure.DataAccess.Repositories;

public class SqlRepository<T> : ISqlRepository<T>, ISqlRepositoryAsync<T> where T : class, IGuid
{
	private readonly DbContext _context;

	public SqlRepository(DbContext dbContext)
	{
		_context = dbContext ?? throw new ArgumentNullException();
	}

	public IQueryable<T> GetAll()
	{
		return _context.Set<T>();
	}

	public T GetById(Guid id)
	{
		return _context.Set<T>().SingleOrDefault(v => v.Id == id)!;
	}

	public bool Create(T obj)
	{
		_context.Add(obj);

		return this.Save();
	}

	public bool Update(Guid id, T obj)
	{
		var existing_obj = _context.Set<T>().SingleOrDefault(e => e.Id == obj.Id);

		if (existing_obj == null)
		{
			return false;
		}

		_context.Entry(existing_obj).CurrentValues.SetValues(obj);

		return this.Save();
	}

	public bool Delete(Guid id)
	{
		var obj = _context.Set<T>().Find(id);

		if (obj == null)
		{
			return false;
		}

		_context.Remove(obj);

		return this.Save();
	}

	public bool Any(Expression<Func<T, bool>> predicate)
	{
		return _context.Set<T>().Any(predicate);
	}

	public bool Save()
	{
		return _context.SaveChanges() > 0;
	}

	public async Task<bool> SaveAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<T> GetByIdAsync(Guid id)
	{
		return await _context.Set<T>().SingleOrDefaultAsync(v => v.Id == id);
	}
	public async Task<bool> CreateAsync(T obj)
	{
		await _context.AddAsync(obj);

		return await this.SaveAsync();
	}
	public async Task<bool> UpdateAsync(Guid id, T obj)
	{
		var existing_obj = await _context.Set<T>().SingleOrDefaultAsync(e => e.Id == obj.Id);

		if (existing_obj == null)
		{
			return false;
		}

		_context.Entry(existing_obj).CurrentValues.SetValues(obj);

		return await this.SaveAsync();
	}
	public async Task<bool> DeleteAsync(Guid id)
	{
		var obj = await _context.Set<T>().FindAsync(id);

		if (obj == null)
		{
			return false;
		}

		_context.Remove(obj);

		return await this.SaveAsync();
	}

	public async Task<IQueryable<T>> GetAllAsync()
	{
		return await Task.FromResult(_context.Set<T>());
	}

	public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
	{
		return await _context.Set<T>().AnyAsync(predicate);
	}
}