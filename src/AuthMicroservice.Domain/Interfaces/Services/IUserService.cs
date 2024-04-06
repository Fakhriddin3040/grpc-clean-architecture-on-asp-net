using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IUserService 
	{
		IQueryable<IUserListDTO> GetAll();
		IUserDetailDTO GetDetail(Guid id);
		IUserListDTO Create(IUser user);
		IUserListDTO Update(IUserUpdateDTO user);
		void Delete(Guid id);
		void SaveChanges();
		void SaveChangesAsync();
		bool Exists(Expression<Func<IUser, bool>> expression);
}
}