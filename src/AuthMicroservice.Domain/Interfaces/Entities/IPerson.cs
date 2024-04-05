using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
	public interface IPerson
	{
		string FirstName { get; }
		string LastName { get; }
		int Age { get; }
		DateOnly Birthday { get; }
		Contacts Contacts { get; }
	}
}