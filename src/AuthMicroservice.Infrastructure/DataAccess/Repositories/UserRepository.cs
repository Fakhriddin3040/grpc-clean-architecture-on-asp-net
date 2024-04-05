using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure.DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly AuthDbContext _context;
        private DbSet<IUser> _dbSet => _context.Set<IUser>();

#region Get Methods

        public UserRepository(AuthDbContext authDbContext)
        {
            _context = authDbContext;
        }
        
		public IQueryable<IUser> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IQueryable<IUser>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public IUser GetDetail(Guid id)
        {
            return _dbSet.Find(id)!;
        }

        public async Task<IUser> GetDetailAsync(Guid id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public IUser GetByUsername(string username)
        {
            return _dbSet.SingleOrDefault(x => x.Username == username);
        }

        public async Task<IUser> GetByUsernameAsync(string username)
        {
            return await _dbSet.SingleOrDefaultAsync(
                x => x.Username == username);
        }

#endregion

#region Create and Update

        public bool Create(IUser entity)
        {
            return default;
        }

        public async Task<bool> CreateAsync(IUser entity)
        {
        await _dbSet.AddAsync(entity);
        return await SaveAsync();
        }
        
        public bool Update(IUser entity)
        {
        var user = GetDetail(entity.Id)!;
        _dbSet.Update(user);
        return true;
        }

        public async Task<bool> UpdateAsync(Guid id, IUser entity)
        {
        var user = await GetDetailAsync(id);
        _dbSet.Update(user);

        return await Task.FromResult(true);
        }
#endregion

#region Delete

        public bool Delete(Guid id)
        {
        var user = GetDetail(id)!;
        _dbSet.Remove(user);
        return Save();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
        var user = await GetDetailAsync(id)!;
        _dbSet.Remove(user!);
        return await SaveAsync();
        }

#endregion

#region Others
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Any(Expression<Func<IUser, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<IUser, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }
#endregion
}
