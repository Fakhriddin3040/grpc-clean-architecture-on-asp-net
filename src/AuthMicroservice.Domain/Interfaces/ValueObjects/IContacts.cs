namespace AuthMicroservice.Domain.Interfaces.ValueObjects
{
	public interface IContacts
	{
		string Email { get; }
		string Phone { get; }
		bool IsValidEmail(string email);
		bool IsValidPhone(string phone);
	}
}