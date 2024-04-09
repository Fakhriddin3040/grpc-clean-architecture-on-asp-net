using AuthMicroservice.Domain.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IUser> GetByUsername(string username);
    }
}