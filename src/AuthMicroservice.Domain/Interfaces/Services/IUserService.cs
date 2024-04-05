using System.Linq.Expressions;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IUserService 
	{
		Guid Authenticate(string username, string password);
		IQueryable<IUserListDTO> GetAll();
		IUserDetailDTO GetDetail(Guid id);
		IUserListDTO Create(IUser user);
		IUserListDTO Update(IUserUpdateDTO user);
		void Delete(Guid id);
		bool SaveChanges();
		bool Exists(Expression<Func<IUser, bool>> expression);
}
}