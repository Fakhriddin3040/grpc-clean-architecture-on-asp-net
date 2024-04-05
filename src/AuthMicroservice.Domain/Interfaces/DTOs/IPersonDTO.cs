using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IPersonDTO : IContactsDTO
	{
		string FirstName { get; }
		string LastName { get; }
		int Age { get; }
		DateOnly Birthday { get; }
	}
}