using System.Linq.Expressions;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IUserService : IUserServiceAsync
	{
		Guid Authenticate(string username, string password);
		IQueryable<IUserListDTO> GetAll();
		IUserDetailDTO GetById(Guid id);
		IUserListDTO Create(IBaseUser user, string password);
		IUserListDTO Update(IBaseUser user, string password);
		void Delete(Guid id);
		bool SaveChanges();
		bool Any(Expression<Func<IBaseUser, bool>> expression);
}
}