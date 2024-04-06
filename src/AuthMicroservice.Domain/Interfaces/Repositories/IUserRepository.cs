using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetByUsername(string username);
	}
}