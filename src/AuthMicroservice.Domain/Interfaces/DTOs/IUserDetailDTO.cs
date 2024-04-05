using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.ValueObjects;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserDetailDTO : IGuid, IBaseUserDTO, IPersonDTO
	{
	}
}