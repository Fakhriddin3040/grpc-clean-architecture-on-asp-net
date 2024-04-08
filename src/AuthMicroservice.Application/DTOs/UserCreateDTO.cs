using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Application.DTOs
{
    public class UserCreateDTO : BaseEntity, IUserCreateDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Salt { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateOnly? Birthday { get; set; }
    }
}