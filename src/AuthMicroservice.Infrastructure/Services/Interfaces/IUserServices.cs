using AuthMicroservice.Infrastructure.DTOs;

namespace AuthMicroservice.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<UserListDTO> GetAllUsers();

        Task<UserDetailDTO> GetUserDetail(Guid id);

        Task<UserDetailDTO> GetByUsername(string username);

        Task<UserListDTO> Create(UserCreateDTO userCreateDTO);

        Task<UserListDTO> Update(Guid id, UserUpdateDTO userUpdateDTO);

        Task<UserAuthDTO> Authenticate(string username, string password);

        Task Delete(Guid id);
    }
}