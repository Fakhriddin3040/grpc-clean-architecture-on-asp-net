using AuthMicroservice.Domain.Abstracts.Fields;
using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserCreateDTO : SaltGenerator, IBaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthday { get; set; }
    }
}