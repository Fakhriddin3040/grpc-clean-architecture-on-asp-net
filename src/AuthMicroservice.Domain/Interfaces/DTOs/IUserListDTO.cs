using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserListDTO : IGuid, IBaseUserDTO, IContactsDTO
	{
	}
}