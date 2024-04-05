using System.ComponentModel.DataAnnotations;
using AuthMicroservice.Domain.Interfaces.ValueObjects;

namespace AuthMicroservice.Domain.ValueObjects;
public class Contacts : IContacts
{
	public string Email { get; }
	public string Phone { get; }

	public Contacts(string email, string phone)
	{
		if (!IsValidEmail(email))
			throw new ArgumentException("Invalid email address");
		if (!IsValidPhone(phone))
			throw new ArgumentException("Invalid phone number");

		Email = email;
		Phone = phone;
	}

	public bool IsValidEmail(string email)
	{
		return new EmailAddressAttribute().IsValid(email);
	}

	public bool IsValidPhone(string phone)
	{
		return new PhoneAttribute().IsValid(phone);
	}
}