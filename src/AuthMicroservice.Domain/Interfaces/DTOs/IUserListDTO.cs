using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IUserListDTO
	{
		Guid Id { get; }

		string Username { get; set; }

		string FirstName { get; set; }

		string LastName { get; set; }

		string Email { get; set; }

		string Phone { get; set; }

		DateTime CreatedAt { get; }

		DateTime UpdatedAt { get; }
	}
}