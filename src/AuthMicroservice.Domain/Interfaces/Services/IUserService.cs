using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IUserService 
	{
		IQueryable<IUserListDTO> GetAll();

		Task<IUserDetailDTO> GetDetail(Guid id);

		Task<IUserListDTO> Create(IUserCreateDTO user);

		Task<IUserListDTO> Update(Guid id, IUserUpdateDTO userUpdateDTO);

		Task Delete(Guid id);

		Task SaveChanges();

		Task<bool> Exists(Expression<Func<IUser, bool>> expression);
}
}