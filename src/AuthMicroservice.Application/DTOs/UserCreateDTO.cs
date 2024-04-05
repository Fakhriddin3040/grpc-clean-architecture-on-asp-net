using AuthMicroservice.Domain.Interfaces.DTOs;

namespace AuthMicroservice.Application.DTOs
{
    public class UserCreateDTO : IUserCreateDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateOnly Birthday { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}