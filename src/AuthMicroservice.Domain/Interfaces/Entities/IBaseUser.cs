using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
	public interface IBaseUser : IPassword
	{
		string Username { get; }
		bool? IsActive { get; }
	}
}