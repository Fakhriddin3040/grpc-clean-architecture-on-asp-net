using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Application.Interfaces.Services;
using AuthMicroservice.Domain.Exceptions;

namespace AuthMicroservice.Application.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _repository;

        private readonly IPasswordService _passwordService;

        public UserDomainService(
            IUserRepository repository, 
            IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _repository = repository;
        }

        public IQueryable<User> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var users = _repository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return users;
        }

        public async Task<User> GetDetail(Guid id)
        {
            var user = await _repository.GetDetail(id);
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _repository.GetDetail(id);
            
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return user;
        }

        public async Task<User> Create(User newUser)
        {
            bool created = await _repository.Create(newUser);

            if (!created)
            {
                throw new EntityNotFoundException();
            }

            var createdUser = await GetById(newUser.Id);

            return createdUser;
        }

        public async Task<User> Update(Guid id, User user)
        {
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            bool updated = await _repository.Update(id, user); 

            if (!updated)
            {
                throw new EntityNotFoundException();
            }

            return user;
        }

        public async Task Delete(Guid id)
        {
            var user = await _repository.GetDetail(id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            bool deleted = await _repository.Delete(user);

            if (!deleted)
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task<bool> Exists(Expression<Func<User, bool>> expression)
        {
            return await _repository.Any(expression);
        }

        public async Task SaveChanges()
        {
            await _repository.SaveChanges();
        }
    }
}