using System.ComponentModel;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserCreateDTO : IBaseUserDTO, IPersonDTO
	{
	}
}