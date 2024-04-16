using System.Linq.Expressions;
using AuthMicroservice.Infrastructure.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Infrastructure.Interfaces.Services;
using AutoMapper;
using Grpc.Core;
using AuthMicroservice.Domain.Exceptions;

namespace AuthMicroservice.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        private readonly IMapper _mapper;

        private readonly IPasswordService _passwordService;

        public UserService(
            IMapper mapper, 
            IUserRepository repository, 
            IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _repository = repository;
            _mapper = mapper;
        }

        public IQueryable<UserListDTO> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var users = _repository.GetAll()
                .Select(user => _mapper.Map<UserListDTO>(user))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return users;
        }

        public async Task<UserDetailDTO> GetDetail(Guid id)
        {
            var user = await _repository.GetDetail(id);
            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task<UserListDTO> GetById(Guid id)
        {
            var user = await _repository.GetDetail(id);
            
            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            return _mapper.Map<UserListDTO>(user);
        }

        public async Task<UserDetailDTO> GetByUsername(string username)
        {
            var user = await _repository.GetByUsername(username);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task<UserDetailDTO> AuthenticateUser(string username, string password)
        {
            var user = await GetByUsername(username);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            var hashedPassword = _passwordService.HashPassword(password, user.Salt);

            bool correctAuth = _passwordService.VerifyPassword(password, user.Salt, hashedPassword);

            if (!correctAuth)
            {
                throw new EntityNotFoundException();
            }

            return user;
        }

        public async Task<UserListDTO> Create(UserCreateDTO userDto)
        {
            User newUser = _mapper.Map<User>(userDto);

            newUser.Password = _passwordService.HashPassword(newUser.Password, newUser.Salt);

            bool created = await _repository.Create(newUser);

            if (!created)
            {
                throw new RpcException(new Status(StatusCode.Internal, "User creation failed"));
            }

            UserListDTO createdUser = await GetById(newUser.Id);

            return createdUser;
        }

        public async Task<UserListDTO> Update(Guid id, UserUpdateDTO userUpdateDTO)
        {
            var user = await _repository.GetDetail(id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            _mapper.Map(userUpdateDTO, user);

            bool updated = await _repository.Update(id, user); 

            if (!updated)
            {
                throw new RpcException(new Status(StatusCode.Internal, "User update failed"));
            }

            return _mapper.Map<UserListDTO>(user);
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

        public async Task<bool> Exists(Expression<Func<IUser, bool>> expression)
        {
            return await _repository.Any(expression);
        }

        public async Task SaveChanges()
        {
            await _repository.SaveChanges();
        }
    }
}