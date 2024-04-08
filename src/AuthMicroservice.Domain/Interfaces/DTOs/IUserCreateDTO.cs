using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserCreateDTO : IGuid, IBaseUserDTO, IPersonDTO, ISalt
	{
		string Password { get; set; }
	}
}