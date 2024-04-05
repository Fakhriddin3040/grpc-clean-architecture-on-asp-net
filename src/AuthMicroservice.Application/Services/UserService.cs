using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Domain.Interfaces.Services;
using Grpc.Core;

namespace AuthMicroservice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool Exists(Expression<Func<IUser, bool>> expression)
        {
            return _repository.Any(expression);
        }

        public Guid Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IUserListDTO Create(IUser user)
        {
            IUser newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
            };
            _repository.Create(newUser);
            return newUser as IUserListDTO;
        }

        public IQueryable<IUserListDTO> GetAll()
        {
            return _repository.GetAll().Select(u => u as IUserListDTO);
        }

        public IUserDetailDTO GetDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUserListDTO Update(IUserUpdateDTO user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}