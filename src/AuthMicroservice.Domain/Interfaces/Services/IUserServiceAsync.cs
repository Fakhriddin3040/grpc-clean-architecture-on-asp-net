using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IUserServiceAsync : IBaseUser
	{
		Guid AuthenticateAsync(string username, string password);
		Task<IQueryable<IUserListDTO>> GetAllAsync();
		Task<IUserDetailDTO> IUser(Guid id);
		Task<IUserListDTO> CreateAsync(IUserCreateDTO entity);
		Task<IUserListDTO> UpdateAsync(Guid id, IUserUpdateDTO entity);
		Task<bool> DeleteAsync(Guid id);
		Task<bool> SaveAsync();
		Task<bool> AnyAsync(Expression<Func<IBaseUser, bool>> expression);
	}
}