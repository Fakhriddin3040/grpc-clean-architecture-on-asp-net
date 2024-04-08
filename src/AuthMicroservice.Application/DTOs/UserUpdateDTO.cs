using AuthMicroservice.Domain.Interfaces.DTOs;

namespace AuthMicroservice.Application.DTOs
{
    public class UserUpdateDTO : IUserUpdateDTO
    {
		public Guid Id { get; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Salt { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateOnly? Birthday { get; set; }
    }
}