using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IUserRepositoryAsync : IRepositoryAsync<IUser>
	{
		Task<IUser> GetByUsernameAsync(string username);
	}
}