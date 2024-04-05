using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<IUser>, IUserRepositoryAsync
	{
		IUser GetByUsername(string username);
	}
}