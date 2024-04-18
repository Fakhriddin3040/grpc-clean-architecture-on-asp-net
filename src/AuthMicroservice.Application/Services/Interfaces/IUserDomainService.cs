using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;

namespace AuthMicroservice.Application.Interfaces.Services
{
    public interface IUserDomainService
    {
        IQueryable<User> GetAll(int pageNumber = 1, int pageSize = 10);

        Task<User> GetDetail(Guid id);

        Task<User> Create(User user);

        Task<User> Update(Guid id, User userUpdateDTO);

        Task Delete(Guid id);

        Task SaveChanges();

        Task<bool> Exists(Expression<Func<User, bool>> expression);
    }
}