using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserUpdateDTO : IGuid, IBaseUserDTO, IPersonDTO, ISalt
	{
	}
}