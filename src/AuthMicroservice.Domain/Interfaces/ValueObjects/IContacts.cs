namespace AuthMicroservice.Domain.Interfaces.ValueObjects
{
	public interface IContacts
	{
		string Email { get; set; }
		string Phone { get; set; }
		// bool IsValidEmail(string email);
		// bool IsValidPhone(string phone);
	}
}