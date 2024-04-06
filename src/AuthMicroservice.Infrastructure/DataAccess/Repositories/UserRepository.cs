using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure.DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext authDbContext)
        {
            _context = authDbContext;
        }
        
        public async Task<IQueryable<User>> GetAll()
        {
            return await Task.FromResult(_context.Users.AsQueryable());
        }

        public async Task<User> GetDetail(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
        }
        
        public async Task Update(Guid id, User entity)
        {
        var user = await GetDetail(id);
        _context.Users.Update(user);
        }

        public async Task Delete(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Any(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.AnyAsync(expression);
        }
    }
}
