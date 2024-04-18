using System.Diagnostics;
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
    _context = authDbContext ?? throw new ArgumentNullException(nameof(authDbContext));
        }
        
        public IQueryable<User> GetAll()
        {
            return _context.Users.AsQueryable();
        }

        public async Task<User> GetDetail(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> Create(User entity)
        {
            var user = entity as User;
            if (user == null)
                throw new InvalidCastException("Entity is not of type User");

            await _context.Users.AddAsync((User)user);
            return await SaveChanges();
        }
        
        public async Task<bool> Update(Guid id, User entity)
        {
            var user = await GetDetail(id);

            _context.Entry(user).CurrentValues.SetValues(entity);

            return await SaveChanges();
        }

        public async Task<bool> Delete(User entity)
        {
            _context.Users.Remove((User)entity);

            return await SaveChanges();
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
