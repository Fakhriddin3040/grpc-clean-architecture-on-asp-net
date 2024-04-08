using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<IUser>
	{
		Task<IUser> GetByUsername(string username);
	}
}