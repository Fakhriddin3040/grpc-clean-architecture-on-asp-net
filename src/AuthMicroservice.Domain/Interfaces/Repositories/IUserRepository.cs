using AuthMicroservice.Domain.Entities;

namespace AuthMicroservice.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}