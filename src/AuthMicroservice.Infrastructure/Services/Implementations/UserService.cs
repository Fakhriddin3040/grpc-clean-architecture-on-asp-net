using AuthMicroservice.Application.Interfaces.Services;
using AuthMicroservice.Infrastructure.DTOs;
using AuthMicroservice.Application;
using AuthMicroservice.Infrastructure.Services.Interfaces;
using AutoMapper;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using AuthMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        IMapper _mapper;
        IPasswordService _passwordService;
        IUserDomainService _userDomainService;

        public UserService(
            IMapper mapper,
            IPasswordService passwordService,
            IUserDomainService userDomainService
        )
        {
            _mapper = mapper;
            _passwordService = passwordService;
            _userDomainService = userDomainService;
        }
        public async Task<UserAuthDTO> Authenticate(string username, string password)
        {
            var user = await GetByUsername(username);

            if (user == null)
            {
                throw new AuthenticationException();
            }

            bool validPassword = 
                _passwordService.VerifyPassword(password, user.Password, user.Salt);

            if (!validPassword)
            {
                throw new AuthenticationException();
            }

            return _mapper.Map<UserAuthDTO>(user);
        }

        public async Task<UserListDTO> Create(UserCreateDTO userCreateDTO)
        {
            var userCreate = _mapper.Map<User>(userCreateDTO);

            userCreate.Salt = _passwordService.GenerateSalt();
            userCreate.Password = _passwordService.HashPassword(userCreateDTO.Password, userCreate.Salt);

            var createdUser = await _userDomainService.Create(userCreate);

            return _mapper.Map<UserListDTO>(createdUser);
        }

        public async Task Delete(Guid id)
        {
            await _userDomainService.Delete(id);
        }

        public IQueryable<UserListDTO> GetAllUsers()
        {
            return _mapper.Map<IQueryable<UserListDTO>>(_userDomainService.GetAll());
        }

        public async Task<UserDetailDTO> GetByUsername(string username)
        {
            var user = await  _userDomainService.GetAll().SingleOrDefaultAsync(d => d.Username == username);

            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task<UserDetailDTO> GetUserDetail(Guid id)
        {
            var user = await _userDomainService.GetDetail(id);
            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task<UserListDTO> Update(Guid id, UserUpdateDTO userUpdateDTO)
        {
            var userUpdate = _mapper.Map<User>(userUpdateDTO);

            var response = await _userDomainService.Update(id, userUpdate);

            return _mapper.Map<UserListDTO>(response);
        }
    }
}