using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Domain.Interfaces.Services;
using AutoMapper;
using Grpc.Core;

namespace AuthMicroservice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Exists(Expression<Func<User, bool>> expression)
        {
            return await _repository.Any(expression);
        }

        public async Task<IUserListDTO> GetById(Guid id)
        {
            var user = await _repository.GetDetail(id);
            return user as IUserListDTO;
        }

        public User Create(IUserCreateDTO user)
        {
            var newUser = _mapper.Map<User>(user);

            _repository.Create(newUser);
            return _repository.GetDetail(newUser.Id);
        }

        public IQueryable<UserListDTO> GetAll()
        {
            return _repository.GetAll().Select(u => u as UserListDTO);
        }

        public UserDetailDTO GetDetail(Guid id)
        {
            throw new NotmplementedException();
        }

        public UserListDTO Update(UserUpdateDTO user)
        {
            throw new NotmplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotmplementedException();
        }

        public void SaveChangesAsync()
        {
            _repository.SaveChanges();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}