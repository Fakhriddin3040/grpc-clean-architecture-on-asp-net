using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Infrastructure.DTOs;

namespace AuthMicroservice.Infrastructure.Interfaces.Services
{
    public interface IUserService
    {
        IQueryable<UserListDTO> GetAll(int pageNumber = 1, int pageSize = 10);

        Task<UserDetailDTO> GetDetail(Guid id);

        Task<UserDetailDTO> GetByUsername(string username);

        Task<UserDetailDTO> AuthenticateUser(string username, string password);

        Task<UserListDTO> Create(UserCreateDTO user);

        Task<UserListDTO> Update(Guid id, UserUpdateDTO userUpdateDTO);

        Task Delete(Guid id);

        Task SaveChanges();

        Task<bool> Exists(Expression<Func<IUser, bool>> expression);
    }
}