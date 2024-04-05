using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
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

    public IQueryable<IUserListDTO> GetAll()
    {
        return _repository.GetAll();
    }

    public Guid Authenticate(string username, string password)
    {
        var user = _repository.GetByUsername(username);

        if (user == null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "User not found"));
        }

        return user.Id;
    }

    public async Task<Guid> AuthecticateAsync(string username, string password)
	{
		var user = await _repository.GetByUsernameAsync(username);

		if (user == null)
		{
			throw new RpcException(
				new Status(StatusCode.NotFound, "User not found"));
		}

		return user.Id;
	}


    public async Task<Guid> AuthenticateAsync(string username, string password)
    {
        var user = (await _repository.GetAllAsync()).FirstOrDefault( x =>
            x.Username == username && x.Password == password
            );

        if (user == null)
            throw new RpcException(
                new Status(StatusCode.NotFound, "User not found")
            );

        return user.Id;
    }
    }
}