

namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserDetailDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthday { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
